namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;
    using Eco.Shared.Localization;

    [Serialized]
    [Weight(500)]
    public partial class HoneyGlazedHamItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Honey Glazed Ham"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Delicious, mouth-watering dish."); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 12, Fat = 18, Protein = 14, Vitamins = 5 };
        public override float Calories { get { return 1050; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(CulinaryArtsSkill), 4)]
    public partial class HoneyGlazedHamRecipe : Recipe
    {
        public HoneyGlazedHamRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<HoneyGlazedHamItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HoneyItem>(typeof(CulinaryArtsEfficiencySkill), 5, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<VegetableMedleyItem>(typeof(CulinaryArtsEfficiencySkill), 1, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PrimeCutItem>(typeof(CulinaryArtsEfficiencySkill), 10, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<MeatStockItem>(typeof(CulinaryArtsEfficiencySkill), 5, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(HoneyGlazedHamRecipe), Item.Get<HoneyGlazedHamItem>().UILink(), 20, typeof(CulinaryArtsSpeedSkill));
            this.Initialize(Localizer.DoStr("Honey Glazed Ham"), typeof(HoneyGlazedHamRecipe));
            CraftingComponent.AddRecipe(typeof(StoveObject), this);
        }
    }
}