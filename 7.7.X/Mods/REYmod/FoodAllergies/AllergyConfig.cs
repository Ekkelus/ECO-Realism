using Eco.Core.Controller;
using Eco.Shared.Localization;

namespace REYmod.Config
{

    public partial class REYconfig
    {
        private bool foodallergiesenabled = false;


        [LocDescription("Enable/disable Food allergies")]
        [SyncToView]public bool Foodallergiesenabled { get { return foodallergiesenabled; } set { foodallergiesenabled = value; } }
    }

}
