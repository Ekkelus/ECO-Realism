namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Shared.Localization;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(SkinningSkill), 2)]
    public class SkinFoxRecipe : Recipe
    {
        public SkinFoxRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedFoxItem>(1),
               new CraftingElement<FurPeltItem>(typeof(SkinningEfficiencySkill), 2, SkinningEfficiencySkill.MultiplicativeStrategy),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FoxCarcassItem>(1),
            };
            this.Initialize("Skin Fox", typeof(SkinFoxRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SkinFoxRecipe), this.UILink(), 1, typeof(SkinningSpeedSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}