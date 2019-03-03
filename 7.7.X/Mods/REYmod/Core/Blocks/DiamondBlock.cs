using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.World;
using Eco.World.Blocks;

namespace REYmod.Blocks
{
    [Serialized]
    [Minable(3), Solid, Wall]
    [BecomesRubble(typeof(DiamondRubble1Object), typeof(DiamondRubble2Object), typeof(DiamondRubble3Object), typeof(DiamondRubble4Object))]
    public partial class DiamondBlock :
        Block
    { }

    [Serialized]
    [Weight(1000)]
    [MaxStackSize(20)]
    public class RawDiamondItem : Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Raw Diamond"); } }
    }

    [Serialized]
    public class DiamondRubble4Object : RubbleObject<RawDiamondItem>
    {    }

    [Serialized]
    public class DiamondRubble3Object : RubbleObject<RawDiamondItem>
    {    }

    [Serialized]
    public class DiamondRubble2Object : RubbleObject<RawDiamondItem>
    {    }

    [Serialized]
    public class DiamondRubble1Object : RubbleObject<RawDiamondItem>
    {    }
}
