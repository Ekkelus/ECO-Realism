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
    public partial class ElectronicsSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Electronics"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        public static int[] SkillPointCost = { 1, 1, 1, 1, 1 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 1; } }
    }

    [Serialized]
    public partial class ElectronicsSkillBook : SkillBook<ElectronicsSkill, ElectronicsSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Electronics Skill Book"); } }
    }

    [Serialized]
    public partial class ElectronicsSkillScroll : SkillScroll<ElectronicsSkill, ElectronicsSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Electronics Skill Scroll"); } }
    }

    [RequiresSkill(typeof(MechanicsSkill), 0)] 
    public partial class ElectronicsSkillBookRecipe : Recipe
    {
        public ElectronicsSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ElectronicsSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CopperIngotItem>(typeof(ResearchEfficiencySkill), 50, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GoldIngotItem>(typeof(ResearchEfficiencySkill), 50, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BookItem>(typeof(ResearchEfficiencySkill), 12, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(ResearchEfficiencySkill), 20, ResearchEfficiencySkill.MultiplicativeStrategy), 
            };
            this.CraftMinutes = new ConstantValue(30);

            this.Initialize(Localizer.Do("Electronics Skill Book"), typeof(ElectronicsSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
