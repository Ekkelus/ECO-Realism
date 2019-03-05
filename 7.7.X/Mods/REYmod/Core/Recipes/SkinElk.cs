namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(HuntingSkill), 3)]
    public class SkinElkRecipe : Recipe
    {
        public SkinElkRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedElkItem>(),
               new CraftingElement<LeatherHideItem>(typeof(HuntingSkill), 2, HuntingSkill.MultiplicativeStrategy),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<ElkCarcassItem>(),
            };
            Initialize(Localizer.DoStr("Skin Elk"), typeof(SkinElkRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(SkinElkRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}