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
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(CustomTextComponent))]              
    [RequireComponent(typeof(MinimapComponent))]                
    public partial class LargeHangingLumberSignObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Hanging Lumber Sign"); } } 

        public virtual Type RepresentedItemType { get { return typeof(LargeHangingLumberSignItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Sign");                                 
            GetComponent<CustomTextComponent>().Initialize(700);                                       


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1000)]
    public partial class LargeHangingLumberSignItem :
        WorldObjectItem<LargeHangingLumberSignObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Hanging Lumber Sign"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A large sign for all your large text needs!"); } }
    }


    [RequiresSkill(typeof(LumberSkill), 4)]
    public partial class LargeHangingLumberSignRecipe : Recipe
    {
        public LargeHangingLumberSignRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<LargeHangingLumberSignItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(LumberSkill), 15, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<LumberItem>(typeof(LumberSkill), 10, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(LumberSkill), 8, LumberSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(LargeHangingLumberSignRecipe), Item.Get<LargeHangingLumberSignItem>().UILink(), 10, typeof(LumberSkill));
            Initialize(Localizer.DoStr("Large Hanging Lumber Sign"), typeof(LargeHangingLumberSignRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}