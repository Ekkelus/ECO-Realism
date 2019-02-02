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
    [Minable, Solid,Wall]
    public partial class CoalBlock :
        Block           
    { }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(15000)]      
    [Fuel(20000)]          
    [ResourcePile]                                          
    [Currency]              
    public partial class CoalItem :
    BlockItem<CoalBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Coal"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Coal"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A combustible mineral which when used as a fuel provides lots of energy but generates lots of pollution."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new Type[] {
            typeof(CoalStacked1Block),
            typeof(CoalStacked2Block),
            typeof(CoalStacked3Block),
            typeof(CoalStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class CoalStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class CoalStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class CoalStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class CoalStacked4Block : PickupableBlock { } //Only a wall if it's all 4 Coal
}