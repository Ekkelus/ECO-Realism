namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;
    using Shared.Serialization;
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
        public override LocString DisplayName { get { return Localizer.DoStr("Tomato Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow tomato plants."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Tomatoes"); } }
    }

    [RequiresSkill(typeof(FarmingSkill), 3)]    
    public class TomatoSeedRecipe : Recipe
    {
        public TomatoSeedRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<TomatoSeedItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<TomatoItem>(typeof(FarmingSkill), 2, FarmingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(TomatoSeedRecipe), Item.Get<TomatoSeedItem>().UILink(), 0.5f, typeof(FarmingSkill));

            Initialize(Localizer.DoStr("Tomato Seed"), typeof(TomatoSeedRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}