namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Serialization;

    [RequiresSkill(typeof(SmeltingSkill), 1)]   
    public partial class NailsRecipe : Recipe
    {
        public NailsRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<NailsItem>(5),          
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(SmeltingSkill), 2, SmeltingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(NailsRecipe), Item.Get<NailsItem>().UILink(), 4, typeof(SmeltingSkill));    
            Initialize(Localizer.DoStr("Nails"), typeof(NailsRecipe));

            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }


    [Serialized]
    [Weight(20)]      
    [Currency]              
    public partial class NailsItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Nails"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Hold wooden constructions together."); } }

    }

}