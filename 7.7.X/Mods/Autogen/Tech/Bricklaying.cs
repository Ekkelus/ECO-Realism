namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [RequiresSkill(typeof(MasonSkill), 0)]    
    public partial class BricklayingSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Bricklaying"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        public static int[] SkillPointCost = { 1, 1, 1, 1, 1 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 1; } }
    }

    [Serialized]
    public partial class BricklayingSkillBook : SkillBook<BricklayingSkill, BricklayingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Bricklaying Skill Book"); } }
    }

    [Serialized]
    public partial class BricklayingSkillScroll : SkillScroll<BricklayingSkill, BricklayingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Bricklaying Skill Scroll"); } }
    }

    public partial class BricklayingSkillBookRecipe : Recipe
    {
        public BricklayingSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BricklayingSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<MortaredStoneItem>(typeof(ResearchEfficiencySkill), 50, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PitchItem>(typeof(ResearchEfficiencySkill), 50, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<ClothItem>(typeof(ResearchEfficiencySkill), 20, ResearchEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(15);

            this.Initialize(Localizer.Do("Bricklaying Skill Book"), typeof(BricklayingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
