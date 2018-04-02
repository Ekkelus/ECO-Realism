    using Eco.Gameplay.Players;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Chat;
    using System;
    using System.Collections.Generic;
    using System.Linq;


namespace EcoRealism.Utils
{
    public class ChatCommands : IChatCommandHandler
    {
        [ChatCommand("rules", "Displays the Server Rules")]
        public static void Rules(User user)
        {
            user.Player.OpenInfoPanel("Rules", IOUtils.ReadConfig("rules.txt"));
        }

        [ChatCommand("changelog", "Displays the changelog of the modkit")]
        public static void Changelog(User user)
        {
            string x = IOUtils.ReadConfig("changelog.txt");         
            user.Player.OpenInfoPanel("Changelog", x);
        }

        [ChatCommand("houseranking", "Displays the users with the highest housing rates")]
        public static void HouseRanking(User user)
        {

            int usercount = UserManager.Users.Count<User>();
            int max = 10;
            int i = 0;
            string output = string.Empty;
            List<KeyValuePair< string, float>> userlist = new List<KeyValuePair<string, float>>(usercount);
            string tmpuser = string.Empty;

            foreach (User userentry in UserManager.Users)
            {
                userlist.Add(new KeyValuePair<string,float>(userentry.Name, (float)Math.Round(userentry.CachedHouseValue.HousingSkillRate,2)));
            }

            userlist.Sort(new MyComparer());           

            if (usercount < max) max = usercount;
            for(i=0; i<max; i++)
            {
                tmpuser = userlist[i].Key;
                output = output + (i + 1).ToString() + ". <link=\"User:" + tmpuser + "\"><style=\"Warning\">" + tmpuser + "</style></link>: <link=\"CachedHouseValue:" + tmpuser +"\"><style=\"Positive\">" + userlist[i].Value.ToString() + "</style></link> skill/day <br>";
            }
            output = output + "<br> Your " + user.HouseValue;

            user.Player.OpenInfoPanel("House Ranking", output);
        }


        [ChatCommand("testlogdmp",level: ChatAuthorizationLevel.Admin)]
        public static void testlogdmp(User user)
        {
            string log = user.Markers.List.First().TooltipTile;
            //string log = user.HouseValue;

            //log = log.Split('>')[3].Split('<')[0];

            IOUtils.WriteToLog(log);

            user.Player.SendTemporaryMessageAlreadyLocalized("Wrote to file");
            //user.Player.OpenInfoPanel("bla", log);

        }


        [ChatCommand("confirmsuperskill", "Confirm that you read the warning about superskills")]
        public static void ConfirmSuperSkill(User user)
        {
            foreach(string id in SkillUtils.superskillconfirmed)
            {
                if (id == user.ID)
                {
                    user.Player.SendTemporaryErrorAlreadyLocalized("You already unlocked SuperSkills");
                    return;
                }                   
            }
            user.Player.SendTemporaryMessageAlreadyLocalized("You can now level up a Skill to level 10");
            SkillUtils.superskillconfirmed.Add(user.ID);
        }
    }


}