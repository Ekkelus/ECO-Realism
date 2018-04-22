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

    [RequiresSkill(typeof(SkinningSkill), 1)]
    public class SkinTurkeyRecipe : Recipe
    {
        public SkinTurkeyRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedTurkeyItem>(1),
               new CraftingElement<LeatherHideItem>(typeof(SkinningEfficiencySkill), 1, SkinningEfficiencySkill.MultiplicativeStrategy),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TurkeyCarcassItem>(1),
            };
            this.Initialize("Skin Turkey", typeof(SkinTurkeyRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SkinTurkeyRecipe), this.UILink(), 1, typeof(SkinningSpeedSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}