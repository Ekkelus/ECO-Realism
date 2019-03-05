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
    public partial class TaiyakiItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Taiyaki"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A fish shaped cake filled with bean paste."); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 16, Fat = 14, Protein = 4, Vitamins = 6 };
        public override float Calories { get { return 800; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedBakingSkill), 4)]
    public partial class TaiyakiRecipe : Recipe
    {
        public TaiyakiRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<TaiyakiItem>(),

            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BeanPasteItem>(typeof(AdvancedBakingSkill), 20, AdvancedBakingSkill.MultiplicativeStrategy),
                new CraftingElement<FlourItem>(typeof(AdvancedBakingSkill), 20, AdvancedBakingSkill.MultiplicativeStrategy),
                new CraftingElement<OilItem>(typeof(AdvancedBakingSkill), 10, AdvancedBakingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(TaiyakiRecipe), Item.Get<TaiyakiItem>().UILink(), 10, typeof(AdvancedBakingSkill));
            Initialize(Localizer.DoStr("Taiyaki"), typeof(TaiyakiRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}