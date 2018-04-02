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
            user.Player.OpenInfoPanel("Changelog", IOUtils.ReadConfig("changelog.txt"));
        }

        [ChatCommand("housingranking", "Displays the users with the highest housing rates")]
        public static void HousingRanking(User user)
        {

            int usercount = UserManager.Users.Count<User>();
            int max = 5;
            int i = 0;
            string output = string.Empty;
            List<KeyValuePair< string, float>> userlist = new List<KeyValuePair<string, float>>(usercount);
            string tmpstring = string.Empty;
            float tmpfloat = 0f;
            string tmpuser = string.Empty;

            foreach (User userentry in UserManager.Users)
            {
                tmpstring = userentry.HouseValue;
                tmpstring = tmpstring.Split('>')[3].Split('<')[0];
                tmpfloat = Convert.ToSingle(tmpstring);
                

                userlist.Add(new KeyValuePair<string,float>(user.Name, tmpfloat));
            }

            userlist.Sort(new MyComparer());
            

            if (usercount < 5) max = usercount;

            for(i=0; i<max; i++)
            {
                tmpuser = userlist[i].Key;
                output = output + (i + 1).ToString() + ". <link=\"User:" + tmpuser + "\"><style=\"Warning\">" + tmpuser + "</style></link>: <b><link=\"CachedHouseValue:" + tmpuser +"\"><style=\"Positive\">" + userlist[i].Value.ToString() + "</style></link></b> skill/day <br>";
            }
            output = output + "<br> Your " + user.HouseValue;

            user.Player.OpenInfoPanel("HouseRanking", output);
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
                    user.Player.SendTemporaryErrorAlreadyLocalized("You are already on the list");
                    return;
                }                   
            }
            SkillUtils.superskillconfirmed.Add(user.ID);
        }
    }


}