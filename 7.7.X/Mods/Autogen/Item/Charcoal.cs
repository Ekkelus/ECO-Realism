namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Serialization;

    [RequiresSkill(typeof(LumberSkill), 1)]   
    public partial class CharcoalRecipe : Recipe
    {
        public CharcoalRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<CharcoalItem>(),          
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LumberItem>(typeof(LumberSkill), 2, LumberSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(CharcoalRecipe), Item.Get<CharcoalItem>().UILink(), 1, typeof(LumberSkill));    
            Initialize(Localizer.DoStr("Charcoal"), typeof(CharcoalRecipe));

            CraftingComponent.AddRecipe(typeof(BloomeryObject), this);
        }
    }


    [Serialized]
    [Weight(1000)]      
    [Fuel(25000)]          
    [Currency]              
    public partial class CharcoalItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Charcoal"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A black residue, consisting of carbon and any remaining ash."); } }

    }

}