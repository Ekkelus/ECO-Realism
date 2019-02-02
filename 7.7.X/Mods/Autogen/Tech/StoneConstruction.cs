// using Eco.Shared.Services;
// using Eco.Shared.Utils;
// using Gameplay.Systems.Tooltip;

// [Serialized]
// [RequiresSkill(typeof(MasonSkill), 0)]    
// public partial class StoneConstructionSkill : Skill
// {
// public override LocString DisplayName { get { return Localizer.DoStr("Stone Construction"); } }
// public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

// public static int[] SkillPointCost = { 1, 1, 1, 1, 1 };
// public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
// public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
// public override int MaxLevel { get { return 1; } }
// }

// [Serialized]
// public partial class StoneConstructionSkillBook : SkillBook<StoneConstructionSkill, StoneConstructionSkillScroll>
// {
// public override LocString DisplayName { get { return Localizer.DoStr("Stone Construction Skill Book"); } }
// }

// [Serialized]
// public partial class StoneConstructionSkillScroll : SkillScroll<StoneConstructionSkill, StoneConstructionSkillBook>
// {
// public override LocString DisplayName { get { return Localizer.DoStr("Stone Construction Skill Scroll"); } }
// }

// [RequiresSkill(typeof(MortaringSkill), 0)] 
// public partial class StoneConstructionSkillBookRecipe : Recipe
// {
// public StoneConstructionSkillBookRecipe()
// {
// this.Products = new CraftingElement[]
// {
// new CraftingElement<StoneConstructionSkillBook>(),
// };
// this.Ingredients = new CraftingElement[]
// {
// new CraftingElement<MortaredStoneItem>(typeof(ResearchEfficiencySkill), 10, ResearchEfficiencySkill.MultiplicativeStrategy), 
// };
// this.CraftMinutes = new ConstantValue(5);

// this.Initialize(Localizer.DoStr("Stone Construction Skill Book"), typeof(StoneConstructionSkillBookRecipe));
// CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
// }
// }
// }
