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
    public class CraneItem : WorldObjectItem<CraneObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Crane"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Allows the placement and transport of materials in an area."); } }
    }

    [RequiresSkill(typeof(IndustrySkill), 1)] 
    public class CraneRecipe : Recipe
    {
        public CraneRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CraneItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<AdvancedCombustionEngineItem>(1),
                new CraftingElement<RubberWheelItem>(4),
                new CraftingElement<RadiatorItem>(2),
                new CraftingElement<SteelAxleItem>(1), 
                new CraftingElement<GearboxItem>(typeof(IndustrySkill), 10, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<CelluloseFiberItem>(typeof(IndustrySkill), 20, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<SteelPlateItem>(typeof(IndustrySkill), 40, IndustrySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(25);

            this.Initialize(Localizer.DoStr("Crane"), typeof(CraneRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }

}