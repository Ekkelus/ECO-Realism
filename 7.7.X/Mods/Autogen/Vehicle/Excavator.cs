namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

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
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ExcavatorItem>(),
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
                new CraftingElement<RivetItem>(typeof(IndustrySkill), 20, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<GlassItem>(typeof(IndustrySkill), 10, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<SteelPlateItem>(typeof(IndustrySkill), 40, IndustrySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(50);

            this.Initialize(Localizer.DoStr("Excavator"), typeof(ExcavatorRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }

}