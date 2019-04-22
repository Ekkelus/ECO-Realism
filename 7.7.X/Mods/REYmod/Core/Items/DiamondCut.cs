namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Serialization;
    using REYmod.Blocks;

    [RequiresSkill(typeof(MortaringSkill), 5)]
    public partial class DiamondCutRecipe : Recipe
    {
        public DiamondCutRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<DiamondCutItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<RawDiamondItem>(typeof(MortaringSkill), 10, MortaringSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(DiamondCutRecipe), Item.Get<DiamondCutItem>().UILink(), 60, typeof(MortaringSkill), typeof(MortaringFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Cut Diamond"), typeof(DiamondCutRecipe));

            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }


    [Serialized]
    [Weight(1000)]
    [Currency]
    public partial class DiamondCutItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Diamond Cut"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Probably useful."); } }

    }

}