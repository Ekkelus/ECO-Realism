namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(LargeButcherySkill), 2)] 
    public class ButcherWolfRecipe : Recipe
    {
        public ButcherWolfRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawMeatItem>(4),
               new CraftingElement<BoneItem>(6),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SkinnedWolfItem>(typeof(LargeButcheryEfficiencySkill), 1, LargeButcheryEfficiencySkill.MultiplicativeStrategy), 
            };
            this.Initialize(Localizer.Do("Butcher Wolf"), typeof(ButcherWolfRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(ButcherWolfRecipe), this.UILink(), 1.5f, typeof(LargeButcherySpeedSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}