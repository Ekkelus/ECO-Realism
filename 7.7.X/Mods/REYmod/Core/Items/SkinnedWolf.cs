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
    public partial class SkinnedWolfItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Skinned Wolf"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A skinned wolf."); } }

    }

    [RequiresSkill(typeof(HuntingSkill), 2)]
    public class SkinWolfRecipe : Recipe
    {
        public SkinWolfRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedWolfItem>(),
               new CraftingElement<FurPeltItem>(typeof(HuntingSkill), 3, HuntingSkill.MultiplicativeStrategy.Inverted()),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<WolfCarcassItem>(),
            };
            Initialize(Localizer.DoStr("Skin Wolf"), typeof(SkinWolfRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(SkinWolfRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }

}