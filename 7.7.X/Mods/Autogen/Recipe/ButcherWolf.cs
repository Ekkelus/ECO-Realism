namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(ButcherySkill), 2)] 
    public class ButcherWolfRecipe : Recipe
    {
        public ButcherWolfRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<RawMeatItem>(4),
               new CraftingElement<BoneItem>(6),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SkinnedWolfItem>(typeof(ButcherySkill), 1, ButcherySkill.MultiplicativeStrategy), 
            };
            Initialize(Localizer.DoStr("Butcher Wolf"), typeof(ButcherWolfRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(ButcherWolfRecipe), this.UILink(), 1.5f, typeof(ButcherySkill), typeof(ButcheryFocusedSpeedTalent));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}