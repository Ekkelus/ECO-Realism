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
    public partial class ClamChowderItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Clam Chowder"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Some soup with clams"); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 4, Fat = 4, Protein = 10, Vitamins = 12 };
        public override float Calories { get { return 1000; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(CookingSkill), 2)]
    public partial class ClamChowderRecipe : Recipe
    {
        public ClamChowderRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<ClamChowderItem>(),

            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<ClamItem>(typeof(CookingSkill), 10, CookingSkill.MultiplicativeStrategy),
                new CraftingElement<FiddleheadsItem>(typeof(CookingSkill), 20, CookingSkill.MultiplicativeStrategy),
                new CraftingElement<CriminiMushroomsItem>(typeof(CookingSkill), 20, CookingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(ClamChowderRecipe), Item.Get<ClamChowderItem>().UILink(), 10, typeof(CookingSkill));
            Initialize(Localizer.DoStr("Clam Chowder"), typeof(ClamChowderRecipe));
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
}