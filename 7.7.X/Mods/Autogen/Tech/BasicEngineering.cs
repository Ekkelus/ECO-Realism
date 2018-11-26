namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [RequiresSkill(typeof(EngineerSkill), 0)]    
    public partial class BasicEngineeringSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Basic Engineering"); } }
        public override string Description { get { return Localizer.DoStr(""); } }

        public static int[] SkillPointCost = { 1, 1, 1, 1, 1 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 1; } }
    }

    [Serialized]
    public partial class BasicEngineeringSkillBook : SkillBook<BasicEngineeringSkill, BasicEngineeringSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Basic Engineering Skill Book"); } }
    }

    [Serialized]
    public partial class BasicEngineeringSkillScroll : SkillScroll<BasicEngineeringSkill, BasicEngineeringSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Basic Engineering Skill Scroll"); } }
    }

    public partial class BasicEngineeringSkillBookRecipe : Recipe
    {
        public BasicEngineeringSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BasicEngineeringSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(ResearchEfficiencySkill), 10, ResearchEfficiencySkill.MultiplicativeStrategy), 
            };
            this.CraftMinutes = new ConstantValue(5);

            this.Initialize("Basic Engineering Skill Book", typeof(BasicEngineeringSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
