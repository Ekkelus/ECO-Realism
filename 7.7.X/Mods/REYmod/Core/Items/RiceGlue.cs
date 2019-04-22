namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(AdvancedCampfireCookingSkill), 1)]
    public class RiceGlueRecipe : Recipe
    {
        public RiceGlueRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<GlueItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<RiceSludgeItem>(typeof(AdvancedCampfireCookingSkill), 5, AdvancedCampfireCookingSkill.MultiplicativeStrategy),
                new CraftingElement<GlassJarItem>(),
            };
            Initialize(Localizer.DoStr("Rice Glue"), typeof(RiceGlueRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(RiceGlueRecipe), this.UILink(), 5, typeof(AdvancedCampfireCookingSkill), typeof(AdvancedCampfireCookingFocusedSpeedTalent));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}