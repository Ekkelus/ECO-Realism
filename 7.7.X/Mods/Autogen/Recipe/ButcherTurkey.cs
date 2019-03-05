namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(ButcherySkill), 1)] 
    public class ButcherTurkeyRecipe : Recipe
    {
        public ButcherTurkeyRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<RawMeatItem>(2),
               new CraftingElement<BoneItem>(2),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SkinnedTurkeyItem>(typeof(ButcherySkill), 1, ButcherySkill.MultiplicativeStrategy), 
            };
            Initialize(Localizer.DoStr("Butcher Turkey"), typeof(ButcherTurkeyRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(ButcherTurkeyRecipe), this.UILink(), 1, typeof(ButcherySkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}