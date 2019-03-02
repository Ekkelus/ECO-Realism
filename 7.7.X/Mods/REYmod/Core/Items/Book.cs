namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;

    [RequiresSkill(typeof(PaperMillingSkill), 1)]
    public partial class BookRecipe : Recipe
    {
        public BookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BookItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PaperItem>(typeof(PaperMillingSkill), 20, PaperMillingSkill.MultiplicativeStrategy),
                new CraftingElement<LeatherHideItem>(typeof(PaperMillingSkill), 10, PaperMillingSkill.MultiplicativeStrategy),
                new CraftingElement<GlueItem>(typeof(PaperMillingSkill), 4, PaperMillingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BookRecipe), Item.Get<BookItem>().UILink(), 10, typeof(PaperMillingSkill));
            this.Initialize(Localizer.DoStr("Book"), typeof(BookRecipe));

            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }


    [Serialized]
    [Weight(500)]
    [Currency]
    public partial class BookItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Book"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A bundle of paper with a protective cover, to contain written information"); } }

    }

}