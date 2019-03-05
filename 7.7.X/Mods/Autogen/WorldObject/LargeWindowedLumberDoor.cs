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
    }


    public partial class LargeWindowedLumberDoorRecipe : Recipe
    {
        public LargeWindowedLumberDoorRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<LargeWindowedLumberDoorItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LumberItem>(typeof(LumberSkill), 20, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<GlassItem>(typeof(LumberSkill), 10, LumberSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(LargeWindowedLumberDoorRecipe), Item.Get<LargeWindowedLumberDoorItem>().UILink(), 30, typeof(LumberSkill));
            Initialize(Localizer.DoStr("Large Windowed Lumber Door"), typeof(LargeWindowedLumberDoorRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}