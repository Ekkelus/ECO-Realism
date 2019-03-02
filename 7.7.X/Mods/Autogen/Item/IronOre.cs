namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.World;
    using Eco.World.Blocks;


    [Serialized]
    [Minable(0), Solid,Wall]
    public partial class IronOreBlock :
        Block           
    { }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(15000)]      
    [ResourcePile]                                          
    [Currency]              
    public partial class IronOreItem :
    BlockItem<IronOreBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Iron Ore"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Iron Ore"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Unrefined ore with traces of iron."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new Type[] {
            typeof(IronOreStacked1Block),
            typeof(IronOreStacked2Block),
            typeof(IronOreStacked3Block),
            typeof(IronOreStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class IronOreStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class IronOreStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class IronOreStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class IronOreStacked4Block : PickupableBlock { } //Only a wall if it's all 4 IronOre
}