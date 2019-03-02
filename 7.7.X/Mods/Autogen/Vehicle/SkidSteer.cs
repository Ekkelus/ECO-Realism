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

    [RequiresSkill(typeof(IndustrySkill), 1)] 
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
                new CraftingElement<GearboxItem>(typeof(IndustrySkill), 10, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<CelluloseFiberItem>(typeof(IndustrySkill), 20, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(IndustrySkill), 40, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<RivetItem>(typeof(IndustrySkill), 16, IndustrySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(25);

            this.Initialize(Localizer.DoStr("Skid Steer"), typeof(SkidSteerRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }

}