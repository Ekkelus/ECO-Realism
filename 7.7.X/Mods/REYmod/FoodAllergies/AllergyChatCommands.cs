using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Gameplay.Systems.Chat;
using Eco.Gameplay.Items;
using REYmod.Utils;

namespace REYmod.Allergies
{
    class AllergyChatCommands : IChatCommandHandler
    {
        [ChatCommand("setallergy", "sets the allergy of the given user or yourself", level: ChatAuthorizationLevel.Admin)]
        public static void SetAllergy(User user, string item, string username = "")
        {
            User target = UserManager.FindUserByName(username);
            target = target ?? user;
            Item x = Item.GetItemByString(user, item);
            if (x != null)
            {
                target.SetState("allergy", x.Type.Name);
                ChatUtils.SendMessage(user, "You made " + target.UILink() + " allergic to " + x.UILink());
                ChatUtils.SendMessage(target, "You are now allergic to " + x.UILink());
            }
        }

        [ChatCommand("clearallergy", "clears allergies", level: ChatAuthorizationLevel.Admin)]
        public static void ClearAllergy(User user, string username = "")
        {
            User target = UserManager.FindUserByName(username);
            target = target ?? user;
            target.SetState("allergy", string.Empty);
            ChatUtils.SendMessage(user, "You cleared the allergies of " + target.UILink());
            ChatUtils.SendMessage(target, "Your allergies have been cleared");
        }
    }
}
