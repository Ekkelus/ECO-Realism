namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(ButcherySkill), 3)] 
    public class ButcherFoxRecipe : Recipe
    {
        public ButcherFoxRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawMeatItem>(3),
               new CraftingElement<BoneItem>(4),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SkinnedFoxItem>(typeof(ButcherySkill), 1, ButcherySkill.MultiplicativeStrategy), 
            };
            this.Initialize(Localizer.DoStr("Butcher Fox"), typeof(ButcherFoxRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(ButcherFoxRecipe), this.UILink(), 1, typeof(ButcherySkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}