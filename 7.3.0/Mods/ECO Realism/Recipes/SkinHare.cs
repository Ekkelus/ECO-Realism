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
    public class SkinHareRecipe : Recipe
    {
        public SkinHareRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedHareItem>(1),
               new CraftingElement<FurPeltItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HareCarcassItem>(typeof(SkinningEfficiencySkill), 1, SkinningEfficiencySkill.MultiplicativeStrategy),
            };
            this.Initialize("Skin Hare", typeof(SkinHareRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SkinHareRecipe), this.UILink(), 1, typeof(SkinningSpeedSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}