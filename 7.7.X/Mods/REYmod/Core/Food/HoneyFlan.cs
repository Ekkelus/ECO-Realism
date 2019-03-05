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
    public partial class HoneyFlanItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Honey Flan"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Classic tasty dessert."); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 15, Fat = 5, Protein = 4, Vitamins = 8 };
        public override float Calories { get { return 550; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 2)]
    public partial class HoneyFlanRecipe : Recipe
    {
        public HoneyFlanRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<HoneyFlanItem>(),

            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<HoneyItem>(typeof(AdvancedCookingSkill), 4, AdvancedCookingSkill.MultiplicativeStrategy),
                new CraftingElement<SugarItem>(typeof(AdvancedCookingSkill), 15, AdvancedCookingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(HoneyFlanRecipe), Item.Get<HoneyFlanItem>().UILink(), 15, typeof(AdvancedCookingSkill));
            Initialize(Localizer.DoStr("Honey Flan"), typeof(HoneyFlanRecipe));
            CraftingComponent.AddRecipe(typeof(StoveObject), this);
        }
    }
}