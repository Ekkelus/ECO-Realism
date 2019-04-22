namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Serialization;

    [RequiresSkill(typeof(GlassworkingSkill), 1)]
    public partial class GlassJarRecipe : Recipe
    {
        public GlassJarRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<GlassJarItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<GlassItem>(typeof(GlassworkingSkill), 2, GlassworkingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(GlassJarRecipe), Item.Get<GlassJarItem>().UILink(), 2, typeof(GlassworkingSkill), typeof(GlassworkingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Glass Jar"), typeof(GlassJarRecipe));

            CraftingComponent.AddRecipe(typeof(KilnObject), this);
        }
    }


    [Serialized]
    [Weight(100)]
    [Currency]
    public partial class GlassJarItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Glass Jar"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A glass container, capable of holding liquids."); } }

    }

}