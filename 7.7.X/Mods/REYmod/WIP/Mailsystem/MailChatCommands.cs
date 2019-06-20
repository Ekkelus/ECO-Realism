using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Gameplay.Systems.Chat;
using Eco.Gameplay.Items;
using REYmod.Utils;
using Eco.Mods.TechTree;
using Eco.Gameplay.Interactions;
using Eco.Core.Utils;

namespace REYmod.Mail
{
    class MailChatCommands : IChatCommandHandler
    {
        [ChatCommand("seal", "seals a letter, it can only be opened by you or the intende recipient afterwards", level: ChatAuthorizationLevel.User)]
        public static void Seal(User user, string target)
        {
            UserIdType idtype;
            User targetuser = UserManager.FindUser(target, out idtype);
            if (idtype == UserIdType.Unknown)
            {
                ChatManager.ServerMessageToPlayer("User " + target + " not found",user);
                return;
            }
            if (!(user.Inventory.Toolbar.SelectedItem is NoteItem))
            {
                ChatManager.ServerMessageToPlayer("No Note selected", user);
                return;
            }

            LetterItem letter = (LetterItem)Item.Create<LetterItem>();
            letter.content = user.Inventory.Toolbar.SelectedItem;
            letter.contentquantity = user.Inventory.Toolbar.SelectedStack.Quantity;
            letter.recipient = targetuser;
            letter.sender = user;


            InventoryChangeSet inventoryChange = new InventoryChangeSet(user.Inventory, user);
            inventoryChange.RemoveItem(typeof(EnvelopeItem));
            inventoryChange.ModifyStack(user.Inventory.Toolbar.SelectedStack, 1 - user.Inventory.Toolbar.SelectedStack.Quantity, letter);
            
            if (inventoryChange.TryApply().Success) return;
            else ChatManager.ServerMessageToPlayer("Not enough Envelopes found", user);


        }

        [ChatCommand("writetext", "writes something on the currently selected note", level: ChatAuthorizationLevel.User)]
        public static void WriteText(User user, string text)
        {
            //string onestringtext = string.Concat(text);

                NoteItem noteItem = (NoteItem)Item.Create<NoteItem>();

                noteItem.description = text;
                noteItem.owner = user;

                InventoryChangeSet inventoryChange = new InventoryChangeSet(user.Inventory, user);
                inventoryChange.AddItem(noteItem);
                inventoryChange.RemoveItem(typeof(EmptyNoteItem));
                if (inventoryChange.TryApply().Success) return;
                else ChatManager.ServerMessageToPlayer("Not enough room in inventory", user);
           

        }
    }
}
