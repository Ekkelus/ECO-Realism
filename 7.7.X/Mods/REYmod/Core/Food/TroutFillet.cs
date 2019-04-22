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
    public partial class TroutFilletItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Trout Fillet"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Some fine trout Fillet."); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 3, Protein = 10, Vitamins = 2 };
        public override float Calories { get { return 400; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(HuntingSkill), 1)]
    public partial class TroutFilletRecipe : Recipe
    {
        public TroutFilletRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<TroutFilletItem>(),

            };
            Ingredients = new CraftingElement[]
            {
				new CraftingElement<TroutItem>(typeof(HuntingSkill), 1, HuntingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(TroutFilletRecipe), Item.Get<TroutFilletItem>().UILink(), 3, typeof(HuntingSkill), typeof(HuntingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Trout Fillet"), typeof(TroutFilletRecipe));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
}