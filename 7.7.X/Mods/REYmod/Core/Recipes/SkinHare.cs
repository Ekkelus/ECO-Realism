namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(SkinningSkill), 1)]
    public class SkinHareRecipe : Recipe
    {
        public SkinHareRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedHareItem>(1),
               new CraftingElement<FurPeltItem>(typeof(SkinningEfficiencySkill), 1, SkinningEfficiencySkill.MultiplicativeStrategy),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HareCarcassItem>(1),
            };
            this.Initialize(Localizer.DoStr("Skin Hare"), typeof(SkinHareRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SkinHareRecipe), this.UILink(), 1, typeof(SkinningSpeedSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}