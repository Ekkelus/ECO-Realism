namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [RequiresSkill(typeof(CarpenterSkill), 0)]    
    public partial class LumberSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lumber"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        public override int RequiredPoint { get { return 0; } }
        public override int MaxLevel { get { return 1; } }
    }

    [Serialized]
    public partial class LumberSkillBook : SkillBook<LumberSkill, LumberSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("LumberSkill Book"); } }
    }

    [Serialized]
    public partial class LumberSkillScroll : SkillScroll<LumberSkill, LumberSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("LumberSkill Scroll"); } }
    }

    public partial class LumberSkillBookRecipe : Recipe
    {
        public LumberSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LumberSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(ResearchEfficiencySkill), 20, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<HewnLogItem>(typeof(ResearchEfficiencySkill), 40, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<ClothItem>(typeof(ResearchEfficiencySkill), 20, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BookItem>(typeof(ResearchEfficiencySkill), 4, ResearchEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(15);

            this.Initialize(Localizer.DoStr("LumberSkill Book"), typeof(LumberSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
