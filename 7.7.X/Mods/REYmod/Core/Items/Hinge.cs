namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;

    [RequiresSkill(typeof(SmeltingSkill), 1)]
    public partial class HingeRecipe : Recipe
    {
        public HingeRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<HingeItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(SmeltingSkill), 2, SmeltingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(HingeRecipe), Item.Get<HingeItem>().UILink(), 7, typeof(SmeltingSkill));
            this.Initialize(Localizer.DoStr("Hinge"), typeof(HingeRecipe));

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