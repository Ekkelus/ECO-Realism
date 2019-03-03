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
    public partial class PricklyPearSeedItem : SeedItem
    {
        static PricklyPearSeedItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Prickly Pear Seed"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow prickly pear cacti."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("PricklyPear"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class PricklyPearSeedPackItem : SeedPackItem
    {
        static PricklyPearSeedPackItem() { }

        public override LocString DisplayName { get { return Localizer.DoStr("Prickly Pear Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow prickly pear cacti."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("PricklyPear"); } }
    }

    [RequiresSkill(typeof(SeedProductionSkill), 4)]    
    public class PricklyPearSeedRecipe : Recipe
    {
        public PricklyPearSeedRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PricklyPearSeedItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PricklyPearFruitItem>(typeof(SeedProductionEfficiencySkill), 2, SeedProductionEfficiencySkill.MultiplicativeStrategy),   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(PricklyPearSeedRecipe), Item.Get<PricklyPearSeedItem>().UILink(), 0.5f, typeof(SeedProductionSpeedSkill));

            this.Initialize(Localizer.DoStr("Prickly Pear Seed"), typeof(PricklyPearSeedRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}