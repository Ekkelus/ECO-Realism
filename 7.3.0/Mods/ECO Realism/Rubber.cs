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

    [RequiresSkill(typeof(PetrolRefiningSkill), 2)]
    public partial class RubberRecipe : Recipe
    {
        public RubberRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RubberItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PetroleumItem>(typeof(PetrolRefiningEfficiencySkill), 8, PetrolRefiningEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(RubberRecipe), Item.Get<RubberItem>().UILink(), 15, typeof(PetrolRefiningSpeedSkill));
            this.Initialize("Rubber", typeof(RubberRecipe));

            CraftingComponent.AddRecipe(typeof(OilRefineryObject), this);
        }
    }


    [Serialized]
    [Weight(1000)]
    [Currency]
    public partial class RubberItem :
    Item
    {
        public override string FriendlyName { get { return "Rubber"; } }
        public override string FriendlyNamePlural { get { return "Rubber"; } }
        public override string Description { get { return "With a bit of forming and air this will make you drive around."; } }

    }

}