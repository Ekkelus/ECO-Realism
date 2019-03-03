namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(ButcherySkill), 2)] 
    public class ButcherWolfRecipe : Recipe
    {
        public ButcherWolfRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawMeatItem>(4),
               new CraftingElement<BoneItem>(6),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SkinnedWolfItem>(typeof(ButcherySkill), 1, ButcherySkill.MultiplicativeStrategy), 
            };
            this.Initialize(Localizer.DoStr("Butcher Wolf"), typeof(ButcherWolfRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(ButcherWolfRecipe), this.UILink(), 1.5f, typeof(ButcherySkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}