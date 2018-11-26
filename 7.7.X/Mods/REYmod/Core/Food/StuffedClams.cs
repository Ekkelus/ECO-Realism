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
    public partial class StuffedClamsItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Stuffed Clams"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Clams filled with some nice ingredients"); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 10, Fat = 7, Protein =11, Vitamins = 6 };
        public override float Calories { get { return 800; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(HomeCookingSkill), 4)]
    public partial class StuffedClamsRecipe : Recipe
    {
        public StuffedClamsRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<StuffedClamsItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ClamItem>(typeof(HomeCookingEfficiencySkill), 10, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<WheatItem>(typeof(HomeCookingEfficiencySkill), 20, HomeCookingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(StuffedClamsRecipe), Item.Get<StuffedClamsItem>().UILink(), 15, typeof(HomeCookingSpeedSkill));
            this.Initialize(Localizer.Do("Stuffed Clams"), typeof(StuffedClamsRecipe));
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
}