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
    public partial class LargeHangingHewnLogSignObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Hanging Hewn Log Sign"); } } 

        public virtual Type RepresentedItemType { get { return typeof(LargeHangingHewnLogSignItem); } } 


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
    public partial class LargeHangingHewnLogSignItem :
        WorldObjectItem<LargeHangingHewnLogSignObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Hanging Hewn Log Sign"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A large sign for all your large text needs!"); } }

        static LargeHangingHewnLogSignItem()
        {
            
        }

    }


    [RequiresSkill(typeof(WoodworkingSkill), 4)]
    public partial class LargeHangingHewnLogSignRecipe : Recipe
    {
        public LargeHangingHewnLogSignRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LargeHangingHewnLogSignItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(WoodworkingEfficiencySkill), 25, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(WoodworkingEfficiencySkill), 8, WoodworkingEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(10, WoodworkingSpeedSkill.MultiplicativeStrategy, typeof(WoodworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(LargeHangingHewnLogSignRecipe), Item.Get<LargeHangingHewnLogSignItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<LargeHangingHewnLogSignItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Large Hanging Hewn Log Sign", typeof(LargeHangingHewnLogSignRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}