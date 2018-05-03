namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;

    [Serialized]
    [Weight(500)]
    public partial class HoneyGlazedHamItem :
        FoodItem
    {
        public override string FriendlyName { get { return "Honey Glazed Ham"; } }
        public override string Description { get { return "Delicious, mouth-watering dish."; } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 10, Fat = 17, Protein = 14, Vitamins = 5 };
        public override float Calories { get { return 1050; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(CulinaryArtsSkill), 4)]
    public partial class HoneyGlazedHamRecipe : Recipe
    {
        public HoneyGlazedHamRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<HoneyGlazedHamItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HoneyItem>(typeof(CulinaryArtsEfficiencySkill), 6, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<VegetableMedleyItem>(typeof(CulinaryArtsEfficiencySkill), 2, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PrimeCutItem>(typeof(CulinaryArtsEfficiencySkill), 12, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<MeatStockItem>(typeof(CulinaryArtsEfficiencySkill), 8, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(HoneyGlazedHamRecipe), Item.Get<HoneyGlazedHamItem>().UILink(), 20, typeof(CulinaryArtsSpeedSkill));
            this.Initialize("Honey Glazed Ham", typeof(HoneyGlazedHamRecipe));
            CraftingComponent.AddRecipe(typeof(StoveObject), this);
        }
    }
}