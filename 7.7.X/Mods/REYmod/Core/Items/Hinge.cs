namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Serialization;

    [RequiresSkill(typeof(SmeltingSkill), 1)]
    public partial class HingeRecipe : Recipe
    {
        public HingeRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<HingeItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(SmeltingSkill), 2, SmeltingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(HingeRecipe), Item.Get<HingeItem>().UILink(), 7, typeof(SmeltingSkill), typeof(SmeltingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Hinge"), typeof(HingeRecipe));

            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }


    [Serialized]
    [Weight(150)]
    [Currency]
    public partial class HingeItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Hinge"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Allows one component to pivot off the other."); } }

    }

}