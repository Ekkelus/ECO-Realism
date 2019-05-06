namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Items;
    using Shared.Serialization;
    using REYmod.Utils;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Components;


    [Serialized]
    [Weight(800)]
    [Currency]
    public partial class SkinnedFoxItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Skinned Fox"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A skinned fox."); } }

    }

    [RequiresSkill(typeof(HuntingSkill), 2)]
    public class SkinFoxRecipe : Recipe
    {
        public SkinFoxRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedFoxItem>(),
               new CraftingElement<FurPeltItem>(typeof(HuntingSkill), 2, HuntingSkill.MultiplicativeStrategy.Inverted()),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<FoxCarcassItem>(),
            };
            Initialize(Localizer.DoStr("Skin Fox"), typeof(SkinFoxRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(SkinFoxRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }

}