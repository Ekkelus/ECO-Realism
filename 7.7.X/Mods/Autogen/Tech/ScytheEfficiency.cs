namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [RequiresSkill(typeof(GatheringSkill), 1)]    
    public partial class ScytheEfficiencySkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Scythe Efficiency"); } }
        public override string Description { get { return Localizer.DoStr(""); } }

        public static ModificationStrategy MultiplicativeStrategy = 
            new MultiplicativeStrategy(new float[] { 1, 1 - 0.2f, 1 - 0.35f, 1 - 0.5f, 1 - 0.65f, 1 - 0.8f });
        public static ModificationStrategy AdditiveStrategy =
            new AdditiveStrategy(new float[] { 0, 0.2f, 0.35f, 0.5f, 0.65f, 0.8f });
        public static int[] SkillPointCost = { 1, 5, 15, 30, 50 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 5; } }
    }

}
