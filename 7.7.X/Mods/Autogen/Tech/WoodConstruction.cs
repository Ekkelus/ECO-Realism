// using Eco.Shared.Services;
// using Eco.Shared.Utils;
// using Gameplay.Systems.Tooltip;

// [Serialized]
// [RequiresSkill(typeof(CarpenterSkill), 0)]    
// public partial class WoodConstructionSkill : Skill
// {
// public override LocString DisplayName { get { return Localizer.DoStr("Wood Construction"); } }
// public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

// public override int RequiredPoint { get { return 0; } }
// public override int MaxLevel { get { return 1; } }
// }

// [Serialized]
// public partial class WoodConstructionSkillBook : SkillBook<WoodConstructionSkill, WoodConstructionSkillScroll>
// {
// public override LocString DisplayName { get { return Localizer.DoStr("Wood Construction Skill Book"); } }
// }

// [Serialized]
// public partial class WoodConstructionSkillScroll : SkillScroll<WoodConstructionSkill, WoodConstructionSkillBook>
// {
// public override LocString DisplayName { get { return Localizer.DoStr("Wood Construction Skill Scroll"); } }
// }

// [RequiresSkill(typeof(HewingSkill), 0)] 
// public partial class WoodConstructionSkillBookRecipe : Recipe
// {
// public WoodConstructionSkillBookRecipe()
// {
// this.Products = new CraftingElement[]
// {
// new CraftingElement<WoodConstructionSkillBook>(),
// };
// this.Ingredients = new CraftingElement[]
// {
// new CraftingElement<HewnLogItem>(typeof(ResearchEfficiencySkill), 10, ResearchEfficiencySkill.MultiplicativeStrategy), 
// };
// this.CraftMinutes = new ConstantValue(5);

// this.Initialize(Localizer.DoStr("Wood Construction Skill Book"), typeof(WoodConstructionSkillBookRecipe));
// CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
// }
// }
// }
