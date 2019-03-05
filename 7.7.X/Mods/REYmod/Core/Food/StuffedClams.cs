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
    public partial class StuffedClamsItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Stuffed Clams"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Clams filled with some nice ingredients"); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 10, Fat = 7, Protein =11, Vitamins = 6 };
        public override float Calories { get { return 800; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(CookingSkill), 4)]
    public partial class StuffedClamsRecipe : Recipe
    {
        public StuffedClamsRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<StuffedClamsItem>(),

            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<ClamItem>(typeof(CookingSkill), 10, CookingSkill.MultiplicativeStrategy),
                new CraftingElement<WheatItem>(typeof(CookingSkill), 20, CookingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(StuffedClamsRecipe), Item.Get<StuffedClamsItem>().UILink(), 15, typeof(CookingSkill));
            Initialize(Localizer.DoStr("Stuffed Clams"), typeof(StuffedClamsRecipe));
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
}