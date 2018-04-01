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

        [ChatCommand("changelog", "Displays the Server Rules")]
        public static void Changelog(User user)
        { }


    }


}