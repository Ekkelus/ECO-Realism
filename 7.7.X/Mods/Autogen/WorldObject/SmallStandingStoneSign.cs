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
    public partial class SmallStandingStoneSignObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Standing Stone Sign"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SmallStandingStoneSignItem); } } 


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
    public partial class SmallStandingStoneSignItem :
        WorldObjectItem<SmallStandingStoneSignObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Standing Stone Sign"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A small sign for all of your smaller text needs!"); } }

        static SmallStandingStoneSignItem()
        {
            
        }

    }


    [RequiresSkill(typeof(StoneworkingSkill), 1)]
    public partial class SmallStandingStoneSignRecipe : Recipe
    {
        public SmallStandingStoneSignRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SmallStandingStoneSignItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(StoneworkingEfficiencySkill), 30, StoneworkingEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(10, StoneworkingSpeedSkill.MultiplicativeStrategy, typeof(StoneworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(SmallStandingStoneSignRecipe), Item.Get<SmallStandingStoneSignItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<SmallStandingStoneSignItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Small Standing Stone Sign", typeof(SmallStandingStoneSignRecipe));
            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }
}