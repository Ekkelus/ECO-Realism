namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;
    using REYmod.Blocks;

    [RequiresSkill(typeof(StoneworkingSkill), 5)]
    public partial class DiamondCutRecipe : Recipe
    {
        public DiamondCutRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<DiamondCutItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<RawDiamondItem>(typeof(StoneworkingEfficiencySkill), 10, StoneworkingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(DiamondCutRecipe), Item.Get<DiamondCutItem>().UILink(), 60, typeof(StoneworkingSpeedSkill));
            this.Initialize(Localizer.DoStr("Cut Diamond"), typeof(DiamondCutRecipe));

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