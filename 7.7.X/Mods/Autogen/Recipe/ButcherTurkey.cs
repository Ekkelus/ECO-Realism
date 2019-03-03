namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(ButcherySkill), 1)] 
    public class ButcherTurkeyRecipe : Recipe
    {
        public ButcherTurkeyRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawMeatItem>(2),
               new CraftingElement<BoneItem>(2),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SkinnedTurkeyItem>(typeof(ButcherySkill), 1, ButcherySkill.MultiplicativeStrategy), 
            };
            this.Initialize(Localizer.DoStr("Butcher Turkey"), typeof(ButcherTurkeyRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(ButcherTurkeyRecipe), this.UILink(), 1, typeof(ButcherySkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}