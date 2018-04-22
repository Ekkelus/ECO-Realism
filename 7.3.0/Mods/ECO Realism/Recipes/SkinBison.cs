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

    [RequiresSkill(typeof(SkinningSkill), 4)]
    public class SkinBisonRecipe : Recipe
    {
        public SkinBisonRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedBisonItem>(1),
               new CraftingElement<LeatherHideItem>(typeof(SkinningEfficiencySkill), 3, SkinningEfficiencySkill.MultiplicativeStrategy),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BisonCarcassItem>(1),
            };
            this.Initialize("Skin Bison", typeof(SkinBisonRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SkinBisonRecipe), this.UILink(), 1, typeof(SkinningSpeedSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}