namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;
    using Shared.Serialization;
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
        public override LocString DisplayName { get { return Localizer.DoStr("Wheat Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow wheat."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Wheat"); } }
    }

    [RequiresSkill(typeof(FarmingSkill), 1)]    
    public class WheatSeedRecipe : Recipe
    {
        public WheatSeedRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<WheatSeedItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<WheatItem>(typeof(FarmingSkill), 2, FarmingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(WheatSeedRecipe), Item.Get<WheatSeedItem>().UILink(), 0.5f, typeof(FarmingSkill));

            Initialize(Localizer.DoStr("Wheat Seed"), typeof(WheatSeedRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}