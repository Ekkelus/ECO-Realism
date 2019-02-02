using Eco.Shared.Localization;
using Eco.Shared.Utils;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Plugins;

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
