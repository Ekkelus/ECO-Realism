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
    public partial class SkinnedElkItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Skinned Elk"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A skinned elk."); } }

    }

    [RequiresSkill(typeof(HuntingSkill), 3)]
    public class SkinElkRecipe : Recipe
    {
        public SkinElkRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedElkItem>(),
               new CraftingElement<LeatherHideItem>(typeof(HuntingSkill), 2, HuntingSkill.MultiplicativeStrategy.Inverted()),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<ElkCarcassItem>(),
            };
            Initialize(Localizer.DoStr("Skin Elk"), typeof(SkinElkRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(SkinElkRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }

}