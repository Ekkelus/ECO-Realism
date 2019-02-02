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
    [IsForm("Floor", typeof(BrickItem))]
    public partial class BrickFloorBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(BrickItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Wall", typeof(BrickItem))]
    public partial class BrickWallBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(BrickItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Cube", typeof(BrickItem))]
    [RequiresSkill(typeof(Tier2ConstructionSkill), 4)]
    public partial class BrickCubeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(BrickItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Roof", typeof(BrickItem))]
    [RequiresSkill(typeof(Tier2ConstructionSkill), 2)]
    public partial class BrickRoofBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(BrickItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Column", typeof(BrickItem))]
    [RequiresSkill(typeof(Tier2ConstructionSkill), 4)]
    public partial class BrickColumnBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(BrickItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Window", typeof(BrickItem))]
    [RequiresSkill(typeof(Tier2ConstructionSkill), 3)]
    public partial class BrickWindowBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(BrickItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Aqueduct", typeof(BrickItem))]
    [RequiresSkill(typeof(Tier2ConstructionSkill), 2)]
    public partial class BrickAqueductBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(BrickItem); } }
    }



    [RotatedVariants(typeof(BrickStairsBlock), typeof(BrickStairs90Block), typeof(BrickStairs180Block), typeof(BrickStairs270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Stairs", typeof(BrickItem))]
    [RequiresSkill(typeof(Tier2ConstructionSkill), 1)]
    public partial class BrickStairsBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [RequiresSkill(typeof(Tier2ConstructionSkill), 1)]
    public partial class BrickStairs90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [RequiresSkill(typeof(Tier2ConstructionSkill), 1)]
    public partial class BrickStairs180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [RequiresSkill(typeof(Tier2ConstructionSkill), 1)]
    public partial class BrickStairs270Block : Block
    { }

}
