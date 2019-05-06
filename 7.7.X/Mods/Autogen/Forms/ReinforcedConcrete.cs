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
    [Tier(3)]
    [IsForm("Floor", typeof(ReinforcedConcreteItem))]
    public partial class ReinforcedConcreteFloorBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(ReinforcedConcreteItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Wall", typeof(ReinforcedConcreteItem))]
    public partial class ReinforcedConcreteWallBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(ReinforcedConcreteItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Cube", typeof(ReinforcedConcreteItem))]
    [RequiresSkill(typeof(Tier3ConstructionSkill), 4)]
    public partial class ReinforcedConcreteCubeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(ReinforcedConcreteItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Roof", typeof(ReinforcedConcreteItem))]
    [RequiresSkill(typeof(Tier3ConstructionSkill), 2)]
    public partial class ReinforcedConcreteRoofBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(ReinforcedConcreteItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Column", typeof(ReinforcedConcreteItem))]
    [RequiresSkill(typeof(Tier3ConstructionSkill), 4)]
    public partial class ReinforcedConcreteColumnBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(ReinforcedConcreteItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Window", typeof(ReinforcedConcreteItem))]
    [RequiresSkill(typeof(Tier3ConstructionSkill), 3)]
    public partial class ReinforcedConcreteWindowBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(ReinforcedConcreteItem); } }
    }



    [RotatedVariants(typeof(ReinforcedConcreteStairsBlock), typeof(ReinforcedConcreteStairs90Block), typeof(ReinforcedConcreteStairs180Block), typeof(ReinforcedConcreteStairs270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Stairs", typeof(ReinforcedConcreteItem))]
    [RequiresSkill(typeof(Tier3ConstructionSkill), 1)]
    public partial class ReinforcedConcreteStairsBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [RequiresSkill(typeof(Tier3ConstructionSkill), 1)]
    public partial class ReinforcedConcreteStairs90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [RequiresSkill(typeof(Tier3ConstructionSkill), 1)]
    public partial class ReinforcedConcreteStairs180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [RequiresSkill(typeof(Tier3ConstructionSkill), 1)]
    public partial class ReinforcedConcreteStairs270Block : Block
    { }

}
