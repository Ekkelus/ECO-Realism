namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(LargeButcherySkill), 1)] 
    public class ButcherElkRecipe : Recipe
    {
        public ButcherElkRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawMeatItem>(10),
               new CraftingElement<BoneItem>(4),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SkinnedElkItem>(typeof(LargeButcheryEfficiencySkill), 1, LargeButcheryEfficiencySkill.MultiplicativeStrategy), 
            };
            this.Initialize(Localizer.Do("Butcher Elk"), typeof(ButcherElkRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(ButcherElkRecipe), this.UILink(), 1, typeof(LargeButcherySpeedSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}