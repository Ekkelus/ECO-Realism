namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(HuntingSkill), 2)]
    public class SkinFoxRecipe : Recipe
    {
        public SkinFoxRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedFoxItem>(1),
               new CraftingElement<FurPeltItem>(typeof(HuntingSkill), 2, HuntingSkill.MultiplicativeStrategy),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FoxCarcassItem>(1),
            };
            this.Initialize(Localizer.DoStr("Skin Fox"), typeof(SkinFoxRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SkinFoxRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}