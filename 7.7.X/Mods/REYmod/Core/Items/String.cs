namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Systems.TextLinks;
    using Shared.Serialization;


    public partial class StringRecipe : Recipe
    {
        public StringRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<StringItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<PlantFibersItem>(typeof(TailoringSkill), 10, TailoringSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(StringRecipe), Item.Get<StringItem>().UILink(), 0.5f, typeof(TailoringSkill), typeof(TailoringFocusedSpeedTalent));
            Initialize(Localizer.DoStr("String"), typeof(StringRecipe));

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