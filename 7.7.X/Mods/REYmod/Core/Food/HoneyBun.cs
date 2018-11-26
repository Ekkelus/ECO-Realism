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

    [RequiresSkill(typeof(LeavenedBakingSkill), 3)]
    public partial class HoneyBunRecipe : Recipe
    {
        public HoneyBunRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<HoneyBunItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HoneyItem>(typeof(LeavenedBakingEfficiencySkill), 2, LeavenedBakingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<YeastItem>(typeof(LeavenedBakingEfficiencySkill), 3, LeavenedBakingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<FlourItem>(typeof(LeavenedBakingEfficiencySkill), 6, LeavenedBakingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(HoneyBunRecipe), Item.Get<HoneyBunItem>().UILink(), 10, typeof(LeavenedBakingSpeedSkill));
            this.Initialize("Honey Bun", typeof(HoneyBunRecipe));
            CraftingComponent.AddRecipe(typeof(BakeryOvenObject), this);
        }
    }
}