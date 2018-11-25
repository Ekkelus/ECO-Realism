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

    public partial class RopeRecipe : Recipe
    {
        public RopeRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RopeItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StringItem>(typeof(ClothProductionEfficiencySkill), 5, ClothProductionEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(RopeRecipe), Item.Get<RopeItem>().UILink(), 2, typeof(ClothProductionSpeedSkill));
            this.Initialize("Rope", typeof(RopeRecipe));

            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }


    [Serialized]
    [Weight(1000)]
    [Currency]
    public partial class RopeItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Rope"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Several strings twisted together to form a long rope."); } }

    }

}