namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;
    using Shared.Serialization;

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
    }


    public partial class LargeLumberDoorRecipe : Recipe
    {
        public LargeLumberDoorRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<LargeLumberDoorItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LumberItem>(typeof(LumberSkill), 20, LumberSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(LargeLumberDoorRecipe), Item.Get<LargeLumberDoorItem>().UILink(), 20, typeof(LumberSkill));
            Initialize(Localizer.DoStr("Large Lumber Door"), typeof(LargeLumberDoorRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}