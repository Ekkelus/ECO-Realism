namespace Eco.Mods.TechTree
{
    using Gameplay.Items;
    using Shared.Localization;
    using Shared.Serialization;
    using Gameplay.Players;
    using System.ComponentModel;

    [Serialized]
    [Yield(typeof(AcornItem), typeof(GatheringSkill), new[] { 1f, 1.4f, 1.8f, 2.2f, 2.6f, 3f  })]  
    [Weight(10)]  
    public partial class AcornItem : SeedItem
    {
        static AcornItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Acorn"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow an oak tree."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Oak"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class AcornPackItem : SeedPackItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Acorn Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow an oak tree."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Oak"); } }
    }

}