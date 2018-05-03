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

namespace REYmod.Allergies
{
    class AllergyChatCommands : IChatCommandHandler
    {
        [ChatCommand("setallergy", "sets the allergy of the given user or yourself", level: ChatAuthorizationLevel.Admin)]
        public static void SetAllergy(User user, string item, string username = "")
        {
            User target = UserManager.FindUserByName(username);
            target = target ?? user;
            Item x = Item.GetItemByString(user, item);
            if (x != null)
            {
                target.SetState("allergy", x.Type.Name);
            }
        }

        [ChatCommand("clearallergy", "clears allergies", level: ChatAuthorizationLevel.Admin)]
        public static void ClearAllergy(User user, string username = "")
        {
            User target = UserManager.FindUserByName(username);
            target = target ?? user;
            target.SetState("allergy", string.Empty);
        }
    }
}
