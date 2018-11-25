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
    [Yield(typeof(AcornItem), typeof(GrasslandGathererSkill), new float[] { 1f, 1.4f, 1.8f, 2.2f, 2.6f, 3f  })]  
    [Weight(10)]  
    public partial class AcornItem : SeedItem
    {
        static AcornItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Acorn"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow an oak tree."); } }
        public override string SpeciesName  { get { return "Oak"; } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class AcornPackItem : SeedPackItem
    {
        static AcornPackItem() { }

        public override LocString DisplayName { get { return Localizer.DoStr("Acorn Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow an oak tree."); } }
        public override string SpeciesName  { get { return "Oak"; } }
    }

}