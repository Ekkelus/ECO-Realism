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
    public partial class LargeStandingLumberSignObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Standing Lumber Sign"); } } 

        public virtual Type RepresentedItemType { get { return typeof(LargeStandingLumberSignItem); } } 


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
    public partial class LargeStandingLumberSignItem :
        WorldObjectItem<LargeStandingLumberSignObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Standing Lumber Sign"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A large sign for all your large text needs!"); } }

        static LargeStandingLumberSignItem()
        {
            
        }

    }


    [RequiresSkill(typeof(LumberSkill), 3)]
    public partial class LargeStandingLumberSignRecipe : Recipe
    {
        public LargeStandingLumberSignRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LargeStandingLumberSignItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(LumberSkill), 15, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<LumberItem>(typeof(LumberSkill), 10, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(LumberSkill), 8, LumberSkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(10, LumberSkill.MultiplicativeStrategy, typeof(LumberSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(LargeStandingLumberSignRecipe), Item.Get<LargeStandingLumberSignItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<LargeStandingLumberSignItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Large Standing Lumber Sign"), typeof(LargeStandingLumberSignRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}