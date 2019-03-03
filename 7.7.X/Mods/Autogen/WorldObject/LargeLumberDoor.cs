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
    public partial class LargeLumberDoorObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Lumber Door"); } } 

        public virtual Type RepresentedItemType { get { return typeof(LargeLumberDoorItem); } } 


        protected override void Initialize()
        {


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class LargeLumberDoorItem :
        WorldObjectItem<LargeLumberDoorObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Lumber Door"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A large door."); } }

        static LargeLumberDoorItem()
        {
            
        }

        
    }


    public partial class LargeLumberDoorRecipe : Recipe
    {
        public LargeLumberDoorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LargeLumberDoorItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LumberItem>(typeof(LumberSkill), 20, LumberSkill.MultiplicativeStrategy),   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(LargeLumberDoorRecipe), Item.Get<LargeLumberDoorItem>().UILink(), 20, typeof(LumberSkill));
            this.Initialize(Localizer.DoStr("Large Lumber Door"), typeof(LargeLumberDoorRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}