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
    [RequiresSkill(typeof(FarmerSkill), 0)]
    public partial class FarmingSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Farming"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("The art of planting and cultivating flora. Level by crafting related recipes and using the hoe."); } }

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

        public override void OnLearned()
        {
            base.OnLearned();
            this.ForceSetLevel(0);
        }
    }

    [Serialized]
    public partial class FarmingSkillBook : SkillBook<FarmingSkill, FarmingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Farming Skill Book"); } }
    }

    [Serialized]
    public partial class FarmingSkillScroll : SkillScroll<FarmingSkill, FarmingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Farming Skill Scroll"); } }
    }

    [RequiresSkill(typeof(GatheringSkill), 0)]
    public partial class FarmingSkillBookRecipe : Recipe
    {
        public FarmingSkillBookRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<FarmingSkillBook>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<HuckleberriesItem>(10),
                new CraftingElement<CornItem>(10),
                new CraftingElement<CriminiMushroomsItem>(5)
            };
            CraftMinutes = new ConstantValue(5);

            Initialize(Localizer.DoStr("Farming Skill Book"), typeof(FarmingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
