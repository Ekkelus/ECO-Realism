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
    public partial class SmallHangingLumberSignObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Hanging Lumber Sign"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SmallHangingLumberSignItem); } } 


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
    public partial class SmallHangingLumberSignItem :
        WorldObjectItem<SmallHangingLumberSignObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Hanging Lumber Sign"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A small sign for all of your smaller text needs!"); } }

        static SmallHangingLumberSignItem()
        {
            
        }

    }


    [RequiresSkill(typeof(LumberWoodworkingSkill), 2)]
    public partial class SmallHangingLumberSignRecipe : Recipe
    {
        public SmallHangingLumberSignRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SmallHangingLumberSignItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(LumberWoodworkingEfficiencySkill), 10, LumberWoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<LumberItem>(typeof(LumberWoodworkingEfficiencySkill), 4, LumberWoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(LumberWoodworkingEfficiencySkill), 4, LumberWoodworkingEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(10, LumberWoodworkingSpeedSkill.MultiplicativeStrategy, typeof(LumberWoodworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(SmallHangingLumberSignRecipe), Item.Get<SmallHangingLumberSignItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<SmallHangingLumberSignItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.Do("Small Hanging Lumber Sign"), typeof(SmallHangingLumberSignRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}