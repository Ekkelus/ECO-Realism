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
    public partial class SmokedTroutSaladItem :
        FoodItem
    {
        public override string FriendlyName { get { return "Smoked Trout Salad"; } }
        public override string Description { get { return "Yummy Salad"; } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 7, Fat = 6, Protein = 12, Vitamins = 14 };
        public override float Calories { get { return 800; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(HomeCookingSkill), 4)]
    public partial class SmokedTroutSaladRecipe : Recipe
    {
        public SmokedTroutSaladRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SmokedTroutSaladItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TroutFilletItem>(typeof(HomeCookingEfficiencySkill), 20, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CriminiMushroomsItem>(typeof(HomeCookingEfficiencySkill), 20, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<WiltedFiddleheadsItem>(typeof(HomeCookingEfficiencySkill), 20, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PricklyPearFruitItem>(typeof(HomeCookingEfficiencySkill), 20, HomeCookingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(SmokedTroutSaladRecipe), Item.Get<SmokedTroutSaladItem>().UILink(), 10, typeof(HomeCookingSpeedSkill));
            this.Initialize("Smoked Trout Salad", typeof(SmokedTroutSaladRecipe));
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
}