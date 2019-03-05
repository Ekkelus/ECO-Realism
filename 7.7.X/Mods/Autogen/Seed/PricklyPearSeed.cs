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
        public override LocString DisplayName { get { return Localizer.DoStr("Prickly Pear Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow prickly pear cacti."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("PricklyPear"); } }
    }

    [RequiresSkill(typeof(FarmingSkill), 4)]    
    public class PricklyPearSeedRecipe : Recipe
    {
        public PricklyPearSeedRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<PricklyPearSeedItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<PricklyPearFruitItem>(typeof(FarmingSkill), 2, FarmingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(PricklyPearSeedRecipe), Item.Get<PricklyPearSeedItem>().UILink(), 0.5f, typeof(FarmingSkill));

            Initialize(Localizer.DoStr("Prickly Pear Seed"), typeof(PricklyPearSeedRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}