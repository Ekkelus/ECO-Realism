namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Serialization;

    [Serialized]
    [Weight(10000)]
    public partial class TreasuryItem : WorldObjectItem<TreasuryObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Treasury"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Allows the setting of taxes."); } }
    }


    [RequiresSkill(typeof(SmeltingSkill), 2)]
    public partial class TreasuryRecipe : Recipe
    {
        public TreasuryRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<TreasuryItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<HingeItem>(typeof(SmeltingSkill), 10, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(SmeltingSkill), 30, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<GoldIngotItem>(typeof(SmeltingSkill), 10, SmeltingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(TreasuryRecipe), Item.Get<TreasuryItem>().UILink(), 60, typeof(SmeltingSkill), typeof(SmeltingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Treasury"), typeof(TreasuryRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}