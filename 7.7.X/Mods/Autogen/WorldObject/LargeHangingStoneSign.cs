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
    public partial class LargeHangingStoneSignObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Hanging Stone Sign"); } } 

        public virtual Type RepresentedItemType { get { return typeof(LargeHangingStoneSignItem); } } 


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
    [Weight(2000)]
    public partial class LargeHangingStoneSignItem :
        WorldObjectItem<LargeHangingStoneSignObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Hanging Stone Sign"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A large sign for all your large text needs!"); } }
    }


    [RequiresSkill(typeof(MortaringSkill), 4)]
    public partial class LargeHangingStoneSignRecipe : Recipe
    {
        public LargeHangingStoneSignRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<LargeHangingStoneSignItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(MortaringSkill), 60, MortaringSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(LargeHangingStoneSignRecipe), Item.Get<LargeHangingStoneSignItem>().UILink(), 10, typeof(MortaringSkill));
            Initialize(Localizer.DoStr("Large Hanging Stone Sign"), typeof(LargeHangingStoneSignRecipe));
            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }
}