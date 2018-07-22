    using Eco.Gameplay.Players;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Chat;
    using System;
    using System.Collections.Generic;
    using System.Linq;
using Eco.Gameplay.Property;
using Eco.Shared.Math;
using Eco.Shared.Utils;
using Eco.Shared.Networking;
using Eco.Simulation.WorldLayers;
using Eco.Simulation.Types;
using Eco.Shared.Localization;
using Eco.Simulation.Time;
using Eco.Gameplay;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Skills;
using Eco.Core.Utils;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Items;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Components;
using Eco.World;
using REYmod.Blocks;
using REYmod.Utils;
using REYmod.Config;
using Eco.Core;
using Eco.Core.Plugins.Interfaces;
using Eco.Mods.TechTree;
using Eco.Shared.Services;
using System.ComponentModel;
using Eco.Gameplay.Rooms;

namespace REYmod.Core.ChatCommands
{
    public class ChatCommands : IChatCommandHandler
    {
        #region ADMIN Commands
        [ChatCommand("globalroomfix", "Reevaluates all rooms that have at least one worldobject placed in them", level: ChatAuthorizationLevel.Admin)]
        public static void GlobalRoomFix(User user)
        {
            foreach(WorldObject obj in WorldObjectManager.All)
            {
                RoomData.QueueRoomTest(obj.Position3i);
                //Console.WriteLine("Checked " + obj.Name + " at " + obj.Position3i.ToStringLabelled("pos"));
            }
            RoomData.Obj.UpdateRooms();
            ChatUtils.SendMessage(user, "Rooms should be fixed now");

        }


        [ChatCommand("tp", "Opens a list of online players. Click the player you(or the given player) should be teleported to", level: ChatAuthorizationLevel.Admin)]
        public static void Tp(User user, string username = "")
        {
            User usertoteleport = UserManager.FindUserByName(username);
            if (usertoteleport == null) usertoteleport = user;

            string panelcontent = "Select Player: <br><br>";
            foreach (User onlineuser in UserManager.OnlineUsers)
            {
                panelcontent += new Button(player => { usertoteleport.Player.SetPosition(onlineuser.Player.Position); }, content: onlineuser.Name, singleuse: true, clickdesc: "Click to teleport " + usertoteleport.Name + " to " + onlineuser.Name).UILink();
                panelcontent += "<br>";
            }
            user.Player.OpenInfoPanel("Teleport Menu", panelcontent);
        }

        [ChatCommand("openauth", "Opens the authmenu for the Plot your currently standing on", level: ChatAuthorizationLevel.Admin)]
        public static void OpenAuth(User user)
        {
            AuthorizationController controller =  PropertyManager.GetAuth(user.Position.XZi);
            if (controller != null) controller.OpenAuthorizationMenuOn(user.Player, new DeedItem());
            else ChatUtils.SendMessage(user, "Plot is not claimed!",true);
        }

        [ChatCommand("setowner", "Opens the authmenu for the Plot your currently standing on", level: ChatAuthorizationLevel.Admin)]
        public static void SetOwner(User user, string newowner)
        {
            User newowneruser = UserManager.FindUserByName(newowner);
            AuthorizationController controller = PropertyManager.GetAuth(user.Position.XZi);
            if (controller == null)
            {
                ChatUtils.SendMessage(user, "Plot is not claimed!");
                return;
            }
            if (newowneruser!=null)
            {
                controller.SetOwner(newowneruser.Name);
                ChatUtils.SendMessage(user, newowneruser + " is now the owner of " + controller.Name);
            }
            else ChatUtils.SendMessage(user, "User not found!",true);
        }

        [ChatCommand("unclaimselect", "Selects the owner of the plot you're standing on for unclaimconfirm", level: ChatAuthorizationLevel.Admin)]
        public static void UnclaimPlayer(User user, string ownername = "")
        {
            User owner = null;
            Tuple<User, int> ownerandcode;
            int confirmationcode = 0;
            bool inactive = true;
            if (ownername == "")
            {
                if (PropertyManager.GetPlot(user.Position.XZi) != null)
                {
                    owner = PropertyManager.GetPlot(user.Position.XZi).Owner;
                }
            }
            else owner = UserManager.FindUserByName(ownername);

            if (owner == null)
            {
                user.Player.SendTemporaryMessageAlreadyLocalized("Player not Found");
                return;
            }
            if ((WorldTime.Seconds - owner.LogoutTime) < REYmodSettings.Obj.Config.Maxinactivetime * 3600)
            {
                confirmationcode = RandomUtil.Range(1000, 9999);
                inactive = false;
            }



            if (UtilsClipboard.UnclaimSelector.ContainsKey(user)) UtilsClipboard.UnclaimSelector.Remove(user);
            UtilsClipboard.UnclaimSelector.Add(user, new Tuple<User, int>(owner, confirmationcode));

            UtilsClipboard.UnclaimSelector.TryGetValue(user, out ownerandcode);
            owner = ownerandcode.Item1;
            user.Player.SendTemporaryMessageAlreadyLocalized("Selected " + owner.UILink() + " as target! ");
            if (owner.LoggedIn)
            {
                user.Player.SendTemporaryMessageAlreadyLocalized((owner.UILink() + " IS ONLINE! ").Color("red").Bold());
                user.Player.SendTemporaryMessageAlreadyLocalized("Confirmationcode: " + confirmationcode);
            }
            else if (!inactive) user.Player.SendTemporaryMessageAlreadyLocalized("WARNING: Player only offline for " + TimeSpan.FromSeconds(WorldTime.Seconds - owner.LogoutTime).ToString() + ". Use Confirmation Code:" + confirmationcode);


        }

        [ChatCommand("unclaimconfirm", "Unclaims all property of the selected player", level: ChatAuthorizationLevel.Admin)]
        public static void UnclaimConfirm(User user, int confirmcode = 0)
        {
            User target = null;
            Tuple<User, int> targetandcode;
            int totalplotcount = 0, deedcount = 0, plotcountdeed = 0, vehiclecount = 0, destroyedvehicles = 0;
            IEnumerable<Vector2i> positions;
            string tmp = string.Empty;
            WorldObject vehicle = null;
            ItemStack sourcestack;
            ItemStack targetstack;
            Func<ItemStack, bool> findDeed = i =>
            {
                if (i.Item as DeedItem != null) return ((i.Item as DeedItem).AuthID == vehicle.GetComponent<StandaloneAuthComponent>().AuthGuid);
                return false;
            };

            if (!UtilsClipboard.UnclaimSelector.TryGetValue(user, out targetandcode))
            {
                user.Player.SendTemporaryErrorAlreadyLocalized("no User selected");
                return;
            }
            target = targetandcode.Item1;
            if (confirmcode != targetandcode.Item2)
            {
                ChatUtils.SendMessage(user, "Confirmationcode not correct");
                return;
            }
            IEnumerable<AuthorizationController> authorizationControllers = PropertyManager.GetAuthBelongingTo(target);
            foreach (AuthorizationController auth in authorizationControllers)
            {
                plotcountdeed = 0;
                if (auth.Type == "Property")
                {
                    deedcount++;
                    positions = PropertyManager.PositionsForId(auth.Id);
                    foreach (Vector2i pos in positions)
                    {
                        plotcountdeed++;
                        totalplotcount++;
                        //ChatUtils.SendMessage(user, TextLinkManager.MarkUpText("Try unclaiming: (" + pos.X + "," + pos.Y + ")"));
                        PropertyManager.UnclaimProperty(pos);
                    }
                    ChatUtils.SendMessage(user, "Unclaimed: " + auth.Name + " (" + plotcountdeed.ToString() + " Plots)");
                }
                else if (auth.Type == "Vehicle")
                {
                    if (auth.AttachedWorldObjects[0].Object != null)
                    {
                        vehiclecount++;
                        vehicle = auth.AttachedWorldObjects[0].Object;
                        ChatUtils.SendMessage(user, "Found Vehicle: " + auth.Name);
                        //vehicle.Destroy();
                        sourcestack = null;
                        sourcestack = target.Inventory.NonEmptyStacks.FirstOrDefault(findDeed);
                        targetstack = vehicle.GetComponent<PublicStorageComponent>().Inventory.Stacks.FirstOrDefault(i => i.Empty);
                        if (targetstack == null)
                        {
                            targetstack = vehicle.GetComponent<PublicStorageComponent>().Inventory.Stacks.First();
                            vehicle.GetComponent<PublicStorageComponent>().Inventory.ClearItemStack(targetstack);
                        }

                        if (sourcestack != null)
                        {
                            vehicle.GetComponent<PublicStorageComponent>().Inventory.AddItem(sourcestack.Item);
                            auth.SetOwner(user.Name);
                            vehicle.GetComponent<StandaloneAuthComponent>().SetLocked(user.Player, false);
                            auth.SetOwner(target.Name);
                            //target.Inventory.MoveItems(sourcestack, targetstack, target.Player);
                        }
                        else
                        {
                            destroyedvehicles++;
                            vehicle.Destroy();
                        }
                    }


                }
            }

            ChatUtils.SendMessage(user, totalplotcount + " Plots on " + deedcount + " Deeds unclaimed. Also found " + vehiclecount + " Vehicles. " + (vehiclecount - destroyedvehicles) + " have been unlocked and got their deed added to inventory. " + destroyedvehicles + " were destroyed as the deed couldn't be found");
        }

        /// <summary>
        /// -OBSOLETE- Finalizes worldgen by running the Custom Generator part
        /// </summary>
        /// <param name="user"></param>
        /// <param name="force"></param>
        [ChatCommand("spawncustomores", "Finalizes worldgen by running the Custom Generator part", level: ChatAuthorizationLevel.Admin)]
        public static void SpawnCustomOres(User user, string force = null)
        {
            if (CustomWorldGen.newworld || (force == "force"))
            {
                DateTime start = DateTime.Now;
                CustomWorldGen.Generate();
                TimeSpan used = DateTime.Now - start;
                ChatUtils.SendMessage(user, "Worldgen finalized. Time spent: " + used.Minutes + ":" + used.Seconds + ":" + used.Milliseconds);
            }
            else ChatUtils.SendMessage(user, "This is not a newly generated world. This command should only be run once. You can however force it by entering \"force\" as parameter");
        }

        [ChatCommand("vomit+", "(Really) Empties your stomach", ChatAuthorizationLevel.Admin)]
        public static void VomitPlus(User user)
        {
            user.Player.User.Stomach.ClearCalories(user.Player);
            user.Player.User.Stomach.Contents.Clear();
            user.Player.SendTemporaryMessageLoc("Really Bad elk meat?");
        }

        [ChatCommand("passlaw+", "Lets you pass a single law instead of all at once", level: ChatAuthorizationLevel.Admin)]
        public static void PassLawPlus(User user)
        {
            string panelcontent = "Click on the law you want to pass: <br><br>";
            if (Legislation.Laws.AllNonFailedLaws.Where(x => !x.InEffect).Count() != 0)
            {
                Legislation.Laws.AllNonFailedLaws.Where(x => !x.InEffect).ToList().ForEach(x =>
                {
                    panelcontent += new Button(player => { Legislation.Laws.EnactLaw(x); player.OpenInfoPanel("Law passed", "You passed " + x.UILink()); }, tooltip: x.Tooltip(), content: x.Title, singleuse: true, clickdesc: "Click to enact this law").UILink();
                    panelcontent += "<br>";
                });
            }
            else panelcontent = "No pending laws!";
            user.Player.OpenInfoPanel("Passlaw Menu", panelcontent);
        }

        [ChatCommand("setmaxsuperskills", "Displays or set the maximum allowed Superskills. -1 for no limit", level: ChatAuthorizationLevel.Admin)]
        public static void SetMaxSuperSkills(User user, int maxallowed = int.MinValue)
        {
            if (maxallowed == -1) maxallowed = int.MaxValue;
            string currentallowedstr = (REYmodSettings.Obj.Config.Maxsuperskills != int.MaxValue) ? REYmodSettings.Obj.Config.Maxsuperskills.ToString() : "Infinite";
            if (maxallowed == int.MinValue || maxallowed == REYmodSettings.Obj.Config.Maxsuperskills)
            {
                ChatUtils.SendMessage(user, "Max allowed Superskills: " + currentallowedstr);
                return;
            }
            else
            {
                string newallowedstr = (maxallowed != int.MaxValue) ? maxallowed.ToString() : "Infinite";
                REYmodSettings.Obj.Config.Maxsuperskills = maxallowed;
                REYmodSettings.Obj.SaveConfig();
                ChatUtils.SendMessage(user, "Changed the amount of allowed Superskills from " + currentallowedstr + " to " + newallowedstr);
                ChatManager.ServerMessageToAllAlreadyLocalized(user.UILink() + "changed the amount of allowed Superskills from " + currentallowedstr + " to " + newallowedstr, false);
                // ConfigHandler.UpdateConfigFile();
            }


        }

        [ChatCommand("reports", "Displays current Reports", level: ChatAuthorizationLevel.Admin)]
        public static void Reports(User user)
        {
            string x = IOUtils.ReadFileFromConfigFolder("../Reports/reports.txt");
            user.Player.OpenInfoPanel("Recent Reports", x);
        }

        [ChatCommand("clearreports", "Deletes all current reports", level: ChatAuthorizationLevel.Admin)]
        public static void ClearReports(User user)
        {
            IOUtils.ClearFile("./mods/ECO Realism/Reports/reports.txt");
            user.Player.SendTemporaryMessageAlreadyLocalized("Reports cleared");
        }

        #endregion

        #region USER Commands
        [ChatCommand("random", "Rolls a random number between 1 and the given number (default 100), visible to all")]
        public static void Random(User user, int max = 100)
        {
            if (max < 1) max = 1;
            int x = RandomUtil.Range(1, max);
            ChatManager.ServerMessageToAllAlreadyLocalized(user.Name + " rolled a random number from 1 to " + max + ": " + "<i>" + x + "</i>", false);
        }

        [ChatCommand("rules", "Displays the Server Rules")]
        public static void Rules(User user)
        {
            string x = IOUtils.ReadFileFromConfigFolder("rules.txt");
            user.Player.OpenInfoPanel("Rules", x.Autolink());
        }

        [ChatCommand("modkit", "Display information about the modkit")]
        public static void Modkit(User user)
        {
            string x = IOUtils.ReadFileFromConfigFolder("modkit.txt");
            user.Player.OpenInfoPanel("Modkit", x.Autolink());
        }

        [ChatCommand("changelog", "Displays the changelog of the modkit")]
        public static void Changelog(User user)
        {
            string x = IOUtils.ReadFileFromConfigFolder("changelog.txt");
            user.Player.OpenInfoPanel("Changelog", x.Autolink());
        }

        [ChatCommand("report", "Report a player or an issue")]
        public static void Report(User user, string part1, string part2 = "", string part3 = "", string part4 = "", string part5 = "", string part6 = "", string part7 = "", string part8 = "", string part9 = "", string part10 = "", string last = "")
        {
            if(last != "")
            {
                user.Player.SendTemporaryErrorAlreadyLocalized("Sorry, too many commas (this command only supports up to 9");
                return;
            }
            string singlestringreport = part1 + "," + part2 + "," + part3 + "," + part4 + "," + part5 + "," + part6 + "," + part7 + "," + part8 + "," + part9 + "," + part10;
            singlestringreport = singlestringreport.TrimEnd(',');
            string texttoadd = string.Empty;

            foreach (User usercheck in UserManager.OnlineUsers)
            {
                if (usercheck.IsAdmin) usercheck.Player.OpenCustomPanel("New Report from " + user.Name, TextLinkManager.MarkUpText(singlestringreport), DateTime.Now.Ticks.ToString());
            }

            texttoadd = TextLinkManager.MarkUpText("<b>" + user.Name + " " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ":</b>" + System.Environment.NewLine + singlestringreport + System.Environment.NewLine + System.Environment.NewLine);
            IOUtils.WriteFileToConfigFolder("../Reports/reports.txt", texttoadd);
            user.Player.SendTemporaryMessageAlreadyLocalized("Report sent");

        }

        [ChatCommand("houseranking", "Displays the users with the highest housing rates")]
        public static void HouseRanking(User user) //can still be optimized theres for example no need for the KeyValuePair<User, float>, a userlist is actually enough
        {

            int usercount = UserManager.Users.Count<User>();
            int max = 10;
            int i = 0;
            string output = string.Empty;
            List<KeyValuePair< User, float>> userlist = new List<KeyValuePair<User, float>>(usercount);
            User tmpuser;

            foreach (User userentry in UserManager.Users)
            {
                userlist.Add(new KeyValuePair<User,float>(userentry, (float)Math.Round(userentry.CachedHouseValue.HousingSkillRate,2)));
            }

            //userlist.Sort(new UserFloatComparer());
            userlist.Sort((KeyValuePair<User, float> x, KeyValuePair<User, float> y) => y.Value.CompareTo(x.Value));

            if (usercount < max) max = usercount;
            for(i=0; i<max; i++)
            {
                tmpuser = userlist[i].Key;
                //output = output + (i + 1).ToString() + ". <link=\"User:" + tmpuser.Name + "\"><style=\"Warning\">" + tmpuser.Name + "</style></link>: <link=\"CachedHouseValue:" + tmpuser.Name +"\"><style=\"Positive\">" + userlist[i].Value.ToString() + "</style></link> skill/day <br>";
                output = output + (i + 1) + ". " + tmpuser.UILink() + ": " + tmpuser.CachedHouseValue.UILink() + "<br>";
            }
            output = output + "<br> Your " + user.HouseValue + " (Rank " + (userlist.FindIndex(x => x.Key == user) + 1) + ")";

            user.Player.OpenInfoPanel("House Ranking", output);
        }

        [ChatCommand("nutritionranking", "Displays the users with the highest food skill rates")]
        public static void NutritionRanking(User user) //can still be optimized theres for example no need for the KeyValuePair<User, float>, a userlist is actually enough
        {

            int usercount = UserManager.Users.Count<User>();
            int max = 10;
            int i = 0;
            string output = string.Empty;
            List<KeyValuePair<User, float>> userlist = new List<KeyValuePair<User, float>>(usercount);
            User tmpuser;

            foreach (User userentry in UserManager.Users)
            {
                userlist.Add(new KeyValuePair<User, float>(userentry, userentry.Stomach.NutrientSkillRate));               
            }

            //userlist.Sort(new UserFloatComparer());
            userlist.Sort((KeyValuePair<User, float> x, KeyValuePair<User, float> y) => y.Value.CompareTo(x.Value));


            if (usercount < max) max = usercount;
            for (i = 0; i < max; i++)
            {
                tmpuser = userlist[i].Key;
                output = output + (i + 1) + ". " + tmpuser.UILink() + ": <color=green>" + Math.Round(userlist[i].Value, 2) + "</color><br>";
            }
            output = output + "<br> Your Foodskillrate: <color=green>" + user.Stomach.NutrientSkillRate + "</color> (Rank " + (userlist.FindIndex(x => x.Key == user)+1) + ")";
            user.Player.OpenInfoPanel("Nutrition Ranking", output);
        }

        [ChatCommand("avatar","Displays information about you or the given Player")]
        public static void Avatarcmd(User user, string playername = "")
        {
            Player targetplayer;
            User targetuser;
            if (playername == "")
            {
                targetplayer = user.Player;
                targetuser = user;
            }
            else
            {
                targetuser = UserManager.FindUserByName(playername);
                if (targetuser == null)
                {
                    user.Player.SendTemporaryErrorAlreadyLocalized("Player " + playername + " not found!");
                    return;
                }
                targetplayer = targetuser.Player;
            }

            string newline = "<br>";
            string title = "Stats for " + targetuser.Name ;
            string skillsheadline = "<b>SKILLRATES:</b>" + newline;
            string housinginfo;
            string foodinfo;
            string totalsp;
            string onlineinfo;
            string superskillsinfo= string.Empty;
            string professioninfo;
            string currencyinfo = "<b>CURRENCIES:</b>" + newline;
            string propertyinfo;
            string admininfo = string.Empty;


            if (targetuser.IsAdmin) admininfo = "<color=red><b>ADMIN</b></color> ";

            foreach(Currency currency in EconomyManager.Currency.Currencies)
            {
                if (currency.HasAccount(targetuser.Name))
                {
                    if (currency.GetAccount(targetuser.Name).Val > 0f)
                    {
                        currencyinfo += currency.UILink(currency.GetAccount(targetuser.Name).Val) + newline;
                    }
                }
            }


            List<Skill> superskills = SkillUtils.GetSuperSkills(targetuser);
            if(superskills.Count > 0)
            {
                superskillsinfo = "<b>SUPERSKILLS:</b>" + newline;
                foreach(Skill skill in superskills)
                {
                    superskillsinfo += skill.UILink() + newline;
                }
                superskillsinfo += newline;                
            }
            
            float foodsp = targetuser.Stomach.NutrientSkillRate;
            float housesp = targetuser.CachedHouseValue.HousingSkillRate;

            professioninfo = newline + "<b>PROFESSION:</b> " + newline + SkillUtils.FindProfession(targetuser).UILink() + newline;
            housinginfo = "House SP: " + targetuser.CachedHouseValue.UILink() + newline;
            foodinfo =  "Food SP: " + Math.Round(foodsp,2) + newline;
            totalsp = "Total SP: " + Math.Round(housesp + foodsp, 2) + newline;
            propertyinfo = "<b>PROPERTY:</b>" + newline + MiscUtils.CountPlots(targetuser) * 25 + " sqm of land." + newline;
            onlineinfo = targetuser.LoggedIn ? "is online. Located at " + new Vector3Tooltip(targetuser.Position).UILink() : "is offline. Last online " + TimeFormatter.FormatSpan(WorldTime.Seconds - targetuser.LogoutTime) + " ago";
            onlineinfo += newline;

            user.Player.OpenInfoPanel(title,admininfo + targetuser.UILink() + " " + onlineinfo + newline + skillsheadline + foodinfo + housinginfo + totalsp + professioninfo + newline + propertyinfo + newline + superskillsinfo + currencyinfo);
        }

        [ChatCommand("donate", "Donates Money to the Treasury")]
        public static void Donate(User user, float amount, string currencytype)
        {
            Currency currency = EconomyManager.Currency.GetCurrency(currencytype);
            if(currency==null)
            {
                user.Player.SendTemporaryErrorAlreadyLocalized("Currency " + currencytype + " not found.");
                return;
            }
           
            if (amount <= 0)
            {
                user.Player.SendTemporaryErrorLoc("Must specify an amount greater than 0.");
                return;
            }

            
            Result result = Legislation.Government.PayTax(currency, user, amount, "Voluntary Donation");
            if (result.Success)
            {
                user.Player.SendTemporaryMessageAlreadyLocalized("You donated " + currency.UILink(amount) + " to the Treasury. Thank you!");
            }
            else user.Player.SendTemporaryMessageAlreadyLocalized(result.Message);
        }

        [ChatCommand("superskillshelp", "Displays an infobox about Super Skills")]
        public static void SuperSkillhelp(User user)
        {
            SkillUtils.ShowSuperSkillInfo(user.Player);
        }

        #endregion
       
    }

}