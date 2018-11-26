namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [RequiresSkill(typeof(IndustrialEngineeringSkill), 2)]   
    public partial class RubberWheelRecipe : Recipe
    {
        public RubberWheelRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RubberWheelItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SyntheticRubberItem>(typeof(IndustrialEngineeringEfficiencySkill), 10, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<IronWheelItem>(1),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(RubberWheelRecipe), Item.Get<RubberWheelItem>().UILink(), 5, typeof(IndustrialEngineeringSpeedSkill));    
            this.Initialize(Localizer.DoStr("Rubber Wheel"), typeof(RubberWheelRecipe));

            CraftingComponent.AddRecipe(typeof(WainwrightTableObject), this);
        }
    }


    [Serialized]
    [Weight(2000)]      
    [Currency]              
    public partial class RubberWheelItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Rubber Wheel"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

    }

}