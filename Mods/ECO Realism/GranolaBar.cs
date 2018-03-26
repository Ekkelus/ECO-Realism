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
    public partial class GranolaBarItem :
        FoodItem
    {
        public override string FriendlyName { get { return "Granola Bar"; } }
        public override string Description { get { return ""; } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 12, Fat = 7, Protein = 4, Vitamins = 6 };
        public override float Calories { get { return 600; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(BasicBakingSkill), 4)]
    public partial class GranolaBarRecipe : Recipe
    {
        public GranolaBarRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<GranolaBarItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<WheatSeedItem>(typeof(BasicBakingEfficiencySkill), 20, BasicBakingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SugarItem>(typeof(BasicBakingEfficiencySkill), 10, BasicBakingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<TallowItem>(typeof(BasicBakingEfficiencySkill), 5, BasicBakingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(GranolaBarRecipe), Item.Get<GranolaBarItem>().UILink(), 10, typeof(BasicBakingSpeedSkill));
            this.Initialize("Granola Bar", typeof(GranolaBarRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}