namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Shared.Localization;
    using Shared.Serialization;
    using World;
    using World.Blocks;


    [Serialized]
    [Minable(1), Solid,Wall]
    public partial class GoldOreBlock :
        Block           
    { }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(15000)]      
    [ResourcePile]                                          
    [Currency]              
    public partial class GoldOreItem :
    BlockItem<GoldOreBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Gold Ore"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Gold Ore"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Unrefined ore with traces of gold."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new[] {
            typeof(GoldOreStacked1Block),
            typeof(GoldOreStacked2Block),
            typeof(GoldOreStacked3Block),
            typeof(GoldOreStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class GoldOreStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class GoldOreStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class GoldOreStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class GoldOreStacked4Block : PickupableBlock { } //Only a wall if it's all 4 GoldOre
}