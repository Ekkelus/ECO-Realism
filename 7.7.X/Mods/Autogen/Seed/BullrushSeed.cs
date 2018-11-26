namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
	using Eco.Shared.Localization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Gameplay.Players;
    using System.ComponentModel;

    [Serialized]
    [Yield(typeof(BullrushSeedItem), typeof(GrasslandGathererSkill), new float[] { 1f, 1.2f, 1.4f, 1.6f, 1.8f, 2f  })]  
    [Weight(10)]  
    public partial class BullrushSeedItem : SeedItem
    {
        static BullrushSeedItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Bullrush Seed"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow bullrush."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Bullrush"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class BullrushSeedPackItem : SeedPackItem
    {
        static BullrushSeedPackItem() { }

        public override LocString DisplayName { get { return Localizer.DoStr("Bullrush Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow bullrush."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Bullrush"); } }
    }

}