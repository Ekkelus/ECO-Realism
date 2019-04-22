namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Serialization;

    [RequiresSkill(typeof(ElectronicsSkill), 3)]   
    public partial class ElectricMotorRecipe : Recipe
    {
        public ElectricMotorRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<ElectricMotorItem>(),          
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<CopperWiringItem>(typeof(ElectronicsSkill), 20, ElectronicsSkill.MultiplicativeStrategy),
                new CraftingElement<CircuitItem>(typeof(ElectronicsSkill), 6, ElectronicsSkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(ElectronicsSkill), 6, ElectronicsSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(ElectricMotorRecipe), Item.Get<ElectricMotorItem>().UILink(), 5, typeof(ElectronicsSkill), typeof(ElectronicsFocusedSpeedTalent));    
            Initialize(Localizer.DoStr("Electric Motor"), typeof(ElectricMotorRecipe));

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