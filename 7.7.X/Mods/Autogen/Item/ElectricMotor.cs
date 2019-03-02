namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;

    [RequiresSkill(typeof(ElectronicsSkill), 3)]   
    public partial class ElectricMotorRecipe : Recipe
    {
        public ElectricMotorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ElectricMotorItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CopperWiringItem>(typeof(ElectronicsSkill), 20, ElectronicsSkill.MultiplicativeStrategy),
                new CraftingElement<CircuitItem>(typeof(ElectronicsSkill), 6, ElectronicsSkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(ElectronicsSkill), 6, ElectronicsSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ElectricMotorRecipe), Item.Get<ElectricMotorItem>().UILink(), 5, typeof(ElectronicsSkill));    
            this.Initialize(Localizer.DoStr("Electric Motor"), typeof(ElectricMotorRecipe));

            CraftingComponent.AddRecipe(typeof(ElectronicsAssemblyObject), this);
        }
    }


    [Serialized]
    [Weight(1000)]      
    [Currency]              
    public partial class ElectricMotorItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Electric Motor"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A motor."); } }

    }

}