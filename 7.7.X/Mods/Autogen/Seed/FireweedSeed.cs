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
    public partial class FireweedSeedItem : SeedItem
    {
        static FireweedSeedItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Fireweed Seed"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow fireweed."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Fireweed"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class FireweedSeedPackItem : SeedPackItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Fireweed Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow fireweed."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Fireweed"); } }
    }

    [RequiresSkill(typeof(FarmingSkill), 3)]    
    public class FireweedSeedRecipe : Recipe
    {
        public FireweedSeedRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<FireweedSeedItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<FireweedShootsItem>(typeof(FarmingSkill), 2, FarmingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(FireweedSeedRecipe), Item.Get<FireweedSeedItem>().UILink(), 0.5f, typeof(FarmingSkill));

            Initialize(Localizer.DoStr("Fireweed Seed"), typeof(FireweedSeedRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}