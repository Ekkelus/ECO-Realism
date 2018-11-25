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
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using REYmod.Utils;

    [Serialized]
    [Weight(150)]
    public partial class HoneyItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Honey"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A spoonful a day keeps the doctor away"); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 14, Fat = 0, Protein = 1, Vitamins = 0 };
        public override float Calories { get { return 45; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(BeekeeperSkill), 1)]
    public partial class HoneyRecipe : Recipe
    {
        public HoneyRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<HoneyItem>(),
                new CraftingElement<BeeswaxItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GlassJarItem>(typeof(BeekeeperEfficiencySkill), 2, BeekeeperEfficiencySkill.MultiplicativeStrategy),
            };
            //            this.CraftMinutes = new MultiDynamicValue(MultiDynamicOps.Multiply
            //, CreateCraftTimeValue(typeof(HoneyRecipe), Item.Get<HoneyItem>().UILink(), 0.1f, typeof(BeekeeperSpeedSkill))
            //, MiscUtils.InvertDynamicvalue(new MultiDynamicValue(MultiDynamicOps.Average
            //    , new LayerModifiedValue("Fireweed", 5)
            //    , new LayerModifiedValue("Camas", 5))
            //));
            this.CraftMinutes = CreateCraftTimeValue(typeof(HoneyRecipe), Item.Get<HoneyItem>().UILink(), 10, typeof(BeekeeperSpeedSkill));
            this.Initialize("Honey", typeof(HoneyRecipe));
            CraftingComponent.AddRecipe(typeof(BeehiveObject), this);
        }
    }
}