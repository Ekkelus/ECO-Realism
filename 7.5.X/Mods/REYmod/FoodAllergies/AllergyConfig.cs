using Eco.Core.Controller;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REYmod.Config
{

    public partial class REYconfig
    {
        private bool foodallergiesenabled = false;


        [LocDescription("Enable/disable Food allergies")]
        [SyncToView]public bool Foodallergiesenabled { get { return foodallergiesenabled; } set { foodallergiesenabled = value; } }
    }

}
