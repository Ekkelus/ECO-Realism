// using Eco.Shared.Services;
// using Eco.Shared.Utils;
// using Gameplay.Systems.Tooltip;

// [Serialized]
// [RequiresSkill(typeof(GlassworkingSkill), 1)]    
// public partial class GlassConstructionSkill : Skill
// {
// public override LocString DisplayName { get { return Localizer.DoStr("Glass Construction"); } }
// public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

// public static ModificationStrategy MultiplicativeStrategy = 
// new MultiplicativeStrategy(new float[] { 1, 1 - 0.2f, 1 - 0.35f, 1 - 0.5f, 1 - 0.65f, 1 - 0.8f });
// public static ModificationStrategy AdditiveStrategy =
// new AdditiveStrategy(new float[] { 0, 0.2f, 0.35f, 0.5f, 0.65f, 0.8f });
// public static int[] SkillPointCost = { 6, 8, 10, 12, 14 };
// public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
// public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
// public override int MaxLevel { get { return 4; } }
// }

// }
