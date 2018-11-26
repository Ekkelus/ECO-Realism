namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [RequiresSkill(typeof(FarmerSkill), 0)]    
    public partial class FertilizersSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Fertilizers"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        public static int[] SkillPointCost = { 1, 1, 1, 1, 1 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 1; } }
    }

    [Serialized]
    public partial class FertilizersSkillBook : SkillBook<FertilizersSkill, FertilizersSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Fertilizers Skill Book"); } }
    }

    [Serialized]
    public partial class FertilizersSkillScroll : SkillScroll<FertilizersSkill, FertilizersSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Fertilizers Skill Scroll"); } }
    }

    [RequiresSkill(typeof(FarmingSkill), 0)] 
    public partial class FertilizersSkillBookRecipe : Recipe
    {
        public FertilizersSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<FertilizersSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<DirtItem>(typeof(ResearchEfficiencySkill), 10, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PlantFibersItem>(typeof(ResearchEfficiencySkill), 30, ResearchEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(5);

            this.Initialize(Localizer.Do("Fertilizers Skill Book"), typeof(FertilizersSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
