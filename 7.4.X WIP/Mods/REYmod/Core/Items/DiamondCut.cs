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
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;

    [RequiresSkill(typeof(BasicCraftingSkill), 2)]
    public partial class DiamondCutRecipe : Recipe
    {
        public DiamondCutRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<DiamondCutItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StringItem>(typeof(ClothProductionEfficiencySkill), 5, ClothProductionEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(DiamondCutRecipe), Item.Get<DiamondCutItem>().UILink(), 2, typeof(ClothProductionSpeedSkill));
            this.Initialize("Cut Diamond", typeof(DiamondCutRecipe));

            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }


    [Serialized]
    [Weight(1000)]
    [Currency]
    public partial class DiamondCutItem :
    Item
    {
        public override string FriendlyName { get { return "Diamond Cut"; } }
        public override string Description { get { return "Probably useful."; } }

    }

}