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
    public partial class ClamChowderItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Clam Chowder"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Some soup with clams"); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 4, Fat = 4, Protein = 10, Vitamins = 12 };
        public override float Calories { get { return 1000; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(HomeCookingSkill), 2)]
    public partial class ClamChowderRecipe : Recipe
    {
        public ClamChowderRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ClamChowderItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ClamItem>(typeof(HomeCookingEfficiencySkill), 10, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<FiddleheadsItem>(typeof(HomeCookingEfficiencySkill), 20, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CriminiMushroomsItem>(typeof(HomeCookingEfficiencySkill), 20, HomeCookingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ClamChowderRecipe), Item.Get<ClamChowderItem>().UILink(), 10, typeof(HomeCookingSpeedSkill));
            this.Initialize(Localizer.DoStr("Clam Chowder"), typeof(ClamChowderRecipe));
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
}