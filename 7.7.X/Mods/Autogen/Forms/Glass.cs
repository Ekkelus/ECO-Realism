namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Items;
    using Eco.Shared.Serialization;
    using Eco.World;
    using Eco.World.Blocks;



    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Window", typeof(GlassItem))]
    public partial class GlassWindowBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(GlassItem); } }
    }


}
