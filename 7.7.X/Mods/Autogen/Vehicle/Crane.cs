namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.DynamicValues;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Shared.Serialization;

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
            Products = new CraftingElement[]
            {
                new CraftingElement<CraneItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<AdvancedCombustionEngineItem>(),
                new CraftingElement<RubberWheelItem>(4),
                new CraftingElement<RadiatorItem>(2),
                new CraftingElement<SteelAxleItem>(), 
                new CraftingElement<GearboxItem>(typeof(IndustrySkill), 10, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<CelluloseFiberItem>(typeof(IndustrySkill), 20, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<SteelPlateItem>(typeof(IndustrySkill), 40, IndustrySkill.MultiplicativeStrategy),
            };
            CraftMinutes = new ConstantValue(25);

            Initialize(Localizer.DoStr("Crane"), typeof(CraneRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }

}