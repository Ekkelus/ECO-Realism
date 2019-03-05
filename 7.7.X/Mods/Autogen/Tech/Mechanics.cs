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
    public partial class MechanicsSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mechanics"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Mechanics for more advanced creations. Level by crafting related recipes."); } }

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
            Products = new CraftingElement[]
            {
                new CraftingElement<MechanicsSkillBook>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<WaterwheelItem>(5),
                new CraftingElement<CopperIngotItem>(20),
                new CraftingElement<IronIngotItem>(20),
                new CraftingElement<BookItem>(4)
            };
            CraftMinutes = new ConstantValue(30);

            Initialize(Localizer.DoStr("Mechanics Skill Book"), typeof(MechanicsSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
