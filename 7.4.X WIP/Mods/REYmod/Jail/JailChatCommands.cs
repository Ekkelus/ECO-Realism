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
using Eco.Gameplay.Systems.Chat;
using System.Linq;
using Eco.Shared.Networking;
using Eco.Simulation.WorldLayers;
using Eco.World;
using Eco.WorldGenerator;
using Eco.Mods.TechTree;
using System.Timers;
using Eco.Gameplay;
using Eco.Simulation.Time;
using Eco.Shared.Localization;

namespace REYmod.Mods.REYmod.Jail
{
    class JailChatCommands
    {
        [ChatCommand("setjailpos", "", level: ChatAuthorizationLevel.Admin)]
        public static void SetJailPosCommand(User user, int radius = 5)
        { }

        [ChatCommand("jail", "Send a Player to jail", level: ChatAuthorizationLevel.Admin)]
        public static void Jail(User user, string username, int termInMinutes)
        { }

        [ChatCommand("unjail", "Free a player from Jail and teleport him", level: ChatAuthorizationLevel.Admin)]
        public static void UnJail(User user, string username)
        { }
    }
}
