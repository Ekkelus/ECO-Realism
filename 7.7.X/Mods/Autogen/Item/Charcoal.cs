namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;

    [RequiresSkill(typeof(LumberSkill), 1)]   
    public partial class CharcoalRecipe : Recipe
    {
        public CharcoalRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CharcoalItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LumberItem>(typeof(LumberProcessingEfficiencySkill), 2, LumberProcessingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CharcoalRecipe), Item.Get<CharcoalItem>().UILink(), 1, typeof(LumberProcessingSpeedSkill));    
            this.Initialize(Localizer.Do("Charcoal"), typeof(CharcoalRecipe));

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