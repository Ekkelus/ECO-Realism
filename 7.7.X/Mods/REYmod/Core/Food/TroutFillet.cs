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
    public partial class TroutFilletItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Trout Fillet"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Some fine trout Fillet."); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 3, Protein = 10, Vitamins = 2 };
        public override float Calories { get { return 400; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(FishingSkill), 1)]
    public partial class TroutFilletRecipe : Recipe
    {
        public TroutFilletRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<TroutFilletItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
				new CraftingElement<TroutItem>(typeof(FishCleaningEfficiencySkill), 1, FishCleaningEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(TroutFilletRecipe), Item.Get<TroutFilletItem>().UILink(), 3, typeof(FishCleaningSpeedSkill));
            this.Initialize(Localizer.DoStr("Trout Fillet"), typeof(TroutFilletRecipe));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
}