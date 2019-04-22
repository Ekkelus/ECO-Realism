namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(HuntingSkill), 1)]
    public class SkinTurkeyRecipe : Recipe
    {
        public SkinTurkeyRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedTurkeyItem>(),
               new CraftingElement<LeatherHideItem>(typeof(HuntingSkill), 1, HuntingSkill.MultiplicativeStrategy),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<TurkeyCarcassItem>(),
            };
            Initialize(Localizer.DoStr("Skin Turkey"), typeof(SkinTurkeyRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(SkinTurkeyRecipe), this.UILink(), 1, typeof(HuntingSkill), typeof(HuntingFocusedSpeedTalent));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}