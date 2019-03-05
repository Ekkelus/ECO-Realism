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
    public partial class CopperOreBlock :
        Block           
    { }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(15000)]      
    [ResourcePile]                                          
    [Currency]              
    public partial class CopperOreItem :
    BlockItem<CopperOreBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Copper Ore"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Copper Ore"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Unrefined ore with traces of copper."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new[] {
            typeof(CopperOreStacked1Block),
            typeof(CopperOreStacked2Block),
            typeof(CopperOreStacked3Block),
            typeof(CopperOreStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class CopperOreStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class CopperOreStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class CopperOreStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class CopperOreStacked4Block : PickupableBlock { } //Only a wall if it's all 4 CopperOre
}