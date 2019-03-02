namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(HuntingSkill), 2)]
    public class SkinWolfRecipe : Recipe
    {
        public SkinWolfRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedWolfItem>(1),
               new CraftingElement<FurPeltItem>(typeof(HuntingSkill), 3, HuntingSkill.MultiplicativeStrategy),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<WolfCarcassItem>(1),
            };
            this.Initialize(Localizer.DoStr("Skin Wolf"), typeof(SkinWolfRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SkinWolfRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}