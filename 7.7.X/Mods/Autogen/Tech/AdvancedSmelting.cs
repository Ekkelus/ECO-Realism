namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [RequiresSkill(typeof(SmithSkill), 0)]    
    public partial class AdvancedSmeltingSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Advanced Smelting"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        public static int[] SkillPointCost = { 1, 1, 1, 1, 1 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 1; } }
    }

    [Serialized]
    public partial class AdvancedSmeltingSkillBook : SkillBook<AdvancedSmeltingSkill, AdvancedSmeltingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Advanced Smelting Skill Book"); } }
    }

    [Serialized]
    public partial class AdvancedSmeltingSkillScroll : SkillScroll<AdvancedSmeltingSkill, AdvancedSmeltingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Advanced Smelting Skill Scroll"); } }
    }

    [RequiresSkill(typeof(SmeltingSkill), 0)] 
    public partial class AdvancedSmeltingSkillBookRecipe : Recipe
    {
        public AdvancedSmeltingSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<AdvancedSmeltingSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CoalItem>(typeof(ResearchEfficiencySkill), 200, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<IronIngotItem>(typeof(ResearchEfficiencySkill), 100, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BrickItem>(typeof(ResearchEfficiencySkill), 100, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BookItem>(typeof(ResearchEfficiencySkill), 4, ResearchEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(30);

            this.Initialize(Localizer.Do("Advanced Smelting Skill Book"), typeof(AdvancedSmeltingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
