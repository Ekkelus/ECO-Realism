namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Serialization;

    [RequiresSkill(typeof(ButcherySkill), 1)]
    public partial class GlueRecipe : Recipe
    {
        public GlueRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<GlueItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoneItem>(typeof(ButcherySkill), 5, ButcherySkill.MultiplicativeStrategy),
                new CraftingElement<GlassJarItem>(),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(GlueRecipe), Item.Get<GlueItem>().UILink(), 3, typeof(ButcherySkill));
            Initialize(Localizer.DoStr("Glue"), typeof(GlueRecipe));

            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }


    [Serialized]
    [Weight(200)]
    [Currency]
    public partial class GlueItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Glue"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Sticks two components together."); } }

    }

}