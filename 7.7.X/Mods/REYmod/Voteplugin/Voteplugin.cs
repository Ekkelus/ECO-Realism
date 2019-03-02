using Eco.Core.Controller;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using REYmod.Config;
using System.Net;

/*
This is the Voteplugin that interacts with the API of Ecoservers.io to make Voterewards possible
*/
namespace REYmod.Utils
{
    public class VoteCommands : IChatCommandHandler
    {
        //this is the chatcommand a user has to enter to claim his voterewards
        [ChatCommand("claimvote", "Claims your vote on Ecoservers.io and get your reward")]
        public static void claimvote(User user)
        {
            int emptystacks = 0;
            switch(VoteUtils.CheckVote(user))
            {
                case -2:
                    ChatUtils.SendMessage(user, "Received an unexpected response from Ecoservers.io. Please try again later.");
                    return;
                case -1:
                    ChatUtils.SendMessage(user, "There was an error when contacting Ecoservers.io. Please try again later.");
                    return;
                case 0:
                    ChatUtils.SendMessage(user, "You have not voted yet. Please vote on Ecoservers.io first.");
                    return;
                case 1: // pass
                    break;
                case 2:
                    ChatUtils.SendMessage(user, "You already claimed your reward.");
                    return;

                default:
                    ChatUtils.SendMessage(user, "Something went wrong... Try again, if it's still the same error, pls report it. (Checkcase default)");
                    return;
            }

            foreach(ItemStack stack in user.Inventory.Stacks)
            {
                
                if (stack.Empty)
                {
                    emptystacks++;
                    //ChatUtils.SendMessage(user, "<color=green> Found empty Stack");
                }
                //else ChatUtils.SendMessage(user, "Stack: " + stack.Item.NameAndNum(stack.Quantity));
            }
            if (emptystacks <= 2)
            {
                ChatUtils.SendMessage(user, "Not enough space in inventory. Please make sure to have more free space.");
                return;
            }

            switch (VoteUtils.SetVote(user))
            {
                case -2:
                    ChatUtils.SendMessage(user, "Received an unexpected response from Ecoservers. Please try again later.");
                    return;
                case -1:
                    ChatUtils.SendMessage(user, "There was an error when contacting Ecoservers.io. Please try again later.");
                    return;
                case 0:                   
                    ChatUtils.SendMessage(user, "Something went wrong... Try again, if it's still the same error, pls report it. (Claimcase 0)");
                    return;
                case 1:
                    break;
                default:
                    ChatUtils.SendMessage(user, "Something went wrong... Try again, if it's still the same error, pls report it. (Claimcase default)");
                    return;
            }
            ChatUtils.SendMessage(user, "Thanks for Voting! Here is your reward :D");

            // maybe that should be moved somewhere else for better configurability
            user.Inventory.AddItems(typeof(CoinItem), 10);



        }
    }

    public class VoteUtils
    {
        /// <summary>
        /// Checks the voting state of the given user on Ecoservers.io
        /// </summary>
        /// <param name="user">The user that will be checked</param>
        /// <returns>
        /// -2 -> Unexpected response (could not parse to integer)
        /// -1 -> Connection Error
        ///  0 -> User has not voted in the last 24h
        ///  1 -> User has voted but not claimed his reward yet
        ///  2 -> User has voted and already claimed his reward
        /// </returns>
        public static int CheckVote(User user)
        {
            WebClient client = new WebClient();
            string response;
            int intresponse;

            string encodedname = WebUtility.UrlEncode(user.Name);
            string url = "https://ecoservers.io/api/?object=votes&element=claim&key=" + REYmodSettings.Obj.Config.Apikey + "&username=" + encodedname;

            try
            {
                response = client.DownloadString(url);
                //ChatUtils.SendMessage(user, "Check:" + response);
            }
            catch (WebException)
            {
                return -1;
                //throw new System.Exception("There was an error when contacting Ecoservers.io. Please try again later.");
            }

            if (int.TryParse(response, out intresponse))
            {
                return intresponse;
            }
            else
            {
                return -2;
                // ChatUtils.SendMessage(user, "Received an unexpected respone from Ecoservers. Please try again later.");
            }
        }
        /// <summary>
        /// Set the vote state of the given user to "claimed"
        /// </summary>
        /// <param name="user">The user who claimed his reward</param>
        /// <returns>
        /// -2 -> Unexpected response (could not parse to integer)
        /// -1 -> Connection Error
        ///  0 -> Vote could not be claimed (user not found or already claimed)
        ///  1 -> OK - Vote has been claimed
        ///  </returns>
        public static int SetVote(User user)
        {
            WebClient client = new WebClient();
            string response;
            int intresponse;
            string encodedname = WebUtility.UrlEncode(user.Name);
            string url = "https://ecoservers.io/api/?action=post&object=votes&element=claim&key=" + REYmodSettings.Obj.Config.Apikey + "&username=" + encodedname;

            try
            {
                response = client.DownloadString(url);
                //ChatUtils.SendMessage(user, "Set:" + response);
            }
            catch (WebException)
            {
                return -1;
                //throw new System.Exception("There was an error when contacting Ecoservers.io. Please try again later.");
            }

            if (int.TryParse(response, out intresponse))
            {
                return intresponse;
            }
            else
            {
                return -2;
                // ChatUtils.SendMessage(user, "Received an unexpected respone from Ecoservers. Please try again later.");
            }

        }


        /* NOT ready yet, i honestly lost a bit of interest in this idea when i noticed that its a bit more complicated for not much use
         * 
        public static User topvoter;
        public static void Initialize()
        {
            UpdateTopVoter();
            GlobalEvents.OneMinuteEvent.Add(UpdateTopVoter);
        }

        public static void UpdateTopVoter()
        {
            WebClient client = new WebClient();
            string response;
            int intresponse;

            string encodedname = WebUtility.UrlEncode(user.Name);
            string url = "https://ecoservers.io/api/?object=servers&element=voters&key=" + REYmodSettings.Obj.Config.Apikey + "&month=current&format=json&limit=1" ;

            try
            {
                response = client.DownloadString(url);
                //ChatUtils.SendMessage(user, "Check:" + response);
            }
            catch (WebException)
            {
                return -1;
                //throw new System.Exception("There was an error when contacting Ecoservers.io. Please try again later.");
            }
        }
    }

    [Category("Hidden")]
    public class VoteInitItem : Item
    {
        static VoteInitItem()
        {
            VoteUtils.Initialize();
        }
    }
    */
    }
}

namespace REYmod.Config
{
    public partial class REYconfig
    {
        private string apikey = "";


        [LocDescription("APIkey for Ecoservers.io")]
        [SyncToView] public string Apikey { get { return apikey; } set { apikey = value; } }
    }

}