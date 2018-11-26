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
    [Tier(2)]
    [IsForm("Floor", typeof(LumberItem))]
    public partial class LumberFloorBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(LumberItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Wall", typeof(LumberItem))]
    public partial class LumberWallBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(LumberItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Cube", typeof(LumberItem))]
    [RequiresSkill(typeof(Tier2ConstructionSkill), 4)]
    public partial class LumberCubeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(LumberItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Roof", typeof(LumberItem))]
    [RequiresSkill(typeof(Tier2ConstructionSkill), 2)]
    public partial class LumberRoofBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(LumberItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Column", typeof(LumberItem))]
    [RequiresSkill(typeof(Tier2ConstructionSkill), 4)]
    public partial class LumberColumnBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(LumberItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Window", typeof(LumberItem))]
    [RequiresSkill(typeof(Tier2ConstructionSkill), 3)]
    public partial class LumberWindowBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(LumberItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Fence", typeof(LumberItem))]
    [RequiresSkill(typeof(Tier2ConstructionSkill), 2)]
    public partial class LumberFenceBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(LumberItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("WindowT2", typeof(LumberItem))]
    [RequiresSkill(typeof(Tier2ConstructionSkill), 4)]
    public partial class LumberWindowT2Block :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(LumberItem); } }
    }



    [RotatedVariants(typeof(LumberStairsBlock), typeof(LumberStairs90Block), typeof(LumberStairs180Block), typeof(LumberStairs270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Stairs", typeof(LumberItem))]
    [RequiresSkill(typeof(Tier2ConstructionSkill), 1)]
    public partial class LumberStairsBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [RequiresSkill(typeof(Tier2ConstructionSkill), 1)]
    public partial class LumberStairs90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [RequiresSkill(typeof(Tier2ConstructionSkill), 1)]
    public partial class LumberStairs180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [RequiresSkill(typeof(Tier2ConstructionSkill), 1)]
    public partial class LumberStairs270Block : Block
    { }

}
