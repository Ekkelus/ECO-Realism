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
    public partial class LargeHangingLumberSignObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Hanging Lumber Sign"); } } 

        public virtual Type RepresentedItemType { get { return typeof(LargeHangingLumberSignItem); } } 


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
    public partial class LargeHangingLumberSignItem :
        WorldObjectItem<LargeHangingLumberSignObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Hanging Lumber Sign"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A large sign for all your large text needs!"); } }

        static LargeHangingLumberSignItem()
        {
            
        }

    }


    [RequiresSkill(typeof(LumberSkill), 4)]
    public partial class LargeHangingLumberSignRecipe : Recipe
    {
        public LargeHangingLumberSignRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LargeHangingLumberSignItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(LumberSkill), 15, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<LumberItem>(typeof(LumberSkill), 10, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(LumberSkill), 8, LumberSkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(10, LumberSkill.MultiplicativeStrategy, typeof(LumberSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(LargeHangingLumberSignRecipe), Item.Get<LargeHangingLumberSignItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<LargeHangingLumberSignItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Large Hanging Lumber Sign"), typeof(LargeHangingLumberSignRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}