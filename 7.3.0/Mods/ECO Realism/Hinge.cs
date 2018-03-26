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

    [RequiresSkill(typeof(MetalworkingSkill), 1)]
    public partial class HingeRecipe : Recipe
    {
        public HingeRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<HingeItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(MetalworkingEfficiencySkill), 4, MetalworkingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(HingeRecipe), Item.Get<HingeItem>().UILink(), 7, typeof(MetalworkingSpeedSkill));
            this.Initialize("Hinge", typeof(HingeRecipe));

            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }


    [Serialized]
    [Weight(2000)]
    [Currency]
    public partial class HingeItem :
    Item
    {
        public override string FriendlyName { get { return "Hinge"; } }
        public override string Description { get { return "Allows one component to pivot off the other."; } }

    }

}