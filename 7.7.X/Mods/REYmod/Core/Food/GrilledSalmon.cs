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
    public partial class GrilledSalmonItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Grilled Salmon"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Salmon fillet grilled on a campfire"); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 5, Fat = 7, Protein = 7, Vitamins = 6 };
        public override float Calories { get { return 800; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(CampfireCreationsSkill), 3)]
    public partial class GrilledSalmonRecipe : Recipe
    {
        public GrilledSalmonRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<GrilledSalmonItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SalmonFilletItem>(typeof(CampfireCreationsEfficiencySkill), 10, CampfireCreationsEfficiencySkill.MultiplicativeStrategy),
				new CraftingElement<TallowItem>(typeof(CampfireCreationsEfficiencySkill), 5, CampfireCreationsEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(GrilledSalmonRecipe), Item.Get<GrilledSalmonItem>().UILink(), 10, typeof(CampfireCreationsSpeedSkill));
            this.Initialize(Localizer.Do("Grilled Salmon"), typeof(GrilledSalmonRecipe));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}