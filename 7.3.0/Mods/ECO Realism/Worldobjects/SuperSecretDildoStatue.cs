namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;

    [Serialized]
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(SolidGroundComponent))]
    public partial class SuperSecretDildoStatueObject :
        WorldObject
    {
        public override string FriendlyName { get { return "Super Secret Dildo Statue"; } }


        protected override void Initialize()
        {
            this.GetComponent<HousingComponent>().Set(SuperSecretDildoStatueItem.HousingVal);



        }

        public override void Destroy()
        {
            base.Destroy();
        }

    }

    [Serialized]
    public partial class SuperSecretDildoStatueItem : WorldObjectItem<SuperSecretDildoStatueObject>
    {
        public override string FriendlyName { get { return "Super Secret Dildo Statue"; } }
        public override string Description { get { return "Think long and hard before you go out looking for this."; } }

        static SuperSecretDildoStatueItem()
        {

        }

        [TooltipChildren]
        public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren]
        public static HousingValue HousingVal
        {
            get
            {
                return new HousingValue()
                {
                    Category = "General",
                    Val = 1000,
                    TypeForRoomLimit = "Decoration",
                    DiminishingReturnPercent = 1.5f
                };
            }
        }
    }
}