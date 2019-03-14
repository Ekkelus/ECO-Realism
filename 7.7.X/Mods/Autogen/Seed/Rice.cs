namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Items.SearchAndSelect;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Gameplay.Players;
    using System.ComponentModel;

    [Serialized]
    [Yield(typeof(RiceItem), typeof(GatheringSkill), new float[] {1f, 1.8f, 2.0f, 2.2f, 2.4f, 2.6f, 2.8f, 3.0f})]  
    [Crop]  
    [Weight(50)]  
    [StartsDiscovered]
    public partial class RiceItem : SeedItem
    {
        static RiceItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 7, Fat = 0, Protein = 1, Vitamins = 0 };

        public override LocString DisplayName        { get { return Localizer.DoStr("Rice"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow rice."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("Rice"); } }

        public override float Calories { get { return 90; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(50)]  
    public partial class RicePackItem : SeedPackItem
    {
        static RicePackItem() { }

        public override LocString DisplayName        { get { return Localizer.DoStr("Rice Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow rice."); } }
        public override LocString SpeciesName        { get { return Localizer.DoStr("Rice"); } }
    }

}