namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.DynamicValues;
    using Gameplay.Items;
    using Shared.Localization;
    using Shared.Serialization;


    [Serialized]
    [Weight(500)]
    public partial class StockpileItem :
        WorldObjectItem<StockpileObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Stockpile"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Designates a 5x5x5 area as storage for large items."); } }
    }


    public partial class StockpileRecipe : Recipe
    {
        public StockpileRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<StockpileItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(10),                                                                    
            };
            CraftMinutes = new ConstantValue(2); 
            Initialize(Localizer.DoStr("Stockpile"), typeof(StockpileRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}