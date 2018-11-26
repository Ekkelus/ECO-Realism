namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(LargeButcherySkill), 3)] 
    public class ButcherBisonRecipe : Recipe
    {
        public ButcherBisonRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawMeatItem>(20),
               new CraftingElement<BoneItem>(6),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SkinnedBisonItem>(typeof(LargeButcheryEfficiencySkill), 1, LargeButcheryEfficiencySkill.MultiplicativeStrategy), 
            };
            this.Initialize("Butcher Bison", typeof(ButcherBisonRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(ButcherBisonRecipe), this.UILink(), 3, typeof(LargeButcherySpeedSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}