namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(ButcherySkill), 3)] 
    public class ButcherBisonRecipe : Recipe
    {
        public ButcherBisonRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<RawMeatItem>(20),
               new CraftingElement<BoneItem>(6),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SkinnedBisonItem>(typeof(ButcherySkill), 1, ButcherySkill.MultiplicativeStrategy), 
            };
            Initialize(Localizer.DoStr("Butcher Bison"), typeof(ButcherBisonRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(ButcherBisonRecipe), this.UILink(), 3, typeof(ButcherySkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}