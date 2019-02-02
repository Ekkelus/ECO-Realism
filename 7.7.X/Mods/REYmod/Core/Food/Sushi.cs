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
    public partial class SushiItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Sushi"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Raw fish with sticky rice, rolled in kelp."); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 15, Fat = 12, Protein = 15, Vitamins = 10 };
        public override float Calories { get { return 950; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(CulinaryArtsSkill), 3)]
    public partial class SushiRecipe : Recipe
    {
        public SushiRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SushiItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SalmonFilletItem>(typeof(CulinaryArtsEfficiencySkill), 10, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<TunaFilletItem>(typeof(CulinaryArtsEfficiencySkill), 10, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<TroutFilletItem>(typeof(CulinaryArtsEfficiencySkill), 10, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<ClamItem>(typeof(CulinaryArtsEfficiencySkill), 4, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<UrchinItem>(typeof(CulinaryArtsEfficiencySkill), 4, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<KelpItem>(typeof(CulinaryArtsEfficiencySkill), 4, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<RiceItem>(typeof(CulinaryArtsEfficiencySkill), 20, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(SushiRecipe), Item.Get<SushiItem>().UILink(), 15, typeof(CulinaryArtsSpeedSkill));
            this.Initialize(Localizer.DoStr("Sushi"), typeof(SushiRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}