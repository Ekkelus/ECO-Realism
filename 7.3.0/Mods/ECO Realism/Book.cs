namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Shared.Localization;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;

    [RequiresSkill(typeof(PaperSkill), 1)]
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
                new CraftingElement<PaperItem>(typeof(PaperEfficiencySkill), 20, PaperEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<LeatherHideItem>(typeof(PaperEfficiencySkill), 10, PaperEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GlueItem>(typeof(PaperEfficiencySkill), 4, PaperEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BookRecipe), Item.Get<BookItem>().UILink(), 10, typeof(PaperSpeedSkill));
            this.Initialize("Book", typeof(BookRecipe));

            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }


    [Serialized]
    [Weight(2000)]
    [Currency]
    public partial class BookItem :
    Item
    {
        public override string FriendlyName { get { return "Book"; } }
        public override string Description { get { return "A bundle of paper with a protective cover, to contain written information"; } }

    }

}