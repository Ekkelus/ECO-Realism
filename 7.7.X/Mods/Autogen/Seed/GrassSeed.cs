namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Gameplay.Players;
    using System.ComponentModel;

    [Serialized]
    [Yield(typeof(GrassSeedItem), typeof(GatheringSkill), new float[] { 1f, 1.2f, 1.4f, 1.6f, 1.8f, 2f  })]  
    [Weight(10)]  
    public partial class GrassSeedItem : SeedItem
    {
        static GrassSeedItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Grass Seed"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow grass."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("CommonGrass"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class GrassSeedPackItem : SeedPackItem
    {
        static GrassSeedPackItem() { }

        public override LocString DisplayName { get { return Localizer.DoStr("Grass Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow grass."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("CommonGrass"); } }
    }

}