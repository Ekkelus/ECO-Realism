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

    [RequiresSkill(typeof(HuntingSkill), 2)] 
    public class CleanSalmonRecipe : Recipe
    {
        public CleanSalmonRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawFishItem>(5),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SalmonItem>(typeof(HuntingSkill), 1, HuntingSkill.MultiplicativeStrategy), 
            };
            this.Initialize(Localizer.DoStr("Clean Salmon"), typeof(CleanSalmonRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(CleanSalmonRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
} */