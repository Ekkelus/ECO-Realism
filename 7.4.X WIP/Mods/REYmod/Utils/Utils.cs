using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Minimap;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems.Chat;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Gameplay.Pipes.LiquidComponents;
using Eco.Gameplay.Pipes.Gases;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using Eco.Shared.View;
using Eco.Shared.Items;
using Eco.Gameplay.Pipes;
using Eco.World.Blocks;
using REYmod.Utils;
using Eco.Shared.Localization;
using System.Linq;
using Eco.Core.Utils;
using Eco.Gameplay.Stats;
using Eco.Mods.TechTree;
using Eco.Core.Utils.AtomicAction;
using System.Timers;

namespace REYmod.Utils
{
    /// <summary>
    /// This class contains various methods related to the ingame chat/textformatting
    /// </summary>
    public static class ChatUtils
    {

        public static void SendMessage(User user, string msg)
        {
            ChatManager.ServerMessageToPlayerAlreadyLocalized(msg.Autolink(), user, false);
        }

        public static void SendMessage(Player player, string msg)
        {
            SendMessage(player.User, msg);
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
    /// currently serves no purpose
    /// </summary>
    public static class GlobalEvents
    {
        private static Timer timer = null;

        public static void Initialize()
        {
            if (timer != null) timer.Dispose(); // dispose the old timer if theres already one
            timer = new Timer
            {
                AutoReset = true,
                Interval = 60000 // once per minute
            };
            timer.Elapsed += MinuteTimerElapsed;
            timer.Start();
            UserManager.OnUserLoggedIn.Remove(OnUserLogin);
            UserManager.OnUserLoggedIn.Add(OnUserLogin);



        }
        /// <summary>
        /// This method will automatically be called every minute
        /// <para/> Could be useful
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MinuteTimerElapsed(object sender, ElapsedEventArgs e)
        {
            //ChatManager.ServerMessageToAllAlreadyLocalized("Another minute passed! Current time: " + DateTime.Now.ToShortTimeString(),false);
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
            if(!user.Stomach.OnEatFood.Any) user.Stomach.OnEatFood.Add(food => PlayerEatFood(food, user));
            user.OnEnterWorld.Add(() => OnUserEnterWorld(user,firstlogin));
        }

        /// <summary>
        /// This Method is called when a player Login is completed
        /// </summary>
        /// <param name="user"></param>
        private static void OnUserEnterWorld(User user,bool firstlogin)
        {
            //Console.WriteLine(user.Name + " entered world");
            if (firstlogin && REYconfig.showwelcomemessage) ChatUtils.ShowWelcomeMessage(user);
            user.OnEnterWorld.Clear();
        }

        /// <summary>
        /// This method is called whenever a player eats somethin
        /// </summary>
        /// <param name="food">the eaten fooditem</param>
        /// <param name="user">the player who ate</param>
        private static void PlayerEatFood(FoodItem food, User user)
        {
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
            string path = ConfigHandler.configfolderpath + filename;
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

        public static void WriteToLog(string logdata, string desc = "")
        {
            string path = "./Dump/log.txt";
            logdata = Environment.NewLine + System.Environment.NewLine + desc + System.Environment.NewLine + logdata;
            if (!File.Exists(path)) File.Create(path).Close();

            File.AppendAllText(path, logdata);

        }
    }

    /// <summary>
    /// This class contains utility methods, that didn't really fit somewhere else
    /// </summary>
    public static class MiscUtils
    {
        /// <summary>
        /// Returns the number of plots a <see cref="User"/> owns.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int CountPlots(User user)
        {
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
        public static List<string> superskillconfirmed;

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
            superskillconfirmed = new List<string>();
        }

        /// <summary>
        /// Opens the Infobox about Superskills for the given <see cref="Player"/>
        /// </summary>
        /// <param name="player"></param>
        public static void ShowSuperSkillInfo(Player player)
        {
            player.OpenInfoPanel("Super Skills", "Current amount of Super Skills: <b><color=green>" + SkillUtils.SuperSkillCount(player.User) + "</color></b><br>Max amount of Super Skills: <b><color=green>" + ((REYconfig.maxsuperskills != int.MaxValue) ? REYconfig.maxsuperskills.ToString() : "Infinite") + "</color></b><br><br>Super Skills are Skills that can be leveled all the way up to level 10.<br><br><color=red>You can only have a limited amount of them.</color><br><br>To confirm that you understood Super Skills and to unlock them please enter <b><color=green>/confirmsuperskill</b></color>");
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
            if (SkillUtils.SuperSkillCount(player.User) >= REYconfig.maxsuperskills) return new FailedAtomicAction(Localizer.Do("You already have enough SuperSkills " + SkillUtils.SuperSkillCount(player.User) + "/" + REYconfig.maxsuperskills));
            foreach (string id in SkillUtils.superskillconfirmed)
            {
                if (id == player.User.ID)
                {
                    SkillUtils.superskillconfirmed.Remove(id);
                    return SimpleAtomicAction.NoOp;
                }
            }
            SkillUtils.ShowSuperSkillInfo(player);
            return new FailedAtomicAction(Localizer.Do("You need to confirm first"));
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
        public static Dictionary<User, Tuple<User,int>> UnclaimSelector;

        public static void Initialize()
        {
            UnclaimSelector = new Dictionary<User, Tuple<User,int>>();

        }

    }

    /// <summary>
    /// Custom Comparer (currently still in use but probably not needed)
    /// </summary>
    public class UserFloatComparer : IComparer<KeyValuePair<User, float>>
    {
        public int Compare(KeyValuePair<User, float> x, KeyValuePair<User, float> y)
        {
            if (x.Value == y.Value) return 0;
            if (x.Value > y.Value) return -1;
            else return 1;
        }

    }

    [Category("Hidden")]
    public class UtilsInitItem : Item
    {
        static UtilsInitItem()
        {
            SkillUtils.Initialize();
            UtilsClipboard.Initialize();
            GlobalEvents.Initialize();
        }
    }

    /// <summary>
    /// Most of the Functionality of this class is already available via the <see cref="Text"/> class.
    /// <para/>The main difference is that this one provides its methods as extension methods for <see cref="string"/>.
    /// </summary>
    public static class CustomStringExtension
    {
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

    }
}
