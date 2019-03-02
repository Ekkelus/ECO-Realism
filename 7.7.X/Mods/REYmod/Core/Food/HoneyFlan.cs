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
            this.Products = new CraftingElement[]
            {
                new CraftingElement<HoneyFlanItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HoneyItem>(typeof(AdvancedCookingSkill), 4, AdvancedCookingSkill.MultiplicativeStrategy),
                new CraftingElement<SugarItem>(typeof(AdvancedCookingSkill), 15, AdvancedCookingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(HoneyFlanRecipe), Item.Get<HoneyFlanItem>().UILink(), 15, typeof(AdvancedCookingSkill));
            this.Initialize(Localizer.DoStr("Honey Flan"), typeof(HoneyFlanRecipe));
            CraftingComponent.AddRecipe(typeof(StoveObject), this);
        }
    }
}