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
    [Minable(0), Solid,Wall]
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

        private static Type[] blockTypes = new[] {
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