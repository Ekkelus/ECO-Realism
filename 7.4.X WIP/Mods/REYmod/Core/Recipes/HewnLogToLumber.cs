using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Utils;
using Eco.World;
using Eco.World.Blocks;
using Eco.Gameplay.Systems.TextLinks;

namespace Eco.Mods.TechTree
{
    [RequiresSkill(typeof(LumberProcessingEfficiencySkill), 10)]
    class HewnLumberRecipe : Recipe
    {
        public HewnLumberRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<LumberItem>(),
               new CraftingElement<WoodPulpItem>(3),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(LumberProcessingEfficiencySkill), 10f, LumberProcessingEfficiencySkill.MultiplicativeStrategy),
            };
            this.Initialize("Upgrade Hewn Logs", typeof(HewnLumberRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(HewnLumberRecipe), this.UILink(), 1, typeof(LumberProcessingSpeedSkill));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}
