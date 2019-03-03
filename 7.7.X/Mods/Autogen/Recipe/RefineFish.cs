namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(CuttingEdgeCookingSkill), 1)] 
    public class RefineFishRecipe : Recipe
    {
        public RefineFishRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<HydrocolloidsItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TunaItem>(typeof(CuttingEdgeCookingSkill), 5, CuttingEdgeCookingSkill.MultiplicativeStrategy),
                new CraftingElement<SalmonItem>(typeof(CuttingEdgeCookingSkill), 5, CuttingEdgeCookingSkill.MultiplicativeStrategy),
                new CraftingElement<TroutItem>(typeof(CuttingEdgeCookingSkill), 5, CuttingEdgeCookingSkill.MultiplicativeStrategy),
            };
            this.Initialize(Localizer.DoStr("Refine Fish"), typeof(RefineFishRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(RefineFishRecipe), this.UILink(), 5, typeof(CuttingEdgeCookingSkill));
            CraftingComponent.AddRecipe(typeof(LaboratoryObject), this);
        }
    }
}