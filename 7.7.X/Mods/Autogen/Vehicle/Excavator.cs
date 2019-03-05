namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.DynamicValues;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]
    [Weight(30000)]  
    public class ExcavatorItem : WorldObjectItem<ExcavatorObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Excavator"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("I EAT DIRT!"); } }
    }

    [RequiresSkill(typeof(IndustrySkill), 1)] 
    public class ExcavatorRecipe : Recipe
    {
        public ExcavatorRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<ExcavatorItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<AdvancedCombustionEngineItem>(),
                new CraftingElement<RubberWheelItem>(4),
                new CraftingElement<RadiatorItem>(2),
                new CraftingElement<SteelAxleItem>(), 
                new CraftingElement<GearboxItem>(typeof(IndustrySkill), 10, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<CelluloseFiberItem>(typeof(IndustrySkill), 20, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(IndustrySkill), 40, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<RivetItem>(typeof(IndustrySkill), 20, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<GlassItem>(typeof(IndustrySkill), 10, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<SteelPlateItem>(typeof(IndustrySkill), 40, IndustrySkill.MultiplicativeStrategy),
            };
            CraftMinutes = new ConstantValue(50);

            Initialize(Localizer.DoStr("Excavator"), typeof(ExcavatorRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }

}