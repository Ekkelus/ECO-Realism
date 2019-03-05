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
    public partial class IndustrySkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Industry"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Advanced industrialization to produce larger machines and advance technology. Level by crafting related recipes."); } }

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
        public override int Tier { get { return 5; } }
    }

    [Serialized]
    public partial class IndustrySkillBook : SkillBook<IndustrySkill, IndustrySkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Industry Skill Book"); } }
    }

    [Serialized]
    public partial class IndustrySkillScroll : SkillScroll<IndustrySkill, IndustrySkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Industry Skill Scroll"); } }
    }

    [RequiresSkill(typeof(MechanicsSkill), 0)]
    public partial class IndustrySkillBookRecipe : Recipe
    {
        public IndustrySkillBookRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<IndustrySkillBook>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(100),
                new CraftingElement<BookItem>(16)
            };
            CraftMinutes = new ConstantValue(30);

            Initialize(Localizer.DoStr("Industry Skill Book"), typeof(IndustrySkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
