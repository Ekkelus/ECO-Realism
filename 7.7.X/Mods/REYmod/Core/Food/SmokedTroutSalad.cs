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
    public partial class SmokedTroutSaladItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Smoked Trout Salad"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Yummy Salad"); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 7, Fat = 7, Protein = 13, Vitamins = 14 };
        public override float Calories { get { return 800; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(CookingSkill), 4)]
    public partial class SmokedTroutSaladRecipe : Recipe
    {
        public SmokedTroutSaladRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SmokedTroutSaladItem>(),

            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<TroutFilletItem>(typeof(CookingSkill), 20, CookingSkill.MultiplicativeStrategy),
                new CraftingElement<CriminiMushroomsItem>(typeof(CookingSkill), 20, CookingSkill.MultiplicativeStrategy),
                new CraftingElement<FiddleheadsItem>(typeof(CookingSkill), 20, CookingSkill.MultiplicativeStrategy),
                new CraftingElement<PricklyPearFruitItem>(typeof(CookingSkill), 20, CookingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(SmokedTroutSaladRecipe), Item.Get<SmokedTroutSaladItem>().UILink(), 10, typeof(CookingSkill), typeof(CookingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Smoked Trout Salad"), typeof(SmokedTroutSaladRecipe));
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
}