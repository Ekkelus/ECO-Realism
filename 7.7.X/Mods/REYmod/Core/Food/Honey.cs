namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Players;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;
    using Shared.Serialization;

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

    [RequiresSkill(typeof(FarmingSkill), 1)]
    public partial class HoneyRecipe : Recipe
    {
        public HoneyRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<HoneyItem>(),
                new CraftingElement<BeeswaxItem>(),

            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<GlassJarItem>(typeof(FarmingSkill), 2, FarmingSkill.MultiplicativeStrategy),
            };
            //            this.CraftMinutes = new MultiDynamicValue(MultiDynamicOps.Multiply
            //, CreateCraftTimeValue(typeof(HoneyRecipe), Item.Get<HoneyItem>().UILink(), 0.1f, typeof(FarmingSkill))
            //, MiscUtils.InvertDynamicvalue(new MultiDynamicValue(MultiDynamicOps.Average
            //    , new LayerModifiedValue("Fireweed", 5)
            //    , new LayerModifiedValue("Camas", 5))
            //));
            CraftMinutes = CreateCraftTimeValue(typeof(HoneyRecipe), Item.Get<HoneyItem>().UILink(), 10, typeof(FarmingSkill));
            Initialize(Localizer.DoStr("Honey"), typeof(HoneyRecipe));
            CraftingComponent.AddRecipe(typeof(BeehiveObject), this);
        }
    }
}