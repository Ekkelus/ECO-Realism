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
    public partial class BasicEngineeringSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Basic Engineering"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Basic Engineering allows for the easier construction of roads and early forms of mechanical power. Level by crafting related recipes."); } }

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
        public override int Tier { get { return 2; } }
    }

    [Serialized]
    public partial class BasicEngineeringSkillBook : SkillBook<BasicEngineeringSkill, BasicEngineeringSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Basic Engineering Skill Book"); } }
    }

    [Serialized]
    public partial class BasicEngineeringSkillScroll : SkillScroll<BasicEngineeringSkill, BasicEngineeringSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Basic Engineering Skill Scroll"); } }
    }

    public partial class BasicEngineeringSkillBookRecipe : Recipe
    {
        public BasicEngineeringSkillBookRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<BasicEngineeringSkillBook>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(10)
            };
            CraftMinutes = new ConstantValue(5);

            Initialize(Localizer.DoStr("Basic Engineering Skill Book"), typeof(BasicEngineeringSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
