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
    public partial class ClamChowderItem :
        FoodItem
    {
        public override string FriendlyName { get { return "Clam Chowder"; } }
        public override string Description { get { return "A fish shaped cake filled with bean paste."; } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 1, Fat = 1, Protein = 1, Vitamins = 1 };
        public override float Calories { get { return 1; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(CookingSkill), 2)]
    public partial class ClamChowderRecipe : Recipe
    {
        public ClamChowderRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ClamChowderItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ClamItem>(typeof(CookingEfficiencySkill), 1, CookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<FiddleheadsItem>(typeof(CookingEfficiencySkill), 1, CookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CriminiMushroomsItem>(typeof(CookingEfficiencySkill), 1, CookingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ClamChowderRecipe), Item.Get<ClamChowderItem>().UILink(), 10, typeof(CookingSpeedSkill));
            this.Initialize("Clam Chowder", typeof(ClamChowderRecipe));
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
}