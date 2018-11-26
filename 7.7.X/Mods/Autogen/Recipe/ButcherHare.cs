namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(SmallButcherySkill), 1)] 
    public class ButcherHareRecipe : Recipe
    {
        public ButcherHareRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawMeatItem>(1),
               new CraftingElement<BoneItem>(2),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SkinnedHareItem>(typeof(SmallButcheryEfficiencySkill), 1, SmallButcheryEfficiencySkill.MultiplicativeStrategy), 
            };
            this.Initialize(Localizer.Do("Butcher Hare"), typeof(ButcherHareRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(ButcherHareRecipe), this.UILink(), 1, typeof(SmallButcherySpeedSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}