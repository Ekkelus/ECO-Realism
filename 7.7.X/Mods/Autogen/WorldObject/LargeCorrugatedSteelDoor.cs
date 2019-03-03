namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

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

        static LargeCorrugatedSteelDoorItem()
        {
            
        }

        
    }


    [RequiresSkill(typeof(AdvancedSmeltingSkill), 2)]
    public partial class LargeCorrugatedSteelDoorRecipe : Recipe
    {
        public LargeCorrugatedSteelDoorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LargeCorrugatedSteelDoorItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(AdvancedSmeltingSkill), 20, AdvancedSmeltingSkill.MultiplicativeStrategy),   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(LargeCorrugatedSteelDoorRecipe), Item.Get<LargeCorrugatedSteelDoorItem>().UILink(), 30, typeof(AdvancedSmeltingSkill));
            this.Initialize(Localizer.DoStr("Large Corrugated Steel Door"), typeof(LargeCorrugatedSteelDoorRecipe));
            CraftingComponent.AddRecipe(typeof(RollingMillObject), this);
        }
    }
}