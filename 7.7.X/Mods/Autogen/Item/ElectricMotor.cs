namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;

    [RequiresSkill(typeof(ElectronicEngineeringSkill), 3)]   
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
                new CraftingElement<CopperWiringItem>(typeof(ElectronicEngineeringEfficiencySkill), 20, ElectronicEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CircuitItem>(typeof(ElectronicEngineeringEfficiencySkill), 6, ElectronicEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(ElectronicEngineeringEfficiencySkill), 6, ElectronicEngineeringEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ElectricMotorRecipe), Item.Get<ElectricMotorItem>().UILink(), 5, typeof(ElectronicEngineeringSpeedSkill));    
            this.Initialize("Electric Motor", typeof(ElectricMotorRecipe));

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