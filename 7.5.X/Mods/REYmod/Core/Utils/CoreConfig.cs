using Eco.Core.Controller;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REYmod.Config
{
    /// <summary>
    /// Stored config values.
    /// </summary>
    public partial class REYconfig : IController
    {
        private int maxsuperskills = 2;
        private double maxinactivetime = 3 * 24; // in hours
        private bool showwelcomemessage = true;
        private string configfolderpath = "./mods/REYmod/configs/";

        int IController.ControllerID { get ; set ; }

        [LocDescription("How many Superskills a player is allowed to have. Set to -1 for unlimited")]
        [SyncToView] public int Maxsuperskills { get { return maxsuperskills; } set { maxsuperskills = value; } }
        [LocDescription("How long a player is allowed to be offline without notice")]
        [SyncToView] public double Maxinactivetime { get { return maxinactivetime; } set { maxinactivetime = value; } }
        [LocDescription("Set if the Welcommessage(welcomemessage.txt) should be displayed on first join")]
        [SyncToView] public bool Showwelcomemessage { get { return showwelcomemessage; } set { showwelcomemessage = value; } }
        [LocDescription("The folder (relative from EcoServer.exe, or absolute should be ok too) where the rules.txt, welcomemessage.txt, and so on are located")]
        [SyncToView] public string Configfolderpath { get { return configfolderpath; } set { configfolderpath = value; } }
    }
}
