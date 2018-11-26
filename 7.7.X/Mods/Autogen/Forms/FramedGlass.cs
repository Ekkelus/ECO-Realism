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
    [Tier(4)]
    [IsForm("Floor", typeof(FramedGlassItem))]
    public partial class FramedGlassFloorBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FramedGlassItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Wall", typeof(FramedGlassItem))]
    public partial class FramedGlassWallBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FramedGlassItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Cube", typeof(FramedGlassItem))]
    [RequiresSkill(typeof(Tier4ConstructionSkill), 4)]
    public partial class FramedGlassCubeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FramedGlassItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Roof", typeof(FramedGlassItem))]
    [RequiresSkill(typeof(Tier4ConstructionSkill), 2)]
    public partial class FramedGlassRoofBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FramedGlassItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Column", typeof(FramedGlassItem))]
    [RequiresSkill(typeof(Tier4ConstructionSkill), 4)]
    public partial class FramedGlassColumnBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FramedGlassItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Window", typeof(FramedGlassItem))]
    [RequiresSkill(typeof(Tier4ConstructionSkill), 3)]
    public partial class FramedGlassWindowBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FramedGlassItem); } }
    }



    [RotatedVariants(typeof(FramedGlassStairsBlock), typeof(FramedGlassStairs90Block), typeof(FramedGlassStairs180Block), typeof(FramedGlassStairs270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Stairs", typeof(FramedGlassItem))]
    [RequiresSkill(typeof(Tier4ConstructionSkill), 1)]
    public partial class FramedGlassStairsBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [RequiresSkill(typeof(Tier4ConstructionSkill), 1)]
    public partial class FramedGlassStairs90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [RequiresSkill(typeof(Tier4ConstructionSkill), 1)]
    public partial class FramedGlassStairs180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [RequiresSkill(typeof(Tier4ConstructionSkill), 1)]
    public partial class FramedGlassStairs270Block : Block
    { }

}
