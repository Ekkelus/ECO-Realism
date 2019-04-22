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
            Products = new CraftingElement[]
            {
                new CraftingElement<ExoticUrchinSurpriseItem>(),

            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<UrchinItem>(typeof(AdvancedCookingSkill), 10, AdvancedCookingSkill.MultiplicativeStrategy),
                new CraftingElement<AmanitaMushroomsItem>(typeof(AdvancedCookingSkill), 20, AdvancedCookingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(ExoticUrchinSurpriseRecipe), Item.Get<ExoticUrchinSurpriseItem>().UILink(), 10, typeof(AdvancedCookingSkill), typeof(AdvancedCookingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Exotic Urchin Surprise"), typeof(ExoticUrchinSurpriseRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}