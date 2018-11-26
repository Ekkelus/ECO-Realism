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
    public partial class SmallHangingStoneSignObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Hanging Stone Sign"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SmallHangingStoneSignItem); } } 


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
    public partial class SmallHangingStoneSignItem :
        WorldObjectItem<SmallHangingStoneSignObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Hanging Stone Sign"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A small sign for all of your smaller text needs!"); } }

        static SmallHangingStoneSignItem()
        {
            
        }

    }


    [RequiresSkill(typeof(StoneworkingSkill), 2)]
    public partial class SmallHangingStoneSignRecipe : Recipe
    {
        public SmallHangingStoneSignRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SmallHangingStoneSignItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(StoneworkingEfficiencySkill), 30, StoneworkingEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(10, StoneworkingSpeedSkill.MultiplicativeStrategy, typeof(StoneworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(SmallHangingStoneSignRecipe), Item.Get<SmallHangingStoneSignItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<SmallHangingStoneSignItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.Do("Small Hanging Stone Sign"), typeof(SmallHangingStoneSignRecipe));
            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }
}