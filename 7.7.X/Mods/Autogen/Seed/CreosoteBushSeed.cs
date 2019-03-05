namespace Eco.Mods.TechTree
{
    using Gameplay.Items;
    using Shared.Localization;
    using Shared.Serialization;
    using Gameplay.Players;
    using System.ComponentModel;

    [Serialized]
    [Yield(typeof(CreosoteBushSeedItem), typeof(GatheringSkill), new[] { 1f, 1.2f, 1.4f, 1.6f, 1.8f, 2f  })]  
    [Weight(10)]  
    public partial class CreosoteBushSeedItem : SeedItem
    {
        static CreosoteBushSeedItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Creosote Bush Seed"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow creosote bushes."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("CreosoteBush"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class CreosoteBushSeedPackItem : SeedPackItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Creosote Bush Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow creosote bushes."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("CreosoteBush"); } }
    }

}