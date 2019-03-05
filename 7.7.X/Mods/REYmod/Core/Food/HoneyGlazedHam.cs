namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Players;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Serialization;
    using Shared.Localization;

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

    [RequiresSkill(typeof(AdvancedCookingSkill), 4)]
    public partial class HoneyGlazedHamRecipe : Recipe
    {
        public HoneyGlazedHamRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<HoneyGlazedHamItem>(),

            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<HoneyItem>(typeof(AdvancedCookingSkill), 5, AdvancedCookingSkill.MultiplicativeStrategy),
                new CraftingElement<VegetableMedleyItem>(typeof(AdvancedCookingSkill), 1, AdvancedCookingSkill.MultiplicativeStrategy),
                new CraftingElement<PrimeCutItem>(typeof(AdvancedCookingSkill), 10, AdvancedCookingSkill.MultiplicativeStrategy),
                new CraftingElement<MeatStockItem>(typeof(AdvancedCookingSkill), 5, AdvancedCookingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(HoneyGlazedHamRecipe), Item.Get<HoneyGlazedHamItem>().UILink(), 20, typeof(AdvancedCookingSkill));
            Initialize(Localizer.DoStr("Honey Glazed Ham"), typeof(HoneyGlazedHamRecipe));
            CraftingComponent.AddRecipe(typeof(StoveObject), this);
        }
    }
}