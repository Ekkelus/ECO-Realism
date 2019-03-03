namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(ButcherySkill), 3)] 
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
                new CraftingElement<SkinnedBisonItem>(typeof(ButcherySkill), 1, ButcherySkill.MultiplicativeStrategy), 
            };
            this.Initialize(Localizer.DoStr("Butcher Bison"), typeof(ButcherBisonRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(ButcherBisonRecipe), this.UILink(), 3, typeof(ButcherySkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}