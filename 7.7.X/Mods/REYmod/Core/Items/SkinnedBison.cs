namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Items;
    using Shared.Serialization;
    using REYmod.Utils;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Components;
    using Gameplay.Systems.TextLinks;

    [Serialized]
    [Weight(3000)]
    [Currency]
    public partial class SkinnedBisonItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Skinned Bison"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A skinned bison."); } }

    }

    [RequiresSkill(typeof(HuntingSkill), 4)]
    public class SkinBisonRecipe : Recipe
    {
        public SkinBisonRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedBisonItem>(),
               new CraftingElement<LeatherHideItem>(typeof(HuntingSkill), 3, HuntingSkill.MultiplicativeStrategy.Inverted()),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BisonCarcassItem>(),
            };
            Initialize(Localizer.DoStr("Skin Bison"), typeof(SkinBisonRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(SkinBisonRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }

}