namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [RequiresSkill(typeof(ChefSkill), 0)]    
    public partial class AdvancedBakingSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Advanced Baking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        public static int[] SkillPointCost = { 1, 1, 1, 1, 1 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 1; } }
    }

    [Serialized]
    public partial class AdvancedBakingSkillBook : SkillBook<AdvancedBakingSkill, AdvancedBakingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Advanced Baking Skill Book"); } }
    }

    [Serialized]
    public partial class AdvancedBakingSkillScroll : SkillScroll<AdvancedBakingSkill, AdvancedBakingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Advanced Baking Skill Scroll"); } }
    }

    [RequiresSkill(typeof(BakingSkill), 0)] 
    public partial class AdvancedBakingSkillBookRecipe : Recipe
    {
        public AdvancedBakingSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<AdvancedBakingSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BrickItem>(typeof(ResearchEfficiencySkill), 50, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<LumberItem>(typeof(ResearchEfficiencySkill), 25, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SugarItem>(typeof(ResearchEfficiencySkill), 15, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BookItem>(typeof(ResearchEfficiencySkill), 8, ResearchEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(30);

            this.Initialize(Localizer.DoStr("Advanced Baking Skill Book"), typeof(AdvancedBakingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
