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
    public partial class AdvancedBakingSkill : Skill
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Advanced Baking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Advanced baking mostly improves recipes that involve a leavening agent. Level by crafting advanced baking recipes."); } }

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
    public partial class AdvancedBakingSkillBook : SkillBook<AdvancedBakingSkill, AdvancedBakingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Advanced Baking Skill Book"); } }
    }

    [Serialized]
    public partial class AdvancedBakingSkillScroll : SkillScroll<AdvancedBakingSkill, AdvancedBakingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Advanced Baking Skill Scroll"); } }
    }

    [RequiresSkill(typeof(BakingSkill), 0)] 
    public partial class AdvancedBakingSkillBookRecipe : Recipe
    {
        public AdvancedBakingSkillBookRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<AdvancedBakingSkillBook>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BrickItem>(50),
                new CraftingElement<LumberItem>(25),
                new CraftingElement<YeastItem>(15),
                new CraftingElement<SugarItem>(15),
                new CraftingElement<BookItem>(8)
            };
            CraftMinutes = new ConstantValue(30);

            Initialize(Localizer.DoStr("Advanced Baking Skill Book"), typeof(AdvancedBakingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
