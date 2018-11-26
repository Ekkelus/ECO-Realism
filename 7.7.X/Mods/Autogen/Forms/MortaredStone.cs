namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Serialization;
    using Eco.World;
    using Eco.World.Blocks;



    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Floor", typeof(MortaredStoneItem))]
    public partial class MortaredStoneFloorBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredStoneItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Wall", typeof(MortaredStoneItem))]
    public partial class MortaredStoneWallBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredStoneItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Cube", typeof(MortaredStoneItem))]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 4)]
    public partial class MortaredStoneCubeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredStoneItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Roof", typeof(MortaredStoneItem))]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 2)]
    public partial class MortaredStoneRoofBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredStoneItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Column", typeof(MortaredStoneItem))]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 4)]
    public partial class MortaredStoneColumnBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredStoneItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Window", typeof(MortaredStoneItem))]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 3)]
    public partial class MortaredStoneWindowBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredStoneItem); } }
    }



    [RotatedVariants(typeof(MortaredStoneStairsBlock), typeof(MortaredStoneStairs90Block), typeof(MortaredStoneStairs180Block), typeof(MortaredStoneStairs270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Stairs", typeof(MortaredStoneItem))]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 1)]
    public partial class MortaredStoneStairsBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 1)]
    public partial class MortaredStoneStairs90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 1)]
    public partial class MortaredStoneStairs180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 1)]
    public partial class MortaredStoneStairs270Block : Block
    { }

}
