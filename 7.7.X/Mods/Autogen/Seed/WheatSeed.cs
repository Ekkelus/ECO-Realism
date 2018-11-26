namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Gameplay.Players;
    using System.ComponentModel;

    [Serialized]
    [Weight(10)]  
    public partial class WheatSeedItem : SeedItem
    {
        static WheatSeedItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Wheat Seed"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow wheat."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Wheat"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class WheatSeedPackItem : SeedPackItem
    {
        static WheatSeedPackItem() { }

        public override LocString DisplayName { get { return Localizer.DoStr("Wheat Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow wheat."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Wheat"); } }
    }

    [RequiresSkill(typeof(SeedProductionSkill), 1)]    
    public class WheatSeedRecipe : Recipe
    {
        public WheatSeedRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WheatSeedItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<WheatItem>(typeof(SeedProductionEfficiencySkill), 2, SeedProductionEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(0.5f, SeedProductionSpeedSkill.MultiplicativeStrategy, typeof(SeedProductionSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(WheatSeedRecipe), Item.Get<WheatSeedItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<WheatSeedItem>().UILink(), value);
            this.CraftMinutes = value;

            this.Initialize(Localizer.DoStr("Wheat Seed"), typeof(WheatSeedRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}