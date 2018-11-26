namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(SkinningSkill), 2)]
    public class SkinWolfRecipe : Recipe
    {
        public SkinWolfRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedWolfItem>(1),
               new CraftingElement<FurPeltItem>(typeof(SkinningEfficiencySkill), 3, SkinningEfficiencySkill.MultiplicativeStrategy),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<WolfCarcassItem>(1),
            };
            this.Initialize(Localizer.DoStr("Skin Wolf"), typeof(SkinWolfRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SkinWolfRecipe), this.UILink(), 1, typeof(SkinningSpeedSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}