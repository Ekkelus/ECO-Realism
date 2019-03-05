namespace Eco.Mods.TechTree
{
    using Gameplay.Items;
    using Shared.Localization;
    using Shared.Serialization;
    using Gameplay.Players;
    using System.ComponentModel;

    [Serialized]
    [Yield(typeof(SagebrushSeedItem), typeof(GatheringSkill), new[] { 1f, 1.2f, 1.4f, 1.6f, 1.8f, 2f  })]  
    [Weight(10)]  
    public partial class SagebrushSeedItem : SeedItem
    {
        static SagebrushSeedItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Sagebrush Seed"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow sagebrush."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Sagebrush"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class SagebrushSeedPackItem : SeedPackItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Sagebrush Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow sagebrush."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Sagebrush"); } }
    }

}