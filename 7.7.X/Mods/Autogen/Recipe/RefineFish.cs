namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(CuttingEdgeCookingSkill), 1)] 
    public class RefineFishRecipe : Recipe
    {
        public RefineFishRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<HydrocolloidsItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<TunaItem>(typeof(CuttingEdgeCookingSkill), 5, CuttingEdgeCookingSkill.MultiplicativeStrategy),
                new CraftingElement<SalmonItem>(typeof(CuttingEdgeCookingSkill), 5, CuttingEdgeCookingSkill.MultiplicativeStrategy),
                new CraftingElement<TroutItem>(typeof(CuttingEdgeCookingSkill), 5, CuttingEdgeCookingSkill.MultiplicativeStrategy),
            };
            Initialize(Localizer.DoStr("Refine Fish"), typeof(RefineFishRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(RefineFishRecipe), this.UILink(), 5, typeof(CuttingEdgeCookingSkill), typeof(CuttingEdgeCookingFocusedSpeedTalent));
            CraftingComponent.AddRecipe(typeof(LaboratoryObject), this);
        }
    }
}