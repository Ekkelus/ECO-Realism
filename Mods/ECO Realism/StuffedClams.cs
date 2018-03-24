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
    public partial class StuffedClamsItem :
        FoodItem
    {
        public override string FriendlyName { get { return "Stuffed Clams"; } }
        public override string Description { get { return "A fish shaped cake filled with bean paste."; } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 1, Fat = 1, Protein = 1, Vitamins = 1 };
        public override float Calories { get { return 1; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(HomeCookingSkill), 4)]
    public partial class StuffedClamsRecipe : Recipe
    {
        public StuffedClamsRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<StuffedClamsItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ClamItem>(typeof(HomeCookingEfficiencySkill), 1, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<WheathPorridgeItem>(typeof(HomeCookingEfficiencySkill), 1, HomeCookingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(StuffedClamsRecipe), Item.Get<StuffedClamsItem>().UILink(), 15, typeof(HomeCookingSpeedSkill));
            this.Initialize("Stuffed Clams", typeof(StuffedClamsRecipe));
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
}