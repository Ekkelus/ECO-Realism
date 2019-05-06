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
    public partial class SkinnedMountainGoatItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Skinned Mountain Goat"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A skinned mountain goat."); } }

    }

    [RequiresSkill(typeof(HuntingSkill), 2)]
    public class SkinMountainGoatRecipe : Recipe
    {
        public SkinMountainGoatRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedMountainGoatItem>(),
               new CraftingElement<LeatherHideItem>(typeof(HuntingSkill), 2, HuntingSkill.MultiplicativeStrategy.Inverted()),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<MountainGoatCarcassItem>(),
            };
            Initialize(Localizer.DoStr("Skin Mountain Goat"), typeof(SkinMountainGoatRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(SkinMountainGoatRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }

}