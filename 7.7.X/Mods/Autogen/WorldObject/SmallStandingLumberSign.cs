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
    public partial class SmallStandingLumberSignObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Standing Lumber Sign"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SmallStandingLumberSignItem); } } 


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
    [Weight(500)]
    public partial class SmallStandingLumberSignItem :
        WorldObjectItem<SmallStandingLumberSignObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Standing Lumber Sign"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A small sign for all of your smaller text needs!"); } }

        static SmallStandingLumberSignItem()
        {
            
        }

    }


    [RequiresSkill(typeof(LumberWoodworkingSkill), 1)]
    public partial class SmallStandingLumberSignRecipe : Recipe
    {
        public SmallStandingLumberSignRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SmallStandingLumberSignItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(LumberWoodworkingEfficiencySkill), 10, LumberWoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<LumberItem>(typeof(LumberWoodworkingEfficiencySkill), 4, LumberWoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(LumberWoodworkingEfficiencySkill), 4, LumberWoodworkingEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(10, LumberWoodworkingSpeedSkill.MultiplicativeStrategy, typeof(LumberWoodworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(SmallStandingLumberSignRecipe), Item.Get<SmallStandingLumberSignItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<SmallStandingLumberSignItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Small Standing Lumber Sign", typeof(SmallStandingLumberSignRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}