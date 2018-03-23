// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

using Eco.Gameplay.Components;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;
using Eco.Shared.Serialization;

[RequiresSkill(typeof(WoodworkingSkill), 1)]
public class DoorRecipe : Recipe
{
    public DoorRecipe()
    {
        this.Products = new CraftingElement[]
        {
            new CraftingElement<DoorItem>(),
        };
        this.Ingredients = new CraftingElement[]
        {
            new CraftingElement<LogItem>(typeof(WoodworkingEfficiencySkill), 6, new PercentReductionStrategy(2, 0.1f)),
            new CraftingElement<HingeItem>(typeof(WoodworkingEfficiencySkill), 4, new PercentReductionStrategy(2, 0.1f)),
            new CraftingElement<NailsItem>(typeof(WoodworkingEfficiencySkill), 8, new PercentReductionStrategy(2, 0.1f)),
            new CraftingElement<IronIngotItem>(typeof(WoodworkingEfficiencySkill), 2, new PercentReductionStrategy(2, 0.1f)),
        };
        this.CraftMinutes = new ConstantValue(5);

        this.Initialize("Door", typeof(DoorRecipe));
        CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
    }
}

[Serialized]
public class DoorItem : WorldObjectItem<DoorObject>
{
    public override string FriendlyName { get { return "Door"; } }

    public override string Description { get { return "A sturdy wood door.  Can be locked for certain players."; } }
}