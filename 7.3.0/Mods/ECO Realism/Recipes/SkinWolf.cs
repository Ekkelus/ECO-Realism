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
    public class SkinWolfRecipe : Recipe
    {
        public SkinWolfRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedWolfItem>(1),
               new CraftingElement<FurPeltItem>(3),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<WolfCarcassItem>(typeof(SkinningEfficiencySkill), 1, SkinningEfficiencySkill.MultiplicativeStrategy),
            };
            this.Initialize("Skin Wolf", typeof(SkinWolfRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SkinWolfRecipe), this.UILink(), 1, typeof(SkinningSpeedSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}