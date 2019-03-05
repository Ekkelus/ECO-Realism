namespace Eco.Mods.TechTree
{
    using Gameplay.Items;
    using Shared.Localization;
    using Shared.Serialization;
    using Gameplay.Players;
    using System.ComponentModel;

    [Serialized]
    [Yield(typeof(BirchSeedItem), typeof(GatheringSkill), new[] { 1f, 1.4f, 1.8f, 2.2f, 2.6f, 3f  })]  
    [Weight(10)]  
    public partial class BirchSeedItem : SeedItem
    {
        static BirchSeedItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Birch Seed"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow into a birch sapling."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Birch"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class BirchSeedPackItem : SeedPackItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Birch Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow into a birch sapling."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Birch"); } }
    }

}