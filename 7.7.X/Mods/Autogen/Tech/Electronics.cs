namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.DynamicValues;
    using Gameplay.Items;
    using Gameplay.Players;
    using Gameplay.Skills;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]
    [RequiresSkill(typeof(EngineerSkill), 0)]
    public partial class ElectronicsSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Electronics"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Electronics improve the recipes for many advanced materials needed for advanced technology. Level by crafting related recipes."); } }

        public override void OnLevelUp(User user)
        {
            user.Skillset.AddExperience(typeof(SelfImprovementSkill), 20, Localizer.DoStr("for leveling up another specialization."));
        }


        public static ModificationStrategy MultiplicativeStrategy =
            new MultiplicativeStrategy(new[] { 1,

                1 - 0.5f,

                1 - 0.55f,

                1 - 0.6f,

                1 - 0.65f,

                1 - 0.7f,

                1 - 0.75f,

                1 - 0.8f,

            });
        public override ModificationStrategy MultiStrategy { get { return MultiplicativeStrategy; } }
        public static ModificationStrategy AdditiveStrategy =
            new AdditiveStrategy(new[] { 0,

                0.5f,

                0.55f,

                0.6f,

                0.65f,

                0.7f,

                0.75f,

                0.8f,

            });
        public override ModificationStrategy AddStrategy { get { return AdditiveStrategy; } }
        public static int[] SkillPointCost = {

            1,

            1,

            1,

            1,

            1,

            1,

            1,

        };
        public override int RequiredPoint { get { return Level < SkillPointCost.Length ? SkillPointCost[Level] : 0; } }
        public override int PrevRequiredPoint { get { return Level - 1 >= 0 && Level - 1 < SkillPointCost.Length ? SkillPointCost[Level - 1] : 0; } }
        public override int MaxLevel { get { return 7; } }
        public override int Tier { get { return 4; } }
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
            Products = new CraftingElement[]
            {
                new CraftingElement<ElectronicsSkillBook>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<CopperIngotItem>(50),
                new CraftingElement<GoldIngotItem>(50),
                new CraftingElement<BookItem>(12)
            };
            CraftMinutes = new ConstantValue(30);

            Initialize(Localizer.DoStr("Electronics Skill Book"), typeof(ElectronicsSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
