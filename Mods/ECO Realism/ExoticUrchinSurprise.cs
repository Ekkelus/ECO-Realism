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
    public partial class ExoticUrchinSurpriseItem :
        FoodItem
    {
        public override string FriendlyName { get { return "Exotic Urchin Surprise"; } }
        public override string Description { get { return "A fish shaped cake filled with bean paste."; } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 16, Fat = 14, Protein = 4, Vitamins = 6 };
        public override float Calories { get { return 800; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(LeavenedBakingSkill), 4)]
    public partial class ExoticUrchinSurpriseRecipe : Recipe
    {
        public ExoticUrchinSurpriseRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ExoticUrchinSurpriseItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<UrchinItem>(typeof(LeavenedBakingEfficiencySkill), 20, LeavenedBakingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<AmanitaMushroomsItem>(typeof(LeavenedBakingEfficiencySkill), 20, LeavenedBakingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ExoticUrchinSurpriseRecipe), Item.Get<ExoticUrchinSurpriseItem>().UILink(), 10, typeof(LeavenedBakingSpeedSkill));
            this.Initialize("Exotic Urchin Surprise", typeof(ExoticUrchinSurpriseRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}