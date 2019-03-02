namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;


    public partial class StringRecipe : Recipe
    {
        public StringRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<StringItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PlantFibersItem>(typeof(TailoringSkill), 10, TailoringSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(StringRecipe), Item.Get<StringItem>().UILink(), 0.5f, typeof(TailoringSkill));
            this.Initialize(Localizer.DoStr("String"), typeof(StringRecipe));

            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }


    [Serialized]
    [Weight(200)]
    [Currency]
    public partial class StringItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("String"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Several fibers twisted together to form a long thread."); } }

    }

}