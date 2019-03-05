// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Objects;
    using Gameplay.Property;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireComponent(typeof(PowerConsumptionComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(PowerGridComponent))]
    [RequireComponent(typeof(PowerGridNetworkComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireComponent(typeof(PowerConsumptionComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(45)]
    [RequireRoomMaterialTier(3.8f)]
    public partial class ComputerLabObject : WorldObject
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Computer Lab"); } }

        protected override void Initialize()
        {
            base.Initialize();

            GetComponent<MinimapComponent>().Initialize("Computer Lab");
            GetComponent<LinkComponent>().Initialize(5);
            GetComponent<PowerConsumptionComponent>().Initialize(1000);
            GetComponent<PowerGridComponent>().Initialize(10.0f, new ElectricPower());
            GetComponent<PowerGridNetworkComponent>().Initialize(new Dictionary<Type, int> { { typeof(LaserObject), 8 }, { typeof(ComputerLabObject), 4 } }, true);
        }
    }
}