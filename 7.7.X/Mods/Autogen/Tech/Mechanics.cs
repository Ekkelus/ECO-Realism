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
    public partial class MechanicsSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mechanics"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        public static int[] SkillPointCost = { 1, 1, 1, 1, 1 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 1; } }
    }

    [Serialized]
    public partial class MechanicsSkillBook : SkillBook<MechanicsSkill, MechanicsSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mechanics Skill Book"); } }
    }

    [Serialized]
    public partial class MechanicsSkillScroll : SkillScroll<MechanicsSkill, MechanicsSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mechanics Skill Scroll"); } }
    }

    [RequiresSkill(typeof(BasicEngineeringSkill), 0)] 
    public partial class MechanicsSkillBookRecipe : Recipe
    {
        public MechanicsSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<MechanicsSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<WaterwheelItem>(typeof(ResearchEfficiencySkill), 5, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CopperIngotItem>(typeof(ResearchEfficiencySkill), 20, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<IronIngotItem>(typeof(ResearchEfficiencySkill), 20, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BookItem>(typeof(ResearchEfficiencySkill), 4, ResearchEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(30);

            this.Initialize(Localizer.DoStr("Mechanics Skill Book"), typeof(MechanicsSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
