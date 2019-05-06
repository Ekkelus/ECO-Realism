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
    public partial class BeetSeedItem : SeedItem
    {
        static BeetSeedItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Beet Seed"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow beets."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Beets"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class BeetSeedPackItem : SeedPackItem
    {
        static BeetSeedPackItem() { }

        public override LocString DisplayName { get { return Localizer.DoStr("Beet Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow beets."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Beets"); } }
    }

    [RequiresSkill(typeof(FarmingSkill), 2)]    
    public class BeetSeedRecipe : Recipe
    {
        public BeetSeedRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BeetSeedItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BeetItem>(typeof(FarmingSkill), 2, FarmingSkill.MultiplicativeStrategy),   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BeetSeedRecipe), Item.Get<BeetSeedItem>().UILink(), 0.5f, typeof(FarmingSkill));

            this.Initialize(Localizer.DoStr("Beet Seed"), typeof(BeetSeedRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}