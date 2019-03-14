using Eco.Core.Controller;
using Eco.Shared.Localization;
using Eco.Shared.Utils;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Plugins;
using Eco.Core.Utils;

namespace REYmod.Config
{



    public class REYmodSettings : Singleton<REYmodSettings>, IConfigurablePlugin, IModKitPlugin
    {
        public IPluginConfig PluginConfig { get { return _config; } }
        private readonly PluginConfig<REYconfig> _config;
        public REYconfig Config { get { return _config.Config; } }

        public static readonly ThreadSafeAction<float> OnPlantYieldChange = new ThreadSafeAction<float>();
        public static readonly ThreadSafeAction<float> OnSeedDropChange = new ThreadSafeAction<float>();

        public REYmodSettings()
        {
            _config = new PluginConfig<REYconfig>("REYmod");

            _config.Config.Subscribe("PlantYieldMultiplier", _onPlantYieldChange);
            _config.Config.Subscribe("SeedDropMultiplier", _onSeedDropChange);
            _onPlantYieldChange();
            _onSeedDropChange();

        }

        public void OnEditObjectChanged(object o, string param)
        {
            //this.Config.OnParamChanged(param);
            this.SaveConfig();
        }

        public object GetEditObject()
        {
            return _config.Config;
        }
        public string GetStatus()
        {
            return "All Fine";
        }
        public override string ToString()
        {
            return Localizer.DoStr("REYmod");
        }

        private void _onPlantYieldChange()
        {
            OnPlantYieldChange.Invoke(Config.PlantYieldMultiplier);
        }

        private void _onSeedDropChange()
        {
            OnSeedDropChange.Invoke(Config.SeedDropMultiplier);
        }
    }
}
