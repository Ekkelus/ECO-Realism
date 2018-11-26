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
    public partial class SalmonFilletItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Salmon Fillet"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Some fine Salmon Fillet."); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 4, Protein = 11, Vitamins = 3 };
        public override float Calories { get { return 450; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(FishingSkill), 2)]
    public partial class SalmonFilletRecipe : Recipe
    {
        public SalmonFilletRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SalmonFilletItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
				new CraftingElement<SalmonItem>(typeof(FishCleaningEfficiencySkill), 1, FishCleaningEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(SalmonFilletRecipe), Item.Get<SalmonFilletItem>().UILink(), 3, typeof(FishCleaningSpeedSkill));
            this.Initialize("Salmon Fillet", typeof(SalmonFilletRecipe));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
}