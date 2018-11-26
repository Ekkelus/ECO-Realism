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
    public partial class SmokedTroutSaladItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Smoked Trout Salad"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Yummy Salad"); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 7, Fat = 7, Protein = 13, Vitamins = 14 };
        public override float Calories { get { return 800; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(HomeCookingSkill), 4)]
    public partial class SmokedTroutSaladRecipe : Recipe
    {
        public SmokedTroutSaladRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SmokedTroutSaladItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TroutFilletItem>(typeof(HomeCookingEfficiencySkill), 20, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CriminiMushroomsItem>(typeof(HomeCookingEfficiencySkill), 20, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<FiddleheadsItem>(typeof(HomeCookingEfficiencySkill), 20, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PricklyPearFruitItem>(typeof(HomeCookingEfficiencySkill), 20, HomeCookingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(SmokedTroutSaladRecipe), Item.Get<SmokedTroutSaladItem>().UILink(), 10, typeof(HomeCookingSpeedSkill));
            this.Initialize("Smoked Trout Salad", typeof(SmokedTroutSaladRecipe));
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
}