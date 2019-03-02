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
    public partial class PizzaItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Pizza"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A flatbread topped with tomato sauce, vegetables, and some meat. Clearly better with pineapple."); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 15, Fat = 15, Protein = 8, Vitamins = 8 };
        public override float Calories { get { return 1100; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedBakingSkill), 3)]
    public partial class PizzaRecipe : Recipe
    {
        public PizzaRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PizzaItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TomatoItem>(typeof(AdvancedBakingSkill), 30, AdvancedBakingSkill.MultiplicativeStrategy),
                new CraftingElement<FlatbreadItem>(typeof(AdvancedBakingSkill), 5, AdvancedBakingSkill.MultiplicativeStrategy),
                new CraftingElement<FiddleheadsItem>(typeof(AdvancedBakingSkill), 30, AdvancedBakingSkill.MultiplicativeStrategy),
                new CraftingElement<ScrapMeatItem>(typeof(AdvancedBakingSkill), 10, AdvancedBakingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(PizzaRecipe), Item.Get<PizzaItem>().UILink(), 15, typeof(AdvancedBakingSkill));
            this.Initialize(Localizer.DoStr("Pizza"), typeof(PizzaRecipe));
            CraftingComponent.AddRecipe(typeof(BakeryOvenObject), this);
        }
    }
}