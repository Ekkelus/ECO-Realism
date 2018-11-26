namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [RequiresSkill(typeof(BigStomachSkill), 1)]    
    public partial class CalorieEfficiencySkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Calorie Efficiency"); } }
        public override string Description { get { return Localizer.DoStr(""); } }

        public static ModificationStrategy MultiplicativeStrategy = 
            new MultiplicativeStrategy(new float[] { 1, 1 - 0.05f, 1 - 0.1f, 1 - 0.15f, 1 - 0.2f, 1 - 0.25f });
        public static ModificationStrategy AdditiveStrategy =
            new AdditiveStrategy(new float[] { 0, 0.05f, 0.1f, 0.15f, 0.2f, 0.25f });
        public static int[] SkillPointCost = { 1, 5, 15, 30, 50 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 5; } }
    }

}
