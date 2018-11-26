namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [Weight(25000)]  
    public class SkidSteerItem : WorldObjectItem<SkidSteerObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Skid Steer"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A WHAT?"); } }
    }

    [RequiresSkill(typeof(IndustrialEngineeringSkill), 1)] 
    public class SkidSteerRecipe : Recipe
    {
        public SkidSteerRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SkidSteerItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<AdvancedCombustionEngineItem>(1),
                new CraftingElement<RubberWheelItem>(4),
                new CraftingElement<RadiatorItem>(2),
                new CraftingElement<SteelAxleItem>(1), 
                new CraftingElement<GearboxItem>(typeof(IndustrialEngineeringEfficiencySkill), 10, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CelluloseFiberItem>(typeof(IndustrialEngineeringEfficiencySkill), 20, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(IndustrialEngineeringEfficiencySkill), 40, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<RivetItem>(typeof(IndustrialEngineeringEfficiencySkill), 16, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(25);

            this.Initialize(Localizer.Do("Skid Steer"), typeof(SkidSteerRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }

}