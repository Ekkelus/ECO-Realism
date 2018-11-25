namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Shared.Localization;
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
    using Eco.Shared.Serialization;
	using Eco.Shared.Localization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;

    [RequiresSkill(typeof(PrimitiveMechanicsSkill), 1)]
    public partial class WoodenGearRecipe : Recipe
    {
        public WoodenGearRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WoodenGearItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(PrimitiveMechanicsEfficiencySkill), 10, PrimitiveMechanicsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(PrimitiveMechanicsEfficiencySkill), 10, PrimitiveMechanicsEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(WoodenGearRecipe), Item.Get<WoodenGearItem>().UILink(), 6, typeof(PrimitiveMechanicsSpeedSkill));
            this.Initialize("Wooden Gear", typeof(WoodenGearRecipe));

            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }


    [Serialized]
    [Weight(2000)]
    [Currency]
    public partial class WoodenGearItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wooden Gear"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A toothed machine part that interlocks with others."); } }

    }

}