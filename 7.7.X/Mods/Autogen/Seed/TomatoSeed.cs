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
    public partial class TomatoSeedItem : SeedItem
    {
        static TomatoSeedItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Tomato Seed"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow tomato plants."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Tomatoes"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class TomatoSeedPackItem : SeedPackItem
    {
        static TomatoSeedPackItem() { }

        public override LocString DisplayName { get { return Localizer.DoStr("Tomato Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow tomato plants."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Tomatoes"); } }
    }

    [RequiresSkill(typeof(FarmingSkill), 3)]    
    public class TomatoSeedRecipe : Recipe
    {
        public TomatoSeedRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<TomatoSeedItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TomatoItem>(typeof(FarmingSkill), 2, FarmingSkill.MultiplicativeStrategy),   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(TomatoSeedRecipe), Item.Get<TomatoSeedItem>().UILink(), 0.5f, typeof(FarmingSkill));

            this.Initialize(Localizer.DoStr("Tomato Seed"), typeof(TomatoSeedRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}