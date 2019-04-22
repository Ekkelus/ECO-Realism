namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(HuntingSkill), 2)]
    public class SkinFoxRecipe : Recipe
    {
        public SkinFoxRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedFoxItem>(),
               new CraftingElement<FurPeltItem>(typeof(HuntingSkill), 2, HuntingSkill.MultiplicativeStrategy),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<FoxCarcassItem>(),
            };
            Initialize(Localizer.DoStr("Skin Fox"), typeof(SkinFoxRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(SkinFoxRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}