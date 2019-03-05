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
    public partial class CornSeedItem : SeedItem
    {
        static CornSeedItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Corn Seed"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow corn."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Corn"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class CornSeedPackItem : SeedPackItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Corn Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow corn."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Corn"); } }
    }

    [RequiresSkill(typeof(FarmingSkill), 4)]    
    public class CornSeedRecipe : Recipe
    {
        public CornSeedRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<CornSeedItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<CornItem>(typeof(FarmingSkill), 2, FarmingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(CornSeedRecipe), Item.Get<CornSeedItem>().UILink(), 0.5f, typeof(FarmingSkill));

            Initialize(Localizer.DoStr("Corn Seed"), typeof(CornSeedRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}