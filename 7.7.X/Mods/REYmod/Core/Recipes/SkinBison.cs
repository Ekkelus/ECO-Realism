namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(HuntingSkill), 4)]
    public class SkinBisonRecipe : Recipe
    {
        public SkinBisonRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedBisonItem>(),
               new CraftingElement<LeatherHideItem>(typeof(HuntingSkill), 3, HuntingSkill.MultiplicativeStrategy),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BisonCarcassItem>(),
            };
            Initialize(Localizer.DoStr("Skin Bison"), typeof(SkinBisonRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(SkinBisonRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}