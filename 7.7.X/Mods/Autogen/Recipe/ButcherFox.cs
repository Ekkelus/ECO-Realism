namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(ButcherySkill), 3)] 
    public class ButcherFoxRecipe : Recipe
    {
        public ButcherFoxRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<RawMeatItem>(3),
               new CraftingElement<BoneItem>(4),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SkinnedFoxItem>(typeof(ButcherySkill), 1, ButcherySkill.MultiplicativeStrategy), 
            };
            Initialize(Localizer.DoStr("Butcher Fox"), typeof(ButcherFoxRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(ButcherFoxRecipe), this.UILink(), 1, typeof(ButcherySkill), typeof(ButcheryFocusedSpeedTalent));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}