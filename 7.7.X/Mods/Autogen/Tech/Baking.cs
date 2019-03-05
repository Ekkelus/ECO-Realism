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
    public partial class BakingSkill : Skill
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Baking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("An introduction to cooking with an oven. Level by crafting related unleavened recipes."); } }

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
    public partial class BakingSkillBook : SkillBook<BakingSkill, BakingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Baking Skill Book"); } }
    }

    [Serialized]
    public partial class BakingSkillScroll : SkillScroll<BakingSkill, BakingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Baking Skill Scroll"); } }
    }

    [RequiresSkill(typeof(CookingSkill), 0)] 
    public partial class BakingSkillBookRecipe : Recipe
    {
        public BakingSkillBookRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<BakingSkillBook>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BrickItem>(30),
                new CraftingElement<HewnLogItem>(30),
                new CraftingElement<FlourItem>(50),
                new CraftingElement<BannockItem>(20),
                new CraftingElement<BookItem>(6)
            };
            CraftMinutes = new ConstantValue(15);

            Initialize(Localizer.DoStr("Baking Skill Book"), typeof(BakingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
