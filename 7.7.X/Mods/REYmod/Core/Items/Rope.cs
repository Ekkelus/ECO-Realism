namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;

    public partial class RopeRecipe : Recipe
    {
        public RopeRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RopeItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StringItem>(typeof(TailoringSkill), 5, TailoringSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(RopeRecipe), Item.Get<RopeItem>().UILink(), 2, typeof(TailoringSkill));
            this.Initialize(Localizer.DoStr("Rope"), typeof(RopeRecipe));

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