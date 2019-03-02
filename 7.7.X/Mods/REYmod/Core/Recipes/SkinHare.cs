namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(HuntingSkill), 1)]
    public class SkinHareRecipe : Recipe
    {
        public SkinHareRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedHareItem>(1),
               new CraftingElement<FurPeltItem>(typeof(HuntingSkill), 1, HuntingSkill.MultiplicativeStrategy),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HareCarcassItem>(1),
            };
            this.Initialize(Localizer.DoStr("Skin Hare"), typeof(SkinHareRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SkinHareRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}