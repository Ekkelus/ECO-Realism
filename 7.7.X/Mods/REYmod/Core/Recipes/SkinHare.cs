namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(HuntingSkill), 1)]
    public class SkinHareRecipe : Recipe
    {
        public SkinHareRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedHareItem>(),
               new CraftingElement<FurPeltItem>(typeof(HuntingSkill), 1, HuntingSkill.MultiplicativeStrategy),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<HareCarcassItem>(),
            };
            Initialize(Localizer.DoStr("Skin Hare"), typeof(SkinHareRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(SkinHareRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}