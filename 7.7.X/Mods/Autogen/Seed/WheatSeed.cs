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

    [RequiresSkill(typeof(FarmingSkill), 1)]    
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
                new CraftingElement<WheatItem>(typeof(FarmingSkill), 2, FarmingSkill.MultiplicativeStrategy),   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(WheatSeedRecipe), Item.Get<WheatSeedItem>().UILink(), 0.5f, typeof(FarmingSkill));

            this.Initialize(Localizer.DoStr("Wheat Seed"), typeof(WheatSeedRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}