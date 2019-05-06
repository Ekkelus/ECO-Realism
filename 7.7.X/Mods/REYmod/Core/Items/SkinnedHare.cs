namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Items;
    using Shared.Serialization;
    using REYmod.Utils;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Components;


    [Serialized]
    [Weight(300)]
    [Currency]
    public partial class SkinnedHareItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Skinned Hare"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A skinned hare."); } }

    }

    [RequiresSkill(typeof(HuntingSkill), 1)]
    public class SkinHareRecipe : Recipe
    {
        public SkinHareRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedHareItem>(),
               new CraftingElement<FurPeltItem>(typeof(HuntingSkill), 1, HuntingSkill.MultiplicativeStrategy.Inverted()),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<HareCarcassItem>(),
            };
            Initialize(Localizer.DoStr("Skin Hare"), typeof(SkinHareRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(SkinHareRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }

}