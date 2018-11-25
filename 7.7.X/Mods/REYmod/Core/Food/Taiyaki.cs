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
	using Eco.Shared.Localization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;

    [Serialized]
    [Weight(200)]
    public partial class TaiyakiItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Taiyaki"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A fish shaped cake filled with bean paste."); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 16, Fat = 14, Protein = 4, Vitamins = 6 };
        public override float Calories { get { return 800; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(LeavenedBakingSkill), 4)]
    public partial class TaiyakiRecipe : Recipe
    {
        public TaiyakiRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<TaiyakiItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BeanPasteItem>(typeof(LeavenedBakingEfficiencySkill), 20, LeavenedBakingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<FlourItem>(typeof(LeavenedBakingEfficiencySkill), 20, LeavenedBakingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<OilItem>(typeof(LeavenedBakingEfficiencySkill), 10, LeavenedBakingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(TaiyakiRecipe), Item.Get<TaiyakiItem>().UILink(), 10, typeof(LeavenedBakingSpeedSkill));
            this.Initialize("Taiyaki", typeof(TaiyakiRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}