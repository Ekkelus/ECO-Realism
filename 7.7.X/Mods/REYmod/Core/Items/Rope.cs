namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Systems.TextLinks;
    using Shared.Serialization;

    public partial class RopeRecipe : Recipe
    {
        public RopeRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<RopeItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<StringItem>(typeof(TailoringSkill), 5, TailoringSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(RopeRecipe), Item.Get<RopeItem>().UILink(), 2, typeof(TailoringSkill), typeof(TailoringFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Rope"), typeof(RopeRecipe));

            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }


    [Serialized]
    [Weight(1000)]
    [Currency]
    public partial class RopeItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Rope"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Several strings twisted together to form a long rope."); } }

    }

}