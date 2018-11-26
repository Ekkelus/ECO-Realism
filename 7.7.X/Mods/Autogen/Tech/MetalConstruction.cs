// using Eco.Shared.Services;
// using Eco.Shared.Utils;
// using Gameplay.Systems.Tooltip;

// [Serialized]
// [RequiresSkill(typeof(SmithSkill), 0)]    
// public partial class MetalConstructionSkill : Skill
// {
// public override LocString DisplayName { get { return Localizer.DoStr("Metal Construction"); } }
// public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

// public static int[] SkillPointCost = { 1, 1, 1, 1, 1 };
// public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
// public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
// public override int MaxLevel { get { return 1; } }
// }

// [Serialized]
// public partial class MetalConstructionSkillBook : SkillBook<MetalConstructionSkill, MetalConstructionSkillScroll>
// {
// public override LocString DisplayName { get { return Localizer.DoStr("Metal Construction Skill Book"); } }
// }

// [Serialized]
// public partial class MetalConstructionSkillScroll : SkillScroll<MetalConstructionSkill, MetalConstructionSkillBook>
// {
// public override LocString DisplayName { get { return Localizer.DoStr("Metal Construction Skill Scroll"); } }
// }

// [RequiresSkill(typeof(AdvancedSmeltingSkill), 0)] 
// public partial class MetalConstructionSkillBookRecipe : Recipe
// {
// public MetalConstructionSkillBookRecipe()
// {
// this.Products = new CraftingElement[]
// {
// new CraftingElement<MetalConstructionSkillBook>(),
// };
// this.Ingredients = new CraftingElement[]
// {
// new CraftingElement<SteelItem>(typeof(ResearchEfficiencySkill), 50, ResearchEfficiencySkill.MultiplicativeStrategy), 
// };
// this.CraftMinutes = new ConstantValue(30);

// this.Initialize(Localizer.Do("Metal Construction Skill Book"), typeof(MetalConstructionSkillBookRecipe));
// CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
// }
// }
// }
