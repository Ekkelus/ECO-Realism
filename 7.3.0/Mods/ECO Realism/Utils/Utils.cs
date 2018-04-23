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
using EcoRealism.Utils;
using Eco.Shared.Localization;
using System.Linq;
using Eco.Core.Utils;
using Eco.Gameplay.Stats;
using Eco.Mods.TechTree;
using Eco.Core.Utils.AtomicAction;

namespace EcoRealism.Utils
{
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


    public static class SkillUtils
    {
        /// <summary>
        /// This Method should override the CreateLevelUpAction() Method of Skills that can be superskilled.
        /// </summary>
        /// <seealso cref="Skill.CreateLevelUpAction(Player)"/>
        /// <param name="skill"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public static IAtomicAction SuperSkillLevelUp(Skill skill,Player player)
        {
            if (skill.Level != 5) return SimpleAtomicAction.NoOp;
            if (SkillUtils.SuperSkillCount(player.User) >= ConfigHandler.maxsuperskills) return new FailedAtomicAction(Localizer.Do("You already have enough SuperSkills " + SkillUtils.SuperSkillCount(player.User) + "/" + ConfigHandler.maxsuperskills));
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

        public static List<string> superskillconfirmed;

        public static void Initialize()
        {
            superskillconfirmed = new List<string>();
        }

        public static void ShowSuperSkillInfo(Player player)
        {
            player.OpenInfoPanel("Super Skills", "Current amount of Super Skills: <b><color=green>" + SkillUtils.SuperSkillCount(player.User) + "</color></b><br>Max amount of Super Skills: <b><color=green>" + ConfigHandler.maxsuperskills + "</color></b><br><br>Super Skills are Skills that can be leveled all the way up to level 10.<br><br><color=red>You can only have a limited amount of them.</color><br><br>To confirm that you understood Super Skills and to unlock them please enter <b><color=green>/confirmsuperskill</b></color>");
        }

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

        public static int SuperSkillCount(User user)
        {
            int x = 0;
            foreach (Skill skill in user.Skillset.Skills)
            {
                if (skill.Level > 5) x++;
            }
            return x;
        }

        public static List<Skill> GetSuperSkills(User user)
        {
            List<Skill> superskills = new List<Skill>();

            foreach(Skill skill in user.Skillset.Skills)
            {
                if(skill.Level > 5)
                {
                    superskills.Add(skill);
                }
            }
            return superskills;
        }

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



            foreach(Skill skill in skills)
            {
                if (skill.IsSpecialty)
                {
                    if(skill.Level>0) rootskills[skill.RootSkillTree] += 100;
                }
                else rootskills[skill.RootSkillTree] += skill.TotalPointSpent;
            }

            foreach(KeyValuePair<SkillTree,int> pair in rootskills)
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
    }

    public static class ChatUtils
    {
        public static void SendMessage(User user, string msg)
        {
            ChatManager.ServerMessageToPlayer(Localizer.Do(AutoLink(msg)), user, false);
        }

        public static void SendMessage(Player player, string msg)
        {
            SendMessage(player.User, msg);
        }

        //public static string CustomTags(string text)
        //{
        //    string[] tmparray;
        //    string tmpstr = string.Empty;
        //    string open, close, item, itemfriendly = string.Empty;
        //    int i = 0;
        //    tmparray = text.Split(new string[] { "@@" }, StringSplitOptions.None);
        //    close = "</ecoicon></link></style>";
        //    if (tmparray.Length == 1) return text;
        //    for (i = 0; i < tmparray.Length - 1; i = i + 2)
        //    {
        //        item = tmparray[i + 1];
        //        try
        //        {
        //            itemfriendly = Item.GetItemByString(null, item).FriendlyName;
        //            open = "<link=\"Item:" + item + "Item\"><style=\"Item\"><ecoicon item='" + item + "Item'>";
        //            tmpstr = tmpstr + tmparray[i] + open + itemfriendly + close;
        //        }
        //        catch (Exception)
        //        {
        //            tmpstr = tmpstr + tmparray[i] + item ;
        //        }
        //    }
        //    if(tmparray.Length%2 != 0) tmpstr = tmpstr + tmparray[tmparray.Length - 1];
        //    return tmpstr;
        //}

        public static string AutoLink(string text) // should not be used better call TextLinkManager.MarkUpText() directly
        {
            return TextLinkManager.MarkUpText(text);
        }
    }


    public static class IOUtils
    {
        public static string ReadConfig(string filename)
        {
            string content = string.Empty;
            string path = "./mods/ECO Realism/configs/" + filename;


            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else return "Error reading file: File does not exist!";


        }

        public static void WriteToLog(string logdata, string desc = "")
        {
            string path = "./Dump/log.txt";
            logdata = Environment.NewLine + System.Environment.NewLine + desc + System.Environment.NewLine + logdata;
            if (!File.Exists(path)) File.Create(path).Close();

            File.AppendAllText(path, logdata);

        }

        public static void WriteToFile(string path,string text)
        {
            string dirpath = path.Substring(0, path.LastIndexOf('/') + 1);
            if (!Directory.Exists(dirpath)) Directory.CreateDirectory(dirpath);
            if (!File.Exists(path)) File.Create(path).Close();
            File.AppendAllText(path, text);
        }

        public static void ClearFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                File.Create(path).Close();
            }
        }
    }

    public static class MiscUtils
    {
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


    public static class GlobalEvents
    {
        private static ThreadSafeAction<User> LoginActions;

        public static void Initialize()
        {
            LoginActions = UserManager.OnUserLoggedIn;
            LoginActions.Add(OnUserLogin);

            
        }

        private static void OnUserLogin(User user)
        {
            /* Too early, Player is not initialized yet...
            user.Player.OpenInfoPanel("Login", "Hello World!");
            */
        }

 



    }





    public class MyComparer : IComparer<KeyValuePair<string, float>>
    {
        public int Compare(KeyValuePair<string, float> x, KeyValuePair<string, float> y)
        {
            if (x.Value == y.Value) return 0;
            if (x.Value > y.Value) return -1;
            else return 1;
        }

    }


    public static class UtilsClipboard
    {
        public static Dictionary<User,User> UnclaimSelector;

            public static void Initialize()
        {
            UnclaimSelector = new Dictionary<User, User>();

        }

    }



}
