namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Serialization;

    [Serialized]
    [RequiresSkill(typeof(SkinningSkill), 1)]
    public partial class SkinningEfficiencySkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Skinning Efficiency"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        public static ModificationStrategy MultiplicativeStrategy =
            new MultiplicativeStrategy(new float[] { 1, 1/(1 - 0.1f), 1 / (1 - 0.2f), 1 / (1 - 0.3f), 1 / (1 - 0.4f), 1 / (1 - 0.5f), 1 / (1 - 0.55f), 1 / (1 - 0.6f), 1 / (1 - 0.65f), 1 / (1 - 0.7f), 1 / (1 - 0.75f) });
        public static ModificationStrategy AdditiveStrategy =
            new AdditiveStrategy(new float[] { 0, 0.2f, 0.35f, 0.5f, 0.65f, 0.8f });
        public static int[] SkillPointCost = { 2, 4, 6, 8, 10, 10, 10, 10, 10, 10 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 5; } }

    }

}