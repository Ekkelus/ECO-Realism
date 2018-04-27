using System;
using System.Collections.Generic;
using System.ComponentModel;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using Eco.World;
using Eco.World.Blocks;
using Eco.Gameplay.Pipes;
using Eco.Mods.TechTree;

namespace REYmod.Blocks
{
    [Serialized]
    [Minable, Solid, Wall]
    [BecomesRubble(typeof(DiamondRubble1Object), typeof(DiamondRubble2Object), typeof(DiamondRubble3Object), typeof(DiamondRubble4Object))]
    public partial class DiamondBlock :
        Block
    { }

    [Serialized]
    [Weight(1000)]
    [MaxStackSize(20)]
    public class DiamondItem : Item
    {
        public override string FriendlyName { get { return "Diamond"; } }
    }

    [Serialized]
    public class DiamondRubble4Object : RubbleObject<DiamondItem>
    {    }

    [Serialized]
    public class DiamondRubble3Object : RubbleObject<DiamondItem>
    {    }

    [Serialized]
    public class DiamondRubble2Object : RubbleObject<DiamondItem>
    {    }

    [Serialized]
    public class DiamondRubble1Object : RubbleObject<DiamondItem>
    {    }
}
