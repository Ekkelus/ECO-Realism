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

    [Serialized]
    [Weight(0)]
    [MaxStackSize(1)]
    public partial class NoteItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr(name); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(description); } }


        [Serialized] private string description = "An Empty note. You can copy text from a sign to it.";
        [Serialized] private string name = "Empty Note";


        public override InteractResult OnActRight(InteractionContext context)
        {

            if (context.Target != null && context.Target is WorldObject)
            {               
                WorldObject wo = context.Target as WorldObject;
                if(wo.HasComponent<CustomTextComponent>())
                {
                    description = wo.GetComponent<CustomTextComponent>().Text;
                    name = context.Player.User.Name + "'s Note";
                    return InteractResult.Success;
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