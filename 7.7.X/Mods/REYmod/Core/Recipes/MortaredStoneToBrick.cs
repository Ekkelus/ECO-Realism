

/* CURRENTLY DISABLED
namespace Eco.Mods.TechTree
{
    [RequiresSkill(typeof(BrickProductionEfficiencySkill), 10)]
    class MortarBrickRecipe : Recipe
    {
        public MortarBrickRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<BrickItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<MortaredStoneItem>(typeof(BrickProductionEfficiencySkill), 5f, BrickProductionEfficiencySkill.MultiplicativeStrategy),
            };
            this.Initialize(Localizer.DoStr("Upgrade Mortared Stones"), typeof(MortarBrickRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(MortarBrickRecipe), this.UILink(), 1, typeof(BrickProductionSpeedSkill));
            CraftingComponent.AddRecipe(typeof(KilnObject), this);
        }
    }
}
*/