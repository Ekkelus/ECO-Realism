namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;


    [Serialized]
    [Weight(500)]
    public partial class StockpileItem :
        WorldObjectItem<StockpileObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Stockpile"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Designates a 5x5x5 area as storage for large items."); } }

        static StockpileItem()
        {
            
        }

    }


    public partial class StockpileRecipe : Recipe
    {
        public StockpileRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<StockpileItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(10),                                                                    
            };
            this.CraftMinutes = new ConstantValue(2); 
            this.Initialize(Localizer.Do("Stockpile"), typeof(StockpileRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}