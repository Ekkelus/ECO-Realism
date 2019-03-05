namespace Eco.Mods.TechTree
{
    using Gameplay.Items;
    using Shared.Localization;
    using Shared.Serialization;
    using Gameplay.Players;
    using System.ComponentModel;

    [Serialized]
    [Yield(typeof(RiceItem), typeof(GatheringSkill), new[] { 1f, 1.4f, 1.8f, 2.2f, 2.6f, 3f  })]  
    [Weight(10)]  
    public partial class RiceItem : SeedItem
    {
        static RiceItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 7, Fat = 0, Protein = 1, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Rice"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow rice."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Rice"); } }

        public override float Calories { get { return 90; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class RicePackItem : SeedPackItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Rice Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow rice."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Rice"); } }
    }

}