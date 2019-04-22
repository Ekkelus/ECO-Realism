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
    public partial class HoneyBunItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Honey Bun"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("One bite and you're hooked."); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 20, Fat = 15, Protein = 5, Vitamins = 5 };
        public override float Calories { get { return 850; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedBakingSkill), 3)]
    public partial class HoneyBunRecipe : Recipe
    {
        public HoneyBunRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<HoneyBunItem>(),

            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<HoneyItem>(typeof(AdvancedBakingSkill), 2, AdvancedBakingSkill.MultiplicativeStrategy),
                new CraftingElement<YeastItem>(typeof(AdvancedBakingSkill), 3, AdvancedBakingSkill.MultiplicativeStrategy),
                new CraftingElement<FlourItem>(typeof(AdvancedBakingSkill), 6, AdvancedBakingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(HoneyBunRecipe), Item.Get<HoneyBunItem>().UILink(), 10, typeof(AdvancedBakingSkill), typeof(AdvancedBakingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Honey Bun"), typeof(HoneyBunRecipe));
            CraftingComponent.AddRecipe(typeof(BakeryOvenObject), this);
        }
    }
}