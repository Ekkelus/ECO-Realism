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


    public partial class StringRecipe : Recipe
    {
        public StringRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<StringItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PlantFibersItem>(typeof(ClothProductionEfficiencySkill), 10, ClothProductionEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(StringRecipe), Item.Get<StringItem>().UILink(), 0.5f, typeof(ClothProductionSpeedSkill));
            this.Initialize("String", typeof(StringRecipe));

            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }


    [Serialized]
    [Weight(200)]
    [Currency]
    public partial class StringItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("String"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Several fibers twisted together to form a long thread."); } }

    }

}