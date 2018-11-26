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
    public partial class CuttingEdgeCookingSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cutting Edge Cooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        public static int[] SkillPointCost = { 1, 1, 1, 1, 1 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 1; } }
    }

    [Serialized]
    public partial class CuttingEdgeCookingSkillBook : SkillBook<CuttingEdgeCookingSkill, CuttingEdgeCookingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cutting Edge Cooking Skill Book"); } }
    }

    [Serialized]
    public partial class CuttingEdgeCookingSkillScroll : SkillScroll<CuttingEdgeCookingSkill, CuttingEdgeCookingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cutting Edge Cooking Skill Scroll"); } }
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 0)] 
    public partial class CuttingEdgeCookingSkillBookRecipe : Recipe
    {
        public CuttingEdgeCookingSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CuttingEdgeCookingSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(ResearchEfficiencySkill), 50, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<YeastItem>(typeof(ResearchEfficiencySkill), 50, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BookItem>(typeof(ResearchEfficiencySkill), 10, ResearchEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(60);

            this.Initialize(Localizer.Do("Cutting Edge Cooking Skill Book"), typeof(CuttingEdgeCookingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
