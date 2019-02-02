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
    public partial class AdvancedCampfireCookingSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Advanced Campfire Cooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        public static int[] SkillPointCost = { 1, 1, 1, 1, 1 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 1; } }
    }

    [Serialized]
    public partial class AdvancedCampfireCookingSkillBook : SkillBook<AdvancedCampfireCookingSkill, AdvancedCampfireCookingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Advanced Campfire Cooking Skill Book"); } }
    }

    [Serialized]
    public partial class AdvancedCampfireCookingSkillScroll : SkillScroll<AdvancedCampfireCookingSkill, AdvancedCampfireCookingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Advanced Campfire Cooking Skill Scroll"); } }
    }

    [RequiresSkill(typeof(GatheringSkill), 0)] 
    public partial class AdvancedCampfireCookingSkillBookRecipe : Recipe
    {
        public AdvancedCampfireCookingSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<AdvancedCampfireCookingSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CharredCornItem>(typeof(ResearchEfficiencySkill), 10, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CharredTomatoItem>(typeof(ResearchEfficiencySkill), 10, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CharredCamasBulbItem>(typeof(ResearchEfficiencySkill), 5, ResearchEfficiencySkill.MultiplicativeStrategy), 
            };
            this.CraftMinutes = new ConstantValue(5);

            this.Initialize(Localizer.DoStr("Advanced Campfire Cooking Skill Book"), typeof(AdvancedCampfireCookingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
