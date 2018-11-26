namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(MolecularGastronomySkill), 1)] 
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
                new CraftingElement<TunaItem>(typeof(MolecularGastronomyEfficiencySkill), 5, MolecularGastronomyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SalmonItem>(typeof(MolecularGastronomyEfficiencySkill), 5, MolecularGastronomyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<TroutItem>(typeof(MolecularGastronomyEfficiencySkill), 5, MolecularGastronomyEfficiencySkill.MultiplicativeStrategy),
            };
            this.Initialize(Localizer.DoStr("Refine Fish"), typeof(RefineFishRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(RefineFishRecipe), this.UILink(), 5, typeof(MolecularGastronomySpeedSkill));
            CraftingComponent.AddRecipe(typeof(LaboratoryObject), this);
        }
    }
}