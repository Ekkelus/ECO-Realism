namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(ButcherySkill), 0)] 
    public class ButcherOtterRecipe : Recipe
    {
        public ButcherOtterRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawMeatItem>(1f),  
               new CraftingElement<FurPeltItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SkinnedOtterItem>(typeof(ButcherySkill), 1, ButcherySkill.MultiplicativeStrategy), 
            };
            this.Initialize(Localizer.DoStr("Butcher Otter"), typeof(ButcherOtterRecipe));
            this.ExperienceOnCraft = 4;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(ButcherOtterRecipe), this.UILink(), 1, typeof(ButcherySkill), typeof(ButcheryFocusedSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}