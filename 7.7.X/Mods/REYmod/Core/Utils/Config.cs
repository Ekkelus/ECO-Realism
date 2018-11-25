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
	using Eco.Shared.Localization;
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
using REYmod.Blocks;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Plugins;
using Eco.Core.Utils;
using Eco.Core.Controller;

namespace REYmod.Config
{



    public class REYmodSettings : Singleton<REYmodSettings>, IConfigurablePlugin, IModKitPlugin
    {
        public IPluginConfig PluginConfig { get { return this.config; } }
        private PluginConfig<REYconfig> config;
        public REYconfig Config { get { return this.config.Config; } }

        public REYmodSettings()
        {
            this.config = new PluginConfig<REYconfig>("REYmod");

            //int maxSuperskills;
            //double maxinactivetime;
            //bool showwelcomemessage;

            //if (int.TryParse(CommandLine.GetValueArg("maxsuperskills"), out maxSuperskills))
            //{
            //    this.Config.Maxsuperskills = maxSuperskills;
            //    this.SaveConfig();
            //}

            //if (double.TryParse(CommandLine.GetValueArg("maxinactivetime"), out maxinactivetime))
            //{
            //    this.Config.Maxinactivetime = maxinactivetime;
            //    this.SaveConfig();
            //}

            //if (bool.TryParse(CommandLine.GetValueArg("showwelcomemessage"), out showwelcomemessage))
            //{
            //    this.Config.Showwelcomemessage = showwelcomemessage;
            //    this.SaveConfig();
            //}

            //this.Config.Configfolderpath = CommandLine.GetValueArg("configfolderpath");
            //this.SaveConfig();




        }

        public void OnEditObjectChanged(object o, string param)
        {
            //this.Config.OnParamChanged(param);
            this.SaveConfig();
        }

        public object GetEditObject()
        {
            return this.config.Config;
        }
        public string GetStatus()
        {
            return "All Fine";
        }
        public override string ToString()
        {
            return Localizer.DoStr("REYmod");
        }
    }
}
