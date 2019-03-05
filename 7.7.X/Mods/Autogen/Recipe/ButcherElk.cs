namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(ButcherySkill), 1)] 
    public class ButcherElkRecipe : Recipe
    {
        public ButcherElkRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<RawMeatItem>(10),
               new CraftingElement<BoneItem>(4),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SkinnedElkItem>(typeof(ButcherySkill), 1, ButcherySkill.MultiplicativeStrategy), 
            };
            Initialize(Localizer.DoStr("Butcher Elk"), typeof(ButcherElkRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(ButcherElkRecipe), this.UILink(), 1, typeof(ButcherySkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}