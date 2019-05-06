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
    public partial class SkinnedDeerItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Skinned Deer"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A skinned Deer."); } }

    }

    [RequiresSkill(typeof(HuntingSkill), 2)]
    public class SkinDeerRecipe : Recipe
    {
        public SkinDeerRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedDeerItem>(),
               new CraftingElement<LeatherHideItem>(typeof(HuntingSkill), 2f, HuntingSkill.MultiplicativeStrategy.Inverted()),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<DeerCarcassItem>(),
            };
            Initialize(Localizer.DoStr("Skin Deer"), typeof(SkinDeerRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(SkinDeerRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }

}