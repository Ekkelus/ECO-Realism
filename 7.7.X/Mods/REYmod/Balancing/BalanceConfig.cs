using Eco.Core.Controller;
using Eco.Shared.Localization;

namespace REYmod.Config
{
    public partial class REYconfig
    {
        #region Organisms

        #region Animals

        #endregion

        #region Plants



        private float _plantYieldMultiplier = .5f;
        private float _seedDropMultiplier;

        [LocDescription(
            "How much should plant yield be multiplied with (where 0.5 is 50%, 1 is 100%, etc.)? Default: 0.5")]
        [SyncToView]
        public float PlantYieldMultiplier
        {
            get { return _plantYieldMultiplier; }
            set
            {
                this.Changed("PlantYieldMultiplier");
                _plantYieldMultiplier = value;
            }
        }

        public float SeedDropMultiplier
        {
            get { return _seedDropMultiplier; }
            set
            {
                this.Changed("SeedDropMultiplier");
                _seedDropMultiplier = value;
            }
        }

        #endregion

        #region Trees

        #endregion

        #endregion
    }
}