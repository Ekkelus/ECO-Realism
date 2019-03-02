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
    public partial class ExoticUrchinSurpriseItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Exotic Urchin Surprise"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Sea Urchins!"); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 8, Fat = 9, Protein =12 , Vitamins = 9 };
        public override float Calories { get { return 500; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 3)]
    public partial class ExoticUrchinSurpriseRecipe : Recipe
    {
        public ExoticUrchinSurpriseRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ExoticUrchinSurpriseItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<UrchinItem>(typeof(AdvancedCookingSkill), 10, AdvancedCookingSkill.MultiplicativeStrategy),
                new CraftingElement<AmanitaMushroomsItem>(typeof(AdvancedCookingSkill), 20, AdvancedCookingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ExoticUrchinSurpriseRecipe), Item.Get<ExoticUrchinSurpriseItem>().UILink(), 10, typeof(AdvancedCookingSkill));
            this.Initialize(Localizer.DoStr("Exotic Urchin Surprise"), typeof(ExoticUrchinSurpriseRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}