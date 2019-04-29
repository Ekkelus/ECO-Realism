using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems.Chat;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared.Localization;
using Eco.Shared.Utils;
using System.Linq;
using Eco.Core.Utils;
using Eco.Mods.TechTree;
using Eco.Core.Utils.AtomicAction;
using System.Timers;
using REYmod.Config;
using Eco.Gameplay;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Components;
using Eco.Gameplay.DynamicValues;


// This file should contain only basic Utils wich are needed for all Modules, no module specific code here, only "interfaces" for events (see OneMinutetimer for example)


namespace REYmod.Utils
{
    /// <summary>
    /// This class contains various methods related to the ingame chat/textformatting
    /// </summary>
    public static class ChatUtils
    {
        public static void BroadcastPopup(string msg)
        {
            LocString locmsg = new LocString(msg);
            foreach (User user in UserManager.OnlineUsers)
            {
                if(user.Player != null) user.Player.PopupOKBoxLoc(locmsg);
            }

        }

        public static void SendMessage(User user, string msg, bool temporary = false, bool createlinks = true)
        {
            if(createlinks) ChatManager.ServerMessageToPlayer(Localizer.DoStr(msg.Autolink()), user, temporary);
            else ChatManager.ServerMessageToPlayer(Localizer.DoStr(msg), user, temporary);
        }

        public static void SendMessage(Player player, string msg, bool temporary = false, bool createlinks = true)
        {
            SendMessage(player.User, msg, temporary,createlinks);
        }

        public static void SendMessageToAll(string msg, bool tmp = false)
        {
            ChatManager.ServerMessageToAll(Localizer.DoStr(msg), tmp);
        }

        public static void ShowWelcomeMessage(User user)
        {
            string title = "Welcome";
            string content = IOUtils.ReadFileFromConfigFolder("welcomemessage.txt");
            user.Player.OpenInfoPanel(title, content.Autolink());
        }

        public static void ShowMOTD(User user)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// This class is responsible for the handling of Events
    /// </summary>
    public static class GlobalEvents
    {
        private static Timer timer = null;

        public static ThreadSafeAction OneMinuteEvent = new ThreadSafeAction();

        public static ThreadSafeAction<FoodItem, User> OnPlayerEatFood = new ThreadSafeAction<FoodItem, User>();

        public static void Initialize()
        {
            REYmodSettings.OnSendMessageToggleChange.Add(ServerGUIBroadcast);


            if (timer != null) timer.Dispose(); // dispose the old timer if theres already one
            timer = new Timer
            {
                AutoReset = true,
                Interval = 60000 // once per minute
            };
            timer.Elapsed += (x, y) => OneMinuteEvent.Invoke();
            timer.Start();
            UserManager.OnUserLoggedIn.Remove(OnUserLogin);
            UserManager.OnUserLoggedIn.Add(OnUserLogin);



        }


        private static void ServerGUIBroadcast()
        {
            string msgwithheader = "<b>Broadcast from Serverconsole</b><br><br>" + REYmodSettings.Obj.Config.ServerMessageSender;

            ChatUtils.BroadcastPopup(msgwithheader);
        }

        /// <summary>
        /// This method is called whenever a user logs in
        /// </summary>
        /// <param name="user">the logged in user</param>
        private static void OnUserLogin(User user) // Note: There's no "Player" at that point yet
        {
            bool firstlogin = false;
            if (user.LoginTime == default(int)) firstlogin = true;
            user.OnLogOut.Add(() => OnUserLogout(user)); //user logout is needed to unsubcribe from certain events when logged out (maybe not but i'll keep it here)
            if (!user.Stomach.OnEatFood.Any) user.Stomach.OnEatFood.Add(food => PlayerEatFood(food, user));
            user.OnEnterWorld.Add(() => OnUserEnterWorld(user, firstlogin));
            MiscUtils.UpdateUserStates();
        }

        /// <summary>
        /// This Method is called when a player Login is completed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="firstlogin"></param>
        private static void OnUserEnterWorld(User user, bool firstlogin) // Note: "Player" should be ready here
        {
            //Console.WriteLine(user.Name + " entered world");
            if (firstlogin && REYmodSettings.Obj.Config.Showwelcomemessage) ChatUtils.ShowWelcomeMessage(user);
            user.OnEnterWorld.Clear();
        }

        /// <summary>
        /// This method is called whenever a player eats somethin
        /// </summary>
        /// <param name="food">the eaten fooditem</param>
        /// <param name="user">the player who ate</param>
        private static void PlayerEatFood(FoodItem food, User user)
        {
            OnPlayerEatFood.Invoke(food, user);
            //ChatManager.ServerMessageToAllAlreadyLocalized(user.UILink() + " just ate " + food.UILink(), false);             

            
        }

        private static void OnUserLogout(User user)
        {
            user.Stomach.OnEatFood.Clear();

        }
    }

    /// <summary>
    /// This class contains methods for File operations
    /// </summary>
    public static class IOUtils
    {
        public static void ClearFile(string path)
        {
            if (File.Exists(path)) File.Delete(path);
            File.Create(path).Close();
        }

        public static string ReadFileFromConfigFolder(string filename)
        {
            string content = string.Empty;
            string path = REYmodSettings.Obj.Config.Configfolderpath + filename;
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else return "Error reading file: File does not exist!";
        }

        public static void WriteToFile(string path, string text)
        {
            string dirpath = path.Substring(0, path.LastIndexOf('/') + 1);
            if (!Directory.Exists(dirpath)) Directory.CreateDirectory(dirpath);
            if (!File.Exists(path)) File.Create(path).Close();
            File.AppendAllText(path, text);
        }

        public static void WriteFileToConfigFolder(string filename, string text)
        {
            string fullfilename = REYmodSettings.Obj.Config.Configfolderpath + filename;
            string dirpath = fullfilename.Substring(0, fullfilename.LastIndexOf('/') + 1);          
            if (!Directory.Exists(dirpath)) Directory.CreateDirectory(dirpath);
            if (!File.Exists(fullfilename)) File.Create(fullfilename).Close();
            File.AppendAllText(fullfilename, text);
        }

        public static void WriteToLog(string logdata, string desc = "")
        {
            string path = "./Dump/log.txt";
            logdata = Environment.NewLine + Environment.NewLine + desc + Environment.NewLine + logdata;
            if (!File.Exists(path)) File.Create(path).Close();

            File.AppendAllText(path, logdata);

        }


        public static void WriteCommandLog(User user,string command, string desc = "")
        {
            string path = "./Mods/REYmod/commandlog.csv";
            string logdata = String.Join(";", DateTime.Now.ToShortDateString(),DateTime.Now.ToLongTimeString(), user.Name, command, desc, Environment.NewLine);
            if (!File.Exists(path)) File.Create(path).Close();

            File.AppendAllText(path, logdata);

        }
    }

    /// <summary>
    /// This class contains utility methods, that didn't really fit somewhere else
    /// </summary>
    public static class MiscUtils
    {

        public static string GetHouseRanking(int amount = 10)
        {
            int usercount = UserManager.Users.Count<User>();
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

            if (usercount < amount) amount = usercount;
            for (i = 0; i < amount; i++)
            {
                tmpuser = userlist[i].Key;
                //output = output + (i + 1).ToString() + ". <link=\"User:" + tmpuser.Name + "\"><style=\"Warning\">" + tmpuser.Name + "</style></link>: <link=\"CachedHouseValue:" + tmpuser.Name +"\"><style=\"Positive\">" + userlist[i].Value.ToString() + "</style></link> skill/day <br>";
                output = output + (i + 1) + ". " + tmpuser.UILink() + ": " + tmpuser.CachedHouseValue.UILink() + "<br>";
            }

            return output;
        }


        /// <summary>
        /// Returns the number of plots a <see cref="User"/> owns.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int CountPlots(User user)
        {
            //*******************************
            //return 0; // currently disabled due to deedchanges
            //*********************************
            return PropertyManager.PropertyForUser(user).Count();


            /*
            IEnumerable<Vector2i> positions;
            int totalplotcount = 0;

            if (user == null) return -1;
            IEnumerable<AuthorizationController> authorizationControllers = PropertyManager.GetAuthBelongingTo(user);
            foreach (AuthorizationController auth in authorizationControllers)
            {
                if (auth.Type == "Property")
                {
                    positions = PropertyManager.PositionsForId(auth.Id);
                    foreach (Vector2i pos in positions) totalplotcount++;
                }
            }
            return totalplotcount;
            */
        }
        
        /// <summary>
        /// Unclaims all property of a given user
        /// </summary>
        /// <param name="target"></param>
        /// <param name="feedbackuser"></param>
        public static void UnclaimUser(User target, User feedbackuser)
        {
            // int emptystacks = 0; 
            IEnumerable<Deed> allDeeds = PropertyManager.GetAllDeeds();
            IEnumerable<Deed> targetDeeds = allDeeds.Where(x => x.OwnerUser == target);

            bool forceunclaim = false;
            string force = "";
            foreach (Deed auth in targetDeeds)
            {

                Result res = PropertyManager.TryRemoveDeed(auth);
                if (!res.Success && feedbackuser != null) ChatUtils.SendMessage(feedbackuser, res.Message);
                if (!res.Success) forceunclaim = true;
            }
            if (forceunclaim)
            {
                PropertyManager.PropertyForUser(target).ForEach(x => PropertyManager.ForceUnclaim(x.Position));
                force = ". Had to use Forceunclaim-Workaround!";
            }
            if (feedbackuser != null) feedbackuser.Player.OpenInfoPanel("Unclaim Player" ,"Unclaimed all property of " + target.Name + force);
        }


        /// <summary>
        /// Sets some basic Userstates if they are not set yet
        /// </summary>
        public static void UpdateUserStates()
        {
            UserManager.Users.ForEach(user =>
            {
                if (!user.HasState("Moderator")) user.SetState("Moderator", false);
                if (!user.HasState("CustomTitle")) user.SetState("CustomTitle", "");
                else return;
            });
        }

        public static void SoftPassLaw(Law law, User user)
        {
            if (user != Legislation.Government.LeaderUser)
            {
                user.Player.OpenInfoPanel("Failed!", "You are not the current leader");
                return;
            }
            if (law.VotedYes.Count < 5)
            {
                user.Player.OpenInfoPanel("Failed!", "Not enough Yes-Votes (" + law.VotedYes.Count + "/5)");
                return;
            }
            if (law.VotedNo.Count > 0)
            {
                user.Player.OpenInfoPanel("Failed!", "Votes are not unanimous! (" +law.VotedNo.Count + " No-Votes)");
                return;
            }
            law.Enact();
            user.Player.OpenInfoPanel("Success!", "Law " + law.Title + " has been passed!");
        }
    }

    /// <summary>
    /// This class contains various methods related to Skills
    /// </summary>
    public static class SkillUtils
    {
        /// <summary>
        /// A List of the users who entered the /superskillconfirmed command
        /// </summary>
        public static List<int> superskillconfirmed;

        /// <summary>
        /// Tries to determine the Profession of the given user
        /// <para/> It sums up all spent points in a given Profession (without Specialty costs as those vary depending on when you learned them).
        /// <para/> Specialtys will be taken into account with a fix value of 100 spent SP.
        /// <para/> Returns the Profession with the most spent points.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Skill FindProfession(User user)
        {
            if (user == null) return new SurvivalistSkill();
            SkillTree profession = new SurvivalistSkill().RootSkillTree;
            int maxpoints = 0;
            Dictionary<SkillTree, int> rootskills = new Dictionary<SkillTree, int>();

            foreach (SkillTree skilltree in SkillTree.RootSkillTrees)
            {
                rootskills.Add(skilltree, 0);
            }


            Skill[] skills = user.Skillset.Skills;



            foreach (Skill skill in skills)
            {
                if (skill.IsSpecialty)
                {
                    if (skill.Level > 0) rootskills[skill.RootSkillTree] += 100;
                }
                else rootskills[skill.RootSkillTree] += skill.TotalPointSpent;
            }

            foreach (KeyValuePair<SkillTree, int> pair in rootskills)
            {
                if (pair.Value > maxpoints)
                {
                    profession = pair.Key;
                    maxpoints = pair.Value;
                }
                //ChatManager.ServerMessageToAllAlreadyLocalized(pair.Key.StaticSkill.UILink() + " " + pair.Value, false);
            }

            return profession.StaticSkill;


        }

        /// <summary>
        /// Returns a list of all Superskills the given User has
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static List<Skill> GetSuperSkills(User user)
        {
            List<Skill> superskills = new List<Skill>();

            foreach (Skill skill in user.Skillset.Skills)
            {
                if (skill.Level > 5)
                {
                    superskills.Add(skill);
                }
            }
            return superskills;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Initialize()
        {
            superskillconfirmed = new List<int>();
        }

        /// <summary>
        /// Opens the Infobox about Superskills for the given <see cref="Player"/>
        /// </summary>
        /// <param name="player"></param>
        public static void ShowSuperSkillInfo(Player player)
        {
            string confirmation = "You already unlocked your next superskill";
            if (!CheckSuperskillConfirmation(player.User))
            {                 
                confirmation = "To confirm that you understood Super Skills and to unlock them please click " + new Button(x => ConfirmSuperskill(x.User),clickdesc:"Click to unlock", text: "HERE".Color("green"), singleuse: true).UILink();
            }
            player.OpenInfoPanel("Super Skills", "Current amount of Super Skills: <b><color=green>" + SuperSkillCount(player.User) + "</color></b><br>Max amount of Super Skills: <b><color=green>" + ((REYmodSettings.Obj.Config.Maxsuperskills != int.MaxValue) ? REYmodSettings.Obj.Config.Maxsuperskills.ToString() : "Infinite") + "</color></b><br><br>Super Skills are Skills that can be leveled all the way up to level 10.<br><br><color=red>You can only have a limited amount of them.</color><br><br>" + confirmation);
        }

        public static void ConfirmSuperskill(User user)
        {
            if (CheckSuperskillConfirmation(user)) return;           
            if (user.Player != null) user.Player.OpenInfoPanel("Superskill Unlocked!", "You can now level up a Skill to level 10");
            superskillconfirmed.Add(user.Id);
        }

        /// <summary>
        /// Returns the amount of Superskills the given User has
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int SuperSkillCount(User user)
        {
            int x = 0;
            foreach (Skill skill in user.Skillset.Skills)
            {
                if (skill.Level > 5) x++;
            }
            return x;
        }

        /// <summary>
        /// This Method should override the CreateLevelUpAction() Method of Skills that can be superskilled.
        /// Just place the Example code in the code of superskillable Skills.
        /// </summary>
        /// <example>
        /// <code>
        ///public override IAtomicAction CreateLevelUpAction(Player player)
        ///{
        ///    return SkillUtils.SuperSkillLevelUp(this, player);
        ///}
        /// </code>
        /// </example>
        /// <seealso cref="Skill.CreateLevelUpAction(Player)"/>
        /// <param name="skill"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public static IAtomicAction SuperSkillLevelUp(Skill skill, Player player)
        {
            if (skill.Level != 5) return SimpleAtomicAction.NoOp;
            if (SuperSkillCount(player.User) >= REYmodSettings.Obj.Config.Maxsuperskills) return new FailedAtomicAction(Localizer.DoStr("You already have enough SuperSkills " + SuperSkillCount(player.User) + "/" + REYmodSettings.Obj.Config.Maxsuperskills));
            if (CheckSuperskillConfirmation(player.User))
            {
                superskillconfirmed.Remove(player.User.Id);
                return SimpleAtomicAction.NoOp;
            }
            ShowSuperSkillInfo(player);
            return new FailedAtomicAction(Localizer.DoStr("You need to confirm first"));
        }

        public static bool CheckSuperskillConfirmation(User user)
        {
            foreach (int id in superskillconfirmed)
            {
                if (id == user.Id)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the given User has the given <see cref="Skill"/> at the given Level or above
        /// </summary>
        /// <param name="user">the user that should be checked</param>
        /// <param name="skilltype">the Type of the <see cref="Skill"/> that should be checked</param>
        /// <param name="lvl">the required level</param>
        /// <returns><c>True</c> when the player has the skill
        /// <c>False</c> if not</returns>
        public static bool UserHasSkill(User user, Type skilltype, int lvl)
        {

            foreach (Skill skill in user.Skillset.Skills)
            {
                if (skill.Type == skilltype)
                {
                    if (skill.Level >= lvl)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    /// <summary>
    /// This class is used to store various temporary values
    /// </summary>
    public static class UtilsClipboard
    {
        public static Dictionary<User, Tuple<User, int>> UnclaimSelector;

        public static void Initialize()
        {
            UnclaimSelector = new Dictionary<User, Tuple<User, int>>();

        }

    }

  
    public class AllergyIgnoreAttribute : ItemAttribute { } // This has to stay in Core because the attribute is added to Existing Items

    [Category("Hidden")]
    public class UtilsInitItem : Item
    {
        static UtilsInitItem()
        {
            SkillUtils.Initialize();
            UtilsClipboard.Initialize();
            GlobalEvents.Initialize();
            //CustomWorldGen.Initialize();
        }
    }

    /// <summary>
    /// Some extensions for existing classes.
    /// </summary>
    public static partial class CustomClassExtensions
    {
        #region String
        public static string Color(this string x, string color)
        {
            return "<color=" + color + ">" + x + "</color>";
        }

        public static string Bold(this string x)
        {
            return Text.Bold(x);
        }

        public static string Italics(this string x)
        {
            return Text.Italics(x);
        }

        public static string Autolink(this string x)
        {
            return TextLinkManager.MarkUpText(x);
        }
        #endregion

        #region Item
        public static bool IsLiquid(this Item x)
        {
            return ItemAttribute.Has<LiquidAttribute>(x.Type);
        }

        public static bool HasIngredient(this Item item, Type ingredienttype, HashSet<Item> checkeditems = null, bool allergyfix = true) //stays in core as it can also be used for othere things other than allergies, also the AllergyIgnoreAttribute exists in core so theres no problem
        {
            if (checkeditems == null) checkeditems = new HashSet<Item>();
            if (checkeditems.Contains(item)) return false;
            checkeditems.Add(item);
            //Console.WriteLine(checkeditems.Count);
            if (allergyfix && ItemAttribute.Has<AllergyIgnoreAttribute>(item.Type)) return false;


            if (item.Type == ingredienttype) return true;
            IEnumerable<Recipe> recipes = Recipe.GetRecipesForItem(item.Type);
            recipes = recipes.Where(y =>
            {
                IEnumerable<CraftingElement> recipeingredients = y.Ingredients.Where(z =>
                 {
                     return z.Item.HasIngredient(ingredienttype, checkeditems, allergyfix);
                 });
                if (recipeingredients.Count() != 0) return true;
                else return false;
            });
            if (recipes.Count() != 0)
            {
                //recipes.ForEach(x =>ChatUtils.SendMessageToAll(x.FriendlyName, false));
                return true;
            }
            else return false;
        }
        #endregion

        #region User
        [Tooltip(1)]
        public static string AdminTooltip(this User user)
        {
            string roles = "";
            if (user.IsAdmin) roles += "<b><color=red>ADMIN</b></color> ";
            if (user.GetState<bool>("Moderator") == true) roles += "<b><color=blue>Moderator</b></color> ";
            if (Legislation.Government.IsLeader(user)) roles += "<b><color=orange>World Leader</b></color> ";
            return roles;
        }
        [Tooltip(2)]
        public static string CustomTitle(this User user)
        {
            string title = user.GetState<string>("CustomTitle");
            return title;
        }
        #endregion

        #region Player
        public static void SendTemporaryMessageAlreadyLocalized(this Player player,string msg)
        {
            player.SendTemporaryMessage(Localizer.DoStr(msg));
        }
        public static void SendTemporaryErrorAlreadyLocalized(this Player player, string msg)
        {
            player.SendTemporaryError(Localizer.DoStr(msg));
            
        }
        #endregion


        #region CustomTextComponent
        public static void UpdateDynamicText(this CustomTextComponent comp)
        {
            if (comp.Text.Contains("[TIME]"))
                {
                     comp.SetText(comp.Parent.OwnerUser.Player, "<size=0>[TIME]</size>" + DateTime.Now.ToString("hh:mm:ss"));
                }
            else if (comp.Text.Contains("[HOUSERANK]"))
            {
                comp.SetText(comp.Parent.OwnerUser.Player, "<size=0>[HOUSERANK]</size>" + MiscUtils.GetHouseRanking(10));
            }
        }
        #endregion

        #region ModificationStrategy
        public static ModificationStrategy Inverted(this ModificationStrategy x)
        {
            if (x is MultiplicativeStrategy)
            {

                List<float> newfactorslist = new List<float>();
                foreach (float factor in (x as MultiplicativeStrategy).Factors)
                {
                    if (factor != 0) newfactorslist.Add(1 / factor);
                }

                return new MultiplicativeStrategy(newfactorslist.ToArray());
            }
            if (x is AdditiveStrategy)
            {
                List<float> subtractions = new List<float>();
                foreach (float addition in (x as AdditiveStrategy).Additions)
                {
                    subtractions.Add(-addition);
                }

                return new AdditiveStrategy(subtractions.ToArray());
            }

            return x;
        }
        #endregion

    }


    #region Button
    public class Button : ILinkable
    {
        private string clickdesc;
        private string tooltip;
        private string linkcontent;
        private Action<Player> linkclickaction;
        public int ID;
        public bool temporary;

        public static HashSet<Button> buttonList = new HashSet<Button>();
        [TooltipTitle]
        public string TooltipTitle { get; set; }

        [Tooltip(100)]
        public string Tooltip()
        {
            return tooltip;
        }

        public Button(Action<Player> onClick = null, string tooltiptitle = null, string tooltip = null, string text = null, bool singleuse = false,string clickdesc = null)
        {
            if (buttonList.Count > 10000000) throw new OverflowException("Too many undisposed Buttons"); // Exception when there are more than 10mil buttons, thats hopefully never the case
            linkclickaction = onClick;
            TooltipTitle = tooltiptitle;
            this.tooltip = tooltip;
            linkcontent = text;
            temporary = singleuse;
            this.clickdesc = clickdesc;
            int newid = RandomUtil.Range(100000000, 999999999);
            while (buttonList.Any(x => x.ID == newid))
            {
                newid = RandomUtil.Range(100000000, 999999999);
            }
            buttonList.Add(this);
            ID = newid;
        }

        public LocString LinkClickedTooltipContent(Player clickingPlayer)
        {
            return new LocString(clickdesc);
        }

        public void OnLinkClicked(Player clickingPlayer)
        {
            if (linkclickaction == null) return;
            linkclickaction(clickingPlayer);
            //if (temporary) this.Dispose();
            //clickingPlayer.OpenInfoPanel("Test", "It worked!");
        }

        public LocString UILinkContent()
        {
            return new LocString(linkcontent);
        }
    }


    public class ButtonIdTranslator : ObjectLinkIdTranslator<Button>
    {
        protected override string GetTypedLinkId(Button linkTarget)
        {
            return linkTarget.ID.ToString();//linkTarget.Guid.ToString();
        }
        protected override Button GetTypedLinkTarget(string linkId)
        {
            int linkIdInt = Convert.ToInt32(linkId);
            return Button.buttonList.FirstOrDefault(x => x.ID == linkIdInt);
        }
    }

    #endregion

    
}
