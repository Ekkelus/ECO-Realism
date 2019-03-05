namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.DynamicValues;
    using Gameplay.Items;
    using Shared.Localization;
    using Shared.Serialization;

    public partial class TorchRecipe : Recipe
    {
        public TorchRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<TorchItem>(),          
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(2),    
            };
            CraftMinutes = new ConstantValue(0.5f);
            Initialize(Localizer.DoStr("Torch"), typeof(TorchRecipe));

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