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
    [Weight(1500)]
    [Currency]
    public partial class SkinnedBighornItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Skinned Bighorn"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A skinned Bighorn."); } }

    }

    [RequiresSkill(typeof(HuntingSkill), 2)]
    public class SkinBighornRecipe : Recipe
    {
        public SkinBighornRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedBighornItem>(),
               new CraftingElement<LeatherHideItem>(typeof(HuntingSkill), 2f, HuntingSkill.MultiplicativeStrategy.Inverted()),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BighornCarcassItem>(),
            };
            Initialize(Localizer.DoStr("Skin Bighorn"), typeof(SkinBighornRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(SkinBighornRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }

}