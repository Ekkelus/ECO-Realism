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
using EcoRealism.Utils;


namespace EcoRealism.Utils
{
    public class ConfigInitItem : Item
    {
        static ConfigInitItem()
        {
            ConfigHandler.Initialize();
        }
    }

    public static class ConfigHandler
    {
        public static string configpath = "./configs/ecorealism.cfg";

        public static int maxsuperskills;



        public static void Initialize()
        {
            //if (!File.Exists(configpath))
            //{
            //    File.Create(configpath);

            //}

            InitWithDefault();
        }


        private static void InitWithDefault()
        {
            maxsuperskills = 2;
        }
    }

}