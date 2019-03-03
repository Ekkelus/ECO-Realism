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
    public partial class LargeStandingHewnLogSignObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Standing Hewn Log Sign"); } } 

        public virtual Type RepresentedItemType { get { return typeof(LargeStandingHewnLogSignItem); } } 


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
    [Weight(1000)]
    public partial class LargeStandingHewnLogSignItem :
        WorldObjectItem<LargeStandingHewnLogSignObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Standing Hewn Log Sign"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A large sign for all your large text needs!"); } }

        static LargeStandingHewnLogSignItem()
        {
            
        }

    }


    [RequiresSkill(typeof(WoodworkingSkill), 3)]
    public partial class LargeStandingHewnLogSignRecipe : Recipe
    {
        public LargeStandingHewnLogSignRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LargeStandingHewnLogSignItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(WoodworkingEfficiencySkill), 25, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(WoodworkingEfficiencySkill), 8, WoodworkingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(LargeStandingHewnLogSignRecipe), Item.Get<LargeStandingHewnLogSignItem>().UILink(), 10, typeof(WoodworkingSpeedSkill));
            this.Initialize(Localizer.DoStr("Large Standing Hewn Log Sign"), typeof(LargeStandingHewnLogSignRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}