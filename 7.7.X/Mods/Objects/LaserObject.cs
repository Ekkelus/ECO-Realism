// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Objects;
using Eco.Mods.TechTree;
using Eco.Shared.Serialization;
using REYmod.Utils;

[Serialized]
[RequireComponent(typeof(MinimapComponent))]
[RequireComponent(typeof(PowerGridComponent))]
[RequireComponent(typeof(PowerConsumptionComponent))]
[RequireComponent(typeof(ChargingComponent))]
[RequireComponent(typeof(PowerGridNetworkComponent))]
[RequireComponent(typeof(SpecificGroundComponent))]
[RequireComponent(typeof(PropertyAuthComponent))]
public class LaserObject : WorldObject
{
    public float MinimapYaw { get { return 0.0f; } }
    public bool HideOnMinimap { get { return false; } }

    protected override void Initialize()
    {
        base.Initialize();
        GetComponent<MinimapComponent>().Initialize("Laser");
        GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());
        GetComponent<PowerConsumptionComponent>().Initialize(9000);
        GetComponent<ChargingComponent>().Initialize(30, 30);
        GetComponent<PowerGridNetworkComponent>().Initialize(new Dictionary<Type, int> { { typeof(LaserObject), 8 }, { typeof(ComputerLabObject), 4 } }, false);
        GetComponent<SpecificGroundComponent>().Initialize(typeof(ReinforcedConcreteItem));
    }
}