namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Gameplay.Players;
    using System.ComponentModel;

    [Serialized]
    [Yield(typeof(BunchgrassSeedItem), typeof(GrasslandGathererSkill), new float[] { 1f, 1.2f, 1.4f, 1.6f, 1.8f, 2f  })]  
    [Weight(10)]  
    public partial class BunchgrassSeedItem : SeedItem
    {
        static BunchgrassSeedItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Bunchgrass Seed"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow bunchgrass."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Bunchgrass"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class BunchgrassSeedPackItem : SeedPackItem
    {
        static BunchgrassSeedPackItem() { }

        public override LocString DisplayName { get { return Localizer.DoStr("Bunchgrass Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow bunchgrass."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Bunchgrass"); } }
    }

}