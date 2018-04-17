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
    [Weight(200)]
    public partial class HoneyGlazedHamItem :
        FoodItem
    {
        public override string FriendlyName { get { return "Honey Glazed Ham"; } }
        public override string Description { get { return "Raw fish with sticky rice, rolled in kelp."; } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 14, Fat = 15, Protein = 11, Vitamins = 4 };
        public override float Calories { get { return 950; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(CulinaryArtsSkill), 3)]
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
                new CraftingElement<HoneyItem>(typeof(CulinaryArtsEfficiencySkill), 2, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(HoneyGlazedHamRecipe), Item.Get<HoneyGlazedHamItem>().UILink(), 15, typeof(CulinaryArtsSpeedSkill));
            this.Initialize("HoneyGlazedHam", typeof(HoneyGlazedHamRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}