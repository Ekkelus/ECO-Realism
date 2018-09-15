using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Gameplay.Rooms;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems.Chat;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Shared.Math;
using Eco.Shared.Networking;
using Eco.Shared.Utils;
using Eco.Simulation.Time;
using REYmod.Config;
using REYmod.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace REYmod.Core.ChatCommands
{
    public class ChatCommands : IChatCommandHandler
    {
        #region ADMIN Commands

        #region  Custom Userranks/privileges/titles
        // configfile/entry with an update command would be better than using Userstates i guess!
        [ChatCommand("setmod", "Promotes a user to Moderator", level: ChatAuthorizationLevel.Admin)]
        public static void Setmod(User user, User target)
        {
            target.SetState("Moderator", true);
            MiscUtils.UpdateUserStates();
        }

        [ChatCommand("removemod", "Removes moderator privileges from a user", level: ChatAuthorizationLevel.Admin)]
        public static void Removemod(User user, User target)
        {
            target.SetState("Moderator", false);
            MiscUtils.UpdateUserStates();
        }

        [ChatCommand("settitle", "Sets the custom title of the given user", level: ChatAuthorizationLevel.Admin)]
        public static void Removemod(User user, User target, string title)
        {
            target.SetState("CustomTitle", title);
            ChatUtils.SendMessage(user, "Set " + target.Name + "'s title to: \"" + title + "\"");
        }
        #endregion


        [ChatCommand("setowner", "Sets tho owner of the Plot your currently standing on", level: ChatAuthorizationLevel.Admin)]
        public static void SetOwner(User user, string newowner)
        {
            User newowneruser = UserManager.FindUserByName(newowner);
            Deed deed = PropertyManager.GetDeed(user.Position.XZi);
            if (deed == null)
            {
                ChatUtils.SendMessage(user, "Plot is not claimed!");
                return;
            }
            if (newowneruser != null)
            {
                deed.SetOwner(newowneruser);
                ChatUtils.SendMessage(user, newowneruser + " is now the owner of " + deed.Name);
            }
            else ChatUtils.SendMessage(user, "User not found!", true);
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

        #endregion ADMIN Commands

        #region MOD Commands
        #region Copies of original Commands
        [ChatCommand("MOD/ADMIN only! - Toggles fly mode", ChatAuthorizationLevel.User)]
        public static void MFly(User user)
        {
            if (!user.IsAdmin && !user.GetState<bool>("Moderator"))//admin/mod only
            {
                ChatUtils.SendMessage(user, "Not Authorized to use this command!");
                return;
            }
            IOUtils.WriteCommandLog(user, "mFly");
            user.Player.RPC("ToggleFly");
        }

        [ChatCommand("MOD/ADMIN only! - Kicks user", ChatAuthorizationLevel.User)]
        public static void MKick(User user, User kickUser, string reason = "")
        {
            if (!user.IsAdmin && !user.GetState<bool>("Moderator"))//admin/mod only
            {
                ChatUtils.SendMessage(user, "Not Authorized to use this command!");
                return;
            }
            IOUtils.WriteCommandLog(user, "mKick", "Kicked " + kickUser.Name);
            var player = kickUser.Player;
            if (player != null)
            {
                player.Client.Disconnect("Moderator "+ Text.Bold(user.Name) + " has kicked you.", reason);
                ChatUtils.SendMessage(user,"You have kicked " + kickUser.Name);
            }
            else
                ChatUtils.SendMessage(user, kickUser.Name + " is not online");
        }

        #endregion


        [ChatCommand("unclaimuser", "MOD/ADMIN only! - Unclaims all property of the given user ", level: ChatAuthorizationLevel.User)]
        public static void UnclaimPlayer(User user, User owner = null)
        {
            bool inactive = true;
            double inactivetime;

            if (!user.IsAdmin && !user.GetState<bool>("Moderator"))//admin/mod only
            {
                ChatUtils.SendMessage(user, "Not Authorized to use this command!");
                return;
            }

            if (owner == null)
            {
                if (PropertyManager.GetPlot(user.Position.XZi) != null) owner = PropertyManager.GetPlot(user.Position.XZi).Owner;
            }

            if (owner == null)
            {
                user.Player.SendTemporaryMessageAlreadyLocalized("Plot not owned");
                return;
            }

            inactivetime = (WorldTime.Seconds - owner.LogoutTime);
            if (inactivetime < REYmodSettings.Obj.Config.Maxinactivetime * 3600) inactive = false;
            if (owner.LoggedIn) inactivetime = 0;

            IEnumerable<Deed> allDeeds = PropertyManager.GetAllDeeds();
            IEnumerable<Deed> targetDeeds = allDeeds.Where(x => x.OwnerUser.User == owner);
            int ownedplots = PropertyManager.PropertyForUser(owner).Count();
            int ownedvehicles = targetDeeds.Sum(x => x.OwnedObjects.Count) - ownedplots;

            string textbox = "";
            textbox += "You are going to unclaim all property of " + owner.UILink() + "<br>";
            textbox += "Owned Plots: " + ownedplots + "<br>";
            textbox += "Owned Vehicles: " + ownedvehicles + "<br>";
            textbox += "<br>Player offline for " + TimeFormatter.FormatSpan(inactivetime);
            if (!inactive) textbox += "<br>WARNING! Player not inactive!".Color("red");
            textbox += "<br><br>";
            if (!inactive && !user.IsAdmin)
            {
                textbox += "You can't unclaim this player! Not inactive for long enough.";
            }
            else
            {
                textbox += new Button(x => { MiscUtils.UnclaimUser(owner, user); IOUtils.WriteCommandLog(user, "UnclaimUser", "Unclaimed " + owner.Name + " (" + ownedplots + " plots/" + ownedvehicles + " vehicles)" + "Inactive for " + TimeFormatter.FormatSpan(inactivetime)); }, "", "Click here to unclaim all property of " + owner.UILink(), "Confirm Unclaiming".Color("green")).UILink();
            }

            user.Player.OpenInfoPanel("Unclaim Player", textbox);

        }

        [ChatCommand("tp", "MOD/ADMIN only! - Opens a list of online players. Click the player you(or the given player) should be teleported to", level: ChatAuthorizationLevel.User)]
        public static void Tp(User user, string username = "")
        {
            if (!user.IsAdmin && !user.GetState<bool>("Moderator"))//admin/mod only
            {
                ChatUtils.SendMessage(user, "Not Authorized to use this command!");
                return;
            }
            User usertoteleport = UserManager.FindUserByName(username);
            if (usertoteleport == null) usertoteleport = user;

            string panelcontent = "Select Player: <br><br>";
            foreach (User onlineuser in UserManager.OnlineUsers)
            {
                panelcontent += new Button(player => { usertoteleport.Player.SetPosition(onlineuser.Player.Position); IOUtils.WriteCommandLog(user, "Tp", usertoteleport.Name + " -> " + onlineuser.Name); }, content: onlineuser.Name, singleuse: true, clickdesc: "Click to teleport " + usertoteleport.Name + " to " + onlineuser.Name).UILink();
                panelcontent += "<br>";
            }
            user.Player.OpenInfoPanel("Teleporting " + usertoteleport.Name, panelcontent);
        }

        [ChatCommand("globalroomfix", "MOD/ADMIN only! - Reevaluates all rooms that have at least one worldobject placed in them", level: ChatAuthorizationLevel.User)]
        public static void GlobalRoomFix(User user)
        {
            if (!user.IsAdmin && !user.GetState<bool>("Moderator"))//admin/mod only
            {
                ChatUtils.SendMessage(user, "Not Authorized to use this command!");
                return;
            }

            IOUtils.WriteCommandLog(user, "Roomfix");
            foreach (WorldObject obj in WorldObjectManager.All)
            {
                RoomData.QueueRoomTest(obj.Position3i);
                //Console.WriteLine("Checked " + obj.Name + " at " + obj.Position3i.ToStringLabelled("pos"));
            }
            RoomData.Obj.UpdateRooms();
            ChatUtils.SendMessage(user, "Rooms should be fixed now");
        }

        [ChatCommand("openauth", "MOD/ADMIN only! - Opens the authmenu for the Plot your currently standing on, only to check settings, only owner can change", level: ChatAuthorizationLevel.User)]
        public static void OpenAuth(User user)
        {
            if (!user.IsAdmin && !user.GetState<bool>("Moderator"))//admin/mod only
            {
                ChatUtils.SendMessage(user, "Not Authorized to use this command!");
                return;
            }
            Deed deed = PropertyManager.GetDeed(user.Position.XZi);
            if (deed != null) deed.OpenAuthorizationMenuOn(user.Player);
            else ChatUtils.SendMessage(user, "Plot is not claimed!", true);
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
            if (last != "")
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
            List<KeyValuePair<User, float>> userlist = new List<KeyValuePair<User, float>>(usercount);
            User tmpuser;

            foreach (User userentry in UserManager.Users)
            {
                userlist.Add(new KeyValuePair<User, float>(userentry, (float)Math.Round(userentry.CachedHouseValue.HousingSkillRate, 2)));
            }

            //userlist.Sort(new UserFloatComparer());
            userlist.Sort((KeyValuePair<User, float> x, KeyValuePair<User, float> y) => y.Value.CompareTo(x.Value));

            if (usercount < max) max = usercount;
            for (i = 0; i < max; i++)
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
            output = output + "<br> Your Foodskillrate: <color=green>" + user.Stomach.NutrientSkillRate + "</color> (Rank " + (userlist.FindIndex(x => x.Key == user) + 1) + ")";
            user.Player.OpenInfoPanel("Nutrition Ranking", output);
        }

        [ChatCommand("avatar", "Displays information about you or the given Player")]
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
            string title = "Stats for " + targetuser.Name;
            string skillsheadline = "<b>SKILLRATES:</b>" + newline;
            string housinginfo;
            string foodinfo;
            string totalsp;
            string onlineinfo;
            string superskillsinfo = string.Empty;
            string professioninfo;
            string currencyinfo = "<b>CURRENCIES:</b>" + newline;
            string propertyinfo;
            string admininfo = string.Empty;

            if (targetuser.IsAdmin) admininfo = "<color=red><b>ADMIN</b></color> ";

            foreach (Currency currency in EconomyManager.Currency.Currencies)
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
            if (superskills.Count > 0)
            {
                superskillsinfo = "<b>SUPERSKILLS:</b>" + newline;
                foreach (Skill skill in superskills)
                {
                    superskillsinfo += skill.UILink() + newline;
                }
                superskillsinfo += newline;
            }

            float foodsp = targetuser.Stomach.NutrientSkillRate;
            float housesp = targetuser.CachedHouseValue.HousingSkillRate;

            professioninfo = newline + "<b>PROFESSION:</b> " + newline + SkillUtils.FindProfession(targetuser).UILink() + newline;
            housinginfo = "House SP: " + targetuser.CachedHouseValue.UILink() + newline;
            foodinfo = "Food SP: " + Math.Round(foodsp, 2) + newline;
            totalsp = "Total SP: " + Math.Round(housesp + foodsp, 2) + newline;
            propertyinfo = "<b>PROPERTY:</b>" + newline + MiscUtils.CountPlots(targetuser) * 25 + " sqm of land." + newline;
            onlineinfo = targetuser.LoggedIn ? "is online. Located at " + new Vector3Tooltip(targetuser.Position).UILink() : "is offline. Last online " + TimeFormatter.FormatSpan(WorldTime.Seconds - targetuser.LogoutTime) + " ago";
            onlineinfo += newline;

            user.Player.OpenInfoPanel(title, admininfo + targetuser.UILink() + " " + onlineinfo + newline + skillsheadline + foodinfo + housinginfo + totalsp + professioninfo + newline + propertyinfo + newline + superskillsinfo + currencyinfo);
        }

        [ChatCommand("donate", "Donates Money to the Treasury")]
        public static void Donate(User user, float amount, string currencytype)
        {
            Currency currency = EconomyManager.Currency.GetCurrency(currencytype);
            if (currency == null)
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

        #endregion USER Commands
    }
}
 