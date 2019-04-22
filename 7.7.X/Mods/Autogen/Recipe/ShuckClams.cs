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
    public class ShuckClamsRecipe : Recipe
    {
        public ShuckClamsRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawFishItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ClamItem>(typeof(HuntingSkill), 5, HuntingSkill.MultiplicativeStrategy), 
            };
            this.Initialize(Localizer.DoStr("Shuck Clams"), typeof(ShuckClamsRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(ShuckClamsRecipe), this.UILink(), 0.2f, typeof(HuntingSkill), typeof(HuntingFocusedSpeedTalent));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
} */