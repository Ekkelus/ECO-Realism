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

    [RequiresSkill(typeof(MeatPrepSkill), 1)]
    public partial class GlueRecipe : Recipe
    {
        public GlueRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<GlueItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoneItem>(typeof(MeatPrepEfficiencySkill), 5, MeatPrepEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GlassJarItem>(1),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(GlueRecipe), Item.Get<GlueItem>().UILink(), 3, typeof(MeatPrepSpeedSkill));
            this.Initialize("Glue", typeof(GlueRecipe));

            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }


    [Serialized]
    [Weight(200)]
    [Currency]
    public partial class GlueItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Glue"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Sticks two components together."); } }

    }

}