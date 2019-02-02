using Eco.Gameplay.Blocks;
using Eco.Gameplay.Objects;
using Eco.Shared.Serialization;
using Eco.World.Blocks;

namespace REYmod.Blocks
{
    [Serialized]
    [Wall, Transient, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    public class T4WorldObjectBlock : WorldObjectBlock
    {
        public T4WorldObjectBlock(WorldObject obj) : base(obj)
        { }

        protected T4WorldObjectBlock()
        { }
    }

    [Serialized]
    [Wall, Transient, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    public class T3WorldObjectBlock : WorldObjectBlock
    {
        public T3WorldObjectBlock(WorldObject obj) : base(obj)
        { }

        protected T3WorldObjectBlock()
        { }
    }

    [Serialized]
    [Wall, Transient, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    public class T2WorldObjectBlock : WorldObjectBlock
    {
        public T2WorldObjectBlock(WorldObject obj) : base(obj)
        { }

        protected T2WorldObjectBlock()
        { }
    }

    [Serialized]
    [Wall, Transient, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    public class T1WorldObjectBlock : WorldObjectBlock
    {
        public T1WorldObjectBlock(WorldObject obj) : base(obj)
        { }

        protected T1WorldObjectBlock()
        { }
    }


}
