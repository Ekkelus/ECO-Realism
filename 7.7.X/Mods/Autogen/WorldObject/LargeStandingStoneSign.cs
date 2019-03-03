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
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(CustomTextComponent))]              
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class LargeStandingStoneSignObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Standing Stone Sign"); } } 

        public virtual Type RepresentedItemType { get { return typeof(LargeStandingStoneSignItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Sign");                                 
            this.GetComponent<CustomTextComponent>().Initialize(700);                                       


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(2000)]
    public partial class LargeStandingStoneSignItem :
        WorldObjectItem<LargeStandingStoneSignObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Standing Stone Sign"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A large sign for all your large text needs!"); } }

        static LargeStandingStoneSignItem()
        {
            
        }

    }


    [RequiresSkill(typeof(MortaringSkill), 3)]
    public partial class LargeStandingStoneSignRecipe : Recipe
    {
        public LargeStandingStoneSignRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LargeStandingStoneSignItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(MortaringSkill), 60, MortaringSkill.MultiplicativeStrategy),   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(LargeStandingStoneSignRecipe), Item.Get<LargeStandingStoneSignItem>().UILink(), 10, typeof(MortaringSkill));
            this.Initialize(Localizer.DoStr("Large Standing Stone Sign"), typeof(LargeStandingStoneSignRecipe));
            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }
}