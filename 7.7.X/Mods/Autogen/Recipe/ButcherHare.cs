namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(ButcherySkill), 1)] 
    public class ButcherHareRecipe : Recipe
    {
        public ButcherHareRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<RawMeatItem>(),
               new CraftingElement<BoneItem>(2),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SkinnedHareItem>(typeof(ButcherySkill), 1, ButcherySkill.MultiplicativeStrategy), 
            };
            Initialize(Localizer.DoStr("Butcher Hare"), typeof(ButcherHareRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(ButcherHareRecipe), this.UILink(), 1, typeof(ButcherySkill), typeof(ButcheryFocusedSpeedTalent));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}