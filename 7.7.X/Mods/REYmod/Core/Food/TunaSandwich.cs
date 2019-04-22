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
    [Weight(200)]
    public partial class TunaSandwichItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tuna Sandwich"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Tuna Sandwich! Delicious!"); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 10, Fat = 12, Protein = 13, Vitamins =9 };
        public override float Calories { get { return 800; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 1)]
    public partial class TunaSandwichRecipe : Recipe
    {
        public TunaSandwichRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<TunaSandwichItem>(),

            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<TunaFilletItem>(typeof(AdvancedCookingSkill), 5, AdvancedCookingSkill.MultiplicativeStrategy),
                new CraftingElement<BreadItem>(typeof(AdvancedCookingSkill), 1, AdvancedCookingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(TunaSandwichRecipe), Item.Get<TunaSandwichItem>().UILink(), 10, typeof(AdvancedCookingSkill), typeof(AdvancedCookingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Tuna Sandwich"), typeof(TunaSandwichRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}