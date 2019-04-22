namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(HuntingSkill), 2)]
    public class SkinWolfRecipe : Recipe
    {
        public SkinWolfRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedWolfItem>(),
               new CraftingElement<FurPeltItem>(typeof(HuntingSkill), 3, HuntingSkill.MultiplicativeStrategy),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<WolfCarcassItem>(),
            };
            Initialize(Localizer.DoStr("Skin Wolf"), typeof(SkinWolfRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(SkinWolfRecipe), this.UILink(), 1, typeof(HuntingSkill), typeof(HuntingFocusedSpeedTalent));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}