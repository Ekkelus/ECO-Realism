namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    public partial class TorchRecipe : Recipe
    {
        public TorchRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<TorchItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(2),    
            };
            this.CraftMinutes = new ConstantValue(0.5f);
            this.Initialize(Localizer.DoStr("Torch"), typeof(TorchRecipe));

            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }


    [Serialized]
    [Weight(500)]      
    [Fuel(1000)]          
    [Currency]              
    public partial class TorchItem :
    ToolItem                        
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Torch"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A little bit of light to help beat back the night."); } }

    }

}