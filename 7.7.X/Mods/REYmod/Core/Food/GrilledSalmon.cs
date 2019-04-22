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
    public partial class GrilledSalmonItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Grilled Salmon"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Salmon fillet grilled on a campfire"); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 5, Fat = 7, Protein = 7, Vitamins = 6 };
        public override float Calories { get { return 800; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCampfireCookingSkill), 3)]
    public partial class GrilledSalmonRecipe : Recipe
    {
        public GrilledSalmonRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<GrilledSalmonItem>(),

            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SalmonFilletItem>(typeof(AdvancedCampfireCookingSkill), 10, AdvancedCampfireCookingSkill.MultiplicativeStrategy),
				new CraftingElement<TallowItem>(typeof(AdvancedCampfireCookingSkill), 5, AdvancedCampfireCookingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(GrilledSalmonRecipe), Item.Get<GrilledSalmonItem>().UILink(), 10, typeof(AdvancedCampfireCookingSkill), typeof(AdvancedCampfireCookingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Grilled Salmon"), typeof(GrilledSalmonRecipe));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}