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
        public override string Description { get { return "Sea Urchins!"; } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 8, Fat = 9, Protein =12 , Vitamins = 9 };
        public override float Calories { get { return 500; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(CulinaryArtsSkill), 3)]
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
                new CraftingElement<UrchinItem>(typeof(CulinaryArtsEfficiencySkill), 10, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<AmanitaMushroomsItem>(typeof(CulinaryArtsEfficiencySkill), 20, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ExoticUrchinSurpriseRecipe), Item.Get<ExoticUrchinSurpriseItem>().UILink(), 10, typeof(CulinaryArtsSpeedSkill));
            this.Initialize("Exotic Urchin Surprise", typeof(ExoticUrchinSurpriseRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}