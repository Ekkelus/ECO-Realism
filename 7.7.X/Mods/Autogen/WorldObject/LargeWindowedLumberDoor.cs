namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(OnOffComponent))]                   
    [RequireComponent(typeof(PropertyAuthComponent))]
    public partial class LargeWindowedLumberDoorObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Windowed Lumber Door"); } } 

        public virtual Type RepresentedItemType { get { return typeof(LargeWindowedLumberDoorItem); } } 


        protected override void Initialize()
        {


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class LargeWindowedLumberDoorItem :
        WorldObjectItem<LargeWindowedLumberDoorObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Windowed Lumber Door"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A large door."); } }

        static LargeWindowedLumberDoorItem()
        {
            
        }

        
    }


    public partial class LargeWindowedLumberDoorRecipe : Recipe
    {
        public LargeWindowedLumberDoorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LargeWindowedLumberDoorItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LumberItem>(typeof(LumberSkill), 20, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<GlassItem>(typeof(LumberSkill), 10, LumberSkill.MultiplicativeStrategy),   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(LargeWindowedLumberDoorRecipe), Item.Get<LargeWindowedLumberDoorItem>().UILink(), 30, typeof(LumberSkill));
            this.Initialize(Localizer.DoStr("Large Windowed Lumber Door"), typeof(LargeWindowedLumberDoorRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}