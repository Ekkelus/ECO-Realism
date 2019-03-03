namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(ButcherySkill), 1)] 
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
                new CraftingElement<SkinnedHareItem>(typeof(ButcherySkill), 1, ButcherySkill.MultiplicativeStrategy), 
            };
            this.Initialize(Localizer.DoStr("Butcher Hare"), typeof(ButcherHareRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(ButcherHareRecipe), this.UILink(), 1, typeof(ButcherySkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}