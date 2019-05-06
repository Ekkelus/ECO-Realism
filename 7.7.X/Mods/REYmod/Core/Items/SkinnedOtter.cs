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
    [Weight(500)]
    [Currency]
    public partial class SkinnedOtterItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Skinned Otter"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A skinned Otter."); } }

    }

    [RequiresSkill(typeof(HuntingSkill), 1)]
    public class SkinOtterRecipe : Recipe
    {
        public SkinOtterRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedOtterItem>(),
               new CraftingElement<FurPeltItem>(typeof(HuntingSkill), 1, HuntingSkill.MultiplicativeStrategy.Inverted()),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<OtterCarcassItem>(),
            };
            Initialize(Localizer.DoStr("Skin Otter"), typeof(SkinOtterRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(SkinOtterRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }

}