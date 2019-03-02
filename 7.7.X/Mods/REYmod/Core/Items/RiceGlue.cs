namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(AdvancedCampfireCookingSkill), 1)]
    public class RiceGlueRecipe : Recipe
    {
        public RiceGlueRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<GlueItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<RiceSludgeItem>(typeof(AdvancedCampfireCookingSkill), 5, AdvancedCampfireCookingSkill.MultiplicativeStrategy),
                new CraftingElement<GlassJarItem>(1),
            };
            this.Initialize(Localizer.DoStr("Rice Glue"), typeof(RiceGlueRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(RiceGlueRecipe), this.UILink(), 5, typeof(AdvancedCampfireCookingSkill));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}