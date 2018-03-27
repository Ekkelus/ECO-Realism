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

    [RequiresSkill(typeof(GlassworkingSkill), 1)]
    public partial class GlassJarRecipe : Recipe
    {
        public GlassJarRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<GlassJarItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GlassItem>(typeof(GlassProductionEfficiencySkill), 3, GlassProductionEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(GlassJarRecipe), Item.Get<GlassJarItem>().UILink(), 2, typeof(GlassProductionSpeedSkill));
            this.Initialize("Glass Jar", typeof(GlassJarRecipe));

            CraftingComponent.AddRecipe(typeof(KilnObject), this);
        }
    }


    [Serialized]
    [Weight(100)]
    [Currency]
    public partial class GlassJarItem :
    Item
    {
        public override string FriendlyName { get { return "Glass Jar"; } }
        public override string Description { get { return "A glass container, capable of holding liquids."; } }

    }

}