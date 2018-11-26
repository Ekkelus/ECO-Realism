namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(SkinningSkill), 2)]
    public class SkinFoxRecipe : Recipe
    {
        public SkinFoxRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedFoxItem>(1),
               new CraftingElement<FurPeltItem>(typeof(SkinningEfficiencySkill), 2, SkinningEfficiencySkill.MultiplicativeStrategy),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FoxCarcassItem>(1),
            };
            this.Initialize(Localizer.DoStr("Skin Fox"), typeof(SkinFoxRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SkinFoxRecipe), this.UILink(), 1, typeof(SkinningSpeedSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}