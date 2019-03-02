namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(HuntingSkill), 1)]
    public class SkinTurkeyRecipe : Recipe
    {
        public SkinTurkeyRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedTurkeyItem>(1),
               new CraftingElement<LeatherHideItem>(typeof(HuntingSkill), 1, HuntingSkill.MultiplicativeStrategy),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TurkeyCarcassItem>(1),
            };
            this.Initialize(Localizer.DoStr("Skin Turkey"), typeof(SkinTurkeyRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SkinTurkeyRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}