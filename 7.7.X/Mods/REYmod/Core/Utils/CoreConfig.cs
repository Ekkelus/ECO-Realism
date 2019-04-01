using Eco.Core.Controller;
using Eco.Shared.Localization;

namespace REYmod.Config
{
    /// <summary>
    /// Stored config values.
    /// </summary>
    public partial class REYconfig : IController
    {
        private int _maxsuperskills = 2;
        private double _maxinactivetime = 3 * 24; // in hours
        private bool _showwelcomemessage = true;
        private string _configfolderpath = "./mods/REYmod/configs/";
        private bool _sendmessagetoggle = false;
        private string _servermessagesender = "";

        int IController.ControllerID { get ; set ; }

        [LocDescription("How many Superskills a player is allowed to have. Set to -1 for unlimited")]
        [SyncToView] public int Maxsuperskills { get { return _maxsuperskills; } set { _maxsuperskills = value; } }
        [LocDescription("How long a player is allowed to be offline without notice")]
        [SyncToView] public double Maxinactivetime { get { return _maxinactivetime; } set { _maxinactivetime = value; } }
        [LocDescription("Set if the Welcommessage(welcomemessage.txt) should be displayed on first join")]
        [SyncToView] public bool Showwelcomemessage { get { return _showwelcomemessage; } set { _showwelcomemessage = value; } }
        [LocDescription("The folder (relative from EcoServer.exe, or absolute should be ok too) where the rules.txt, welcomemessage.txt, and so on are located")]
        [SyncToView] public string Configfolderpath { get { return _configfolderpath; } set { _configfolderpath = value; } }
        [LocDescription("Enter a message you want to broadcast here, then toggle the SendMessageToggle to send it")]
        [SyncToView] public string ServerMessageSender { get { return _servermessagesender; } set { _servermessagesender = value; } }
        [LocDescription("Switch this value to send a message to everyone")]
        [SyncToView] public bool SendMessageToggle
        {
            get
            {
                return _sendmessagetoggle;
            }
            set
            {
                this.Changed("SendMessageToggle");
                _sendmessagetoggle = value;
            }
        }
    }
}
