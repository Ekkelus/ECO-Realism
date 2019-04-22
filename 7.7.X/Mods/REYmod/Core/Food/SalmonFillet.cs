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
    public partial class SalmonFilletItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Salmon Fillet"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Some fine Salmon Fillet."); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 4, Protein = 11, Vitamins = 3 };
        public override float Calories { get { return 450; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(HuntingSkill), 2)]
    public partial class SalmonFilletRecipe : Recipe
    {
        public SalmonFilletRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SalmonFilletItem>(),

            };
            Ingredients = new CraftingElement[]
            {
				new CraftingElement<SalmonItem>(typeof(HuntingSkill), 1, HuntingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(SalmonFilletRecipe), Item.Get<SalmonFilletItem>().UILink(), 3, typeof(HuntingSkill), typeof(HuntingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Salmon Fillet"), typeof(SalmonFilletRecipe));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
}