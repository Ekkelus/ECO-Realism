namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(OnOffComponent))]                   
    [RequireComponent(typeof(PropertyAuthComponent))]
    public partial class LargeCorrugatedSteelDoorObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Corrugated Steel Door"); } } 

        public virtual Type RepresentedItemType { get { return typeof(LargeCorrugatedSteelDoorItem); } } 


        protected override void Initialize()
        {


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class LargeCorrugatedSteelDoorItem :
        WorldObjectItem<LargeCorrugatedSteelDoorObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Corrugated Steel Door"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A large door."); } }
    }


    [RequiresSkill(typeof(AdvancedSmeltingSkill), 2)]
    public partial class LargeCorrugatedSteelDoorRecipe : Recipe
    {
        public LargeCorrugatedSteelDoorRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<LargeCorrugatedSteelDoorItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(AdvancedSmeltingSkill), 20, AdvancedSmeltingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(LargeCorrugatedSteelDoorRecipe), Item.Get<LargeCorrugatedSteelDoorItem>().UILink(), 30, typeof(AdvancedSmeltingSkill), typeof(AdvancedSmeltingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Large Corrugated Steel Door"), typeof(LargeCorrugatedSteelDoorRecipe));
            CraftingComponent.AddRecipe(typeof(RollingMillObject), this);
        }
    }
}