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
    [RequiresSkill(typeof(ChefSkill), 0)]
    public partial class CookingSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("The basics of cooking in a more civilized enviornment give bonuses to a variety of food recipes. Level by crafting related recipes."); } }

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
        public override int Tier { get { return 3; } }
    }

    [Serialized]
    public partial class CookingSkillBook : SkillBook<CookingSkill, CookingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cooking Skill Book"); } }
    }

    [Serialized]
    public partial class CookingSkillScroll : SkillScroll<CookingSkill, CookingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cooking Skill Scroll"); } }
    }

    [RequiresSkill(typeof(AdvancedCampfireCookingSkill), 0)]
    public partial class CookingSkillBookRecipe : Recipe
    {
        public CookingSkillBookRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<CookingSkillBook>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(25),
                new CraftingElement<HewnLogItem>(20),
                new CraftingElement<CampfireRoastItem>(10),
                new CraftingElement<WheatPorridgeItem>(10),
                new CraftingElement<BookItem>(4)
            };
            CraftMinutes = new ConstantValue(15);

            Initialize(Localizer.DoStr("Cooking Skill Book"), typeof(CookingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
