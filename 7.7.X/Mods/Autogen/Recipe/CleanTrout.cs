/* namespace Eco.Mods.TechTree
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

    [RequiresSkill(typeof(HuntingSkill), 1)] 
    public class CleanTroutRecipe : Recipe
    {
        public CleanTroutRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawFishItem>(3),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TroutItem>(typeof(HuntingSkill), 1, HuntingSkill.MultiplicativeStrategy), 
            };
            this.Initialize(Localizer.DoStr("Clean Trout"), typeof(CleanTroutRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(CleanTroutRecipe), this.UILink(), 1.5f, typeof(HuntingSkill), typeof(HuntingFocusedSpeedTalent));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
} */