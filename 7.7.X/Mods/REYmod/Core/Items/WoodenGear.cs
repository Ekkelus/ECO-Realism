namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;

    [RequiresSkill(typeof(BasicEngineeringSkill), 1)]
    public partial class WoodenGearRecipe : Recipe
    {
        public WoodenGearRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WoodenGearItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(BasicEngineeringSkill), 10, BasicEngineeringSkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(BasicEngineeringSkill), 10, BasicEngineeringSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(WoodenGearRecipe), Item.Get<WoodenGearItem>().UILink(), 6, typeof(BasicEngineeringSkill));
            this.Initialize(Localizer.DoStr("Wooden Gear"), typeof(WoodenGearRecipe));

            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }


    [Serialized]
    [Weight(2000)]
    [Currency]
    public partial class WoodenGearItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wooden Gear"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A toothed machine part that interlocks with others."); } }

    }

}