namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Serialization;

    [Serialized]
    [Weight(10000)]
    public partial class TreasuryItem : WorldObjectItem<TreasuryObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Treasury"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Allows the setting of taxes."); } }

        static TreasuryItem()
        {
            
        }
        
    }


    [RequiresSkill(typeof(MetalworkingSkill), 2)]
    public partial class TreasuryRecipe : Recipe
    {
        public TreasuryRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<TreasuryItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HingeItem>(typeof(MetalworkingEfficiencySkill), 10, MetalworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(MetalworkingEfficiencySkill), 30, MetalworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GoldIngotItem>(typeof(MetalworkingEfficiencySkill), 10, MetalworkingEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(60, MetalworkingSpeedSkill.MultiplicativeStrategy, typeof(MetalworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(TreasuryRecipe), Item.Get<TreasuryItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<TreasuryItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Treasury", typeof(TreasuryRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}