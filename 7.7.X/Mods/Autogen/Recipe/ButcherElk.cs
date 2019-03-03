namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(ButcherySkill), 1)] 
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
                new CraftingElement<SkinnedElkItem>(typeof(ButcherySkill), 1, ButcherySkill.MultiplicativeStrategy), 
            };
            this.Initialize(Localizer.DoStr("Butcher Elk"), typeof(ButcherElkRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(ButcherElkRecipe), this.UILink(), 1, typeof(ButcherySkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}