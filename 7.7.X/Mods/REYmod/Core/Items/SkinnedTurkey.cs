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
    [Weight(800)]
    [Currency]
    public partial class SkinnedTurkeyItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Skinned Turkey"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A skinned turkey."); } }

    }

    [RequiresSkill(typeof(HuntingSkill), 1)]
    public class SkinTurkeyRecipe : Recipe
    {
        public SkinTurkeyRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedTurkeyItem>(),
               new CraftingElement<LeatherHideItem>(typeof(HuntingSkill), 1, HuntingSkill.MultiplicativeStrategy.Inverted()),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<TurkeyCarcassItem>(),
            };
            Initialize(Localizer.DoStr("Skin Turkey"), typeof(SkinTurkeyRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(SkinTurkeyRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }

}