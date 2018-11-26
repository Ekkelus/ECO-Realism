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
    public partial class OilDrillingSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Oil Drilling"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        public static int[] SkillPointCost = { 1, 1, 1, 1, 1 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 1; } }
    }

    [Serialized]
    public partial class OilDrillingSkillBook : SkillBook<OilDrillingSkill, OilDrillingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Oil Drilling Skill Book"); } }
    }

    [Serialized]
    public partial class OilDrillingSkillScroll : SkillScroll<OilDrillingSkill, OilDrillingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Oil Drilling Skill Scroll"); } }
    }

    [RequiresSkill(typeof(MechanicsSkill), 0)] 
    public partial class OilDrillingSkillBookRecipe : Recipe
    {
        public OilDrillingSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<OilDrillingSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ReinforcedConcreteItem>(typeof(ResearchEfficiencySkill), 50, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CombustionEngineItem>(typeof(ResearchEfficiencySkill), 4, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<LumberItem>(typeof(ResearchEfficiencySkill), 80, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BookItem>(typeof(ResearchEfficiencySkill), 16, ResearchEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(30);

            this.Initialize(Localizer.Do("Oil Drilling Skill Book"), typeof(OilDrillingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
