namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Items;
    using Shared.Serialization;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Core.Utils;
    using Eco.Gameplay.Systems.Chat;

    [Serialized]
    [Weight(0)]
    [MaxStackSize(1)]
    public partial class LetterItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Letter"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A sealed letter. Can only be opened by the intended recipient"); } }

        [Serialized] public User sender = null;
        [Serialized] public User recipient = null;
        [Serialized] public Item content = null;
        [Serialized] public int contentquantity = 1;
        [Serialized] public string desc = "";

        

        public override InteractResult OnActRight(InteractionContext context)
        {
            if (context.HasTarget && context.Target is WorldObject)
            {
                return base.OnActRight(context);

            }

            return base.OnActRight(context);
        }

        public override void OnUsed(Player player, ItemStack itemStack)
        {
            if (player.User != sender && player.User != recipient)
            {
                ChatManager.ServerMessageToPlayer("You are not allowed to open this",player.User);
                return; 
            }


            InventoryChangeSet inventoryChange = new InventoryChangeSet(player.User.Inventory, player.User);
            inventoryChange.ModifyStack(itemStack, contentquantity - 1, content);
            if (inventoryChange.TryApply().Success) return;            
            else ChatManager.ServerMessageToPlayer("Not enough room in inventory", player.User);
        }


        [Tooltip(1)]
        public string SenderRecipient(Player player)
        {
            if (sender == null && recipient == null) return "";

            return "Recipient: " + recipient.Name + "<br>Sender: " + sender.Name;
        }




    }

}