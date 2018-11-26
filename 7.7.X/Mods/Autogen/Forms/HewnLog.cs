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
    [IsForm("Floor", typeof(HewnLogItem))]
    public partial class HewnLogFloorBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Wall", typeof(HewnLogItem))]
    public partial class HewnLogWallBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Cube", typeof(HewnLogItem))]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 4)]
    public partial class HewnLogCubeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Roof", typeof(HewnLogItem))]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 2)]
    public partial class HewnLogRoofBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Column", typeof(HewnLogItem))]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 4)]
    public partial class HewnLogColumnBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Window", typeof(HewnLogItem))]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 3)]
    public partial class HewnLogWindowBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("PeatRoof", typeof(HewnLogItem))]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 1)]
    public partial class HewnLogPeatRoofBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }
    }



    [RotatedVariants(typeof(HewnLogStairsBlock), typeof(HewnLogStairs90Block), typeof(HewnLogStairs180Block), typeof(HewnLogStairs270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Stairs", typeof(HewnLogItem))]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 1)]
    public partial class HewnLogStairsBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 1)]
    public partial class HewnLogStairs90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 1)]
    public partial class HewnLogStairs180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 1)]
    public partial class HewnLogStairs270Block : Block
    { }


    [RotatedVariants(typeof(HewnLogLadderBlock), typeof(HewnLogLadder90Block), typeof(HewnLogLadder180Block), typeof(HewnLogLadder270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 1)]
    [IsForm("Ladder", typeof(HewnLogItem))]
    public partial class HewnLogLadderBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 1)]
    public partial class HewnLogLadder90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 1)]
    public partial class HewnLogLadder180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [RequiresSkill(typeof(Tier1ConstructionSkill), 1)]
    public partial class HewnLogLadder270Block : Block
    { }

}
