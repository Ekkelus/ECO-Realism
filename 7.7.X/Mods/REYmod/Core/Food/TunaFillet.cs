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
    public partial class TunaFilletItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tuna Fillet"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Some fine tuna Fillet​."); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 0 , Fat = 5, Protein = 12, Vitamins = 4 };
        public override float Calories { get { return 500; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(HuntingSkill), 3)]
    public partial class TunaFilletRecipe : Recipe
    {
        public TunaFilletRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<TunaFilletItem>(),

            };
            Ingredients = new CraftingElement[]
            {
				new CraftingElement<TunaItem>(typeof(HuntingSkill), 1, HuntingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(TunaFilletRecipe), Item.Get<TunaFilletItem>().UILink(), 3, typeof(HuntingSkill), typeof(HuntingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Tuna Fillet"), typeof(TunaFilletRecipe));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
}