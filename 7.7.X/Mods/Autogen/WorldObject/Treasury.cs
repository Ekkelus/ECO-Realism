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


    [RequiresSkill(typeof(SmeltingSkill), 2)]
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
                new CraftingElement<HingeItem>(typeof(SmeltingSkill), 10, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(SmeltingSkill), 30, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<GoldIngotItem>(typeof(SmeltingSkill), 10, SmeltingSkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(60, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(TreasuryRecipe), Item.Get<TreasuryItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<TreasuryItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Treasury"), typeof(TreasuryRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}