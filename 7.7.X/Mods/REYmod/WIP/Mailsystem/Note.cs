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
    using Eco.Gameplay.Systems.Chat;
    using Eco.Core.Utils;

    [Serialized]
    [Weight(0)]
    [MaxStackSize(1)]
    public partial class NoteItem : Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Note"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(description); } }


        [Serialized] public string description = "A piece of paper with some text written on it";
        [Serialized] public User owner = null;

        [Tooltip(1)]
        public string OwnerTooltip(Player player)
        {
            if (owner == null) return "";
            else return "by " + owner.Name;
        }






    }




    [Serialized]
    [Weight(0)]
    public partial class EmptyNoteItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Empty Note"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("An Empty note. You can copy text from a sign to it or use /writenote to add text."); } }


        //[Serialized] private string description = "An Empty note. You can copy text from a sign to it or use /writenote to add text.";
        //[Serialized] private string name = "Empty Note";
        //[Serialized] private bool empty = true;


        public override InteractResult OnActRight(InteractionContext context)
        {
                        if (context.HasTarget && context.Target is WorldObject)
            {               
                WorldObject wo = context.Target as WorldObject;
                if(wo.HasComponent<CustomTextComponent>())
                {
                    NoteItem noteItem = (NoteItem)Item.Create<NoteItem>();

                    noteItem.description = wo.GetComponent<CustomTextComponent>().Text;
                    noteItem.owner = context.Player.User;

                    InventoryChangeSet inventoryChange = new InventoryChangeSet(context.Player.User.Inventory, context.Player.User);                   
                    inventoryChange.RemoveItem(typeof(EmptyNoteItem));
                    inventoryChange.AddItem(noteItem);
                    if (inventoryChange.TryApply().Success) return InteractResult.Success;
                    else return InteractResult.Failure("Not enough room in inventory".ToLocString());//ChatManager.ServerMessageToPlayer("Not enough room in inventory",context.Player.User);

                }

            }


            return base.OnActRight(context);
        }


        //[Tooltip(10)]
        //public string WrittenNote(Player player)
        //{
        //    return name + "\n" + description;
        //}




    }

}