namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Gameplay.Players;
    using System.ComponentModel;

    [Serialized]
    [Weight(10)]  
    public partial class HuckleberrySeedItem : SeedItem
    {
        static HuckleberrySeedItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Huckleberry Seed"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow a huckleberry bush."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Huckleberry"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class HuckleberrySeedPackItem : SeedPackItem
    {
        static HuckleberrySeedPackItem() { }

        public override LocString DisplayName { get { return Localizer.DoStr("Huckleberry Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow a huckleberry bush."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Huckleberry"); } }
    }

    [RequiresSkill(typeof(SeedProductionSkill), 2)]    
    public class HuckleberrySeedRecipe : Recipe
    {
        public HuckleberrySeedRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<HuckleberrySeedItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HuckleberriesItem>(typeof(SeedProductionEfficiencySkill), 4, SeedProductionEfficiencySkill.MultiplicativeStrategy),   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(HuckleberrySeedRecipe), Item.Get<HuckleberrySeedItem>().UILink(), 0.5f, typeof(SeedProductionSpeedSkill));

            this.Initialize(Localizer.DoStr("Huckleberry Seed"), typeof(HuckleberrySeedRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}