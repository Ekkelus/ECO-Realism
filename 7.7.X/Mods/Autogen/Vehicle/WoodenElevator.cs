namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [Weight(10000)]  
    public class WoodenElevatorItem : WorldObjectItem<WoodenElevatorObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wooden Elevator"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("An elevator for transporting loads vertically."); } }
    }

    [RequiresSkill(typeof(MechanicsSkill), 0)] 
    public class WoodenElevatorRecipe : Recipe
    {
        public WoodenElevatorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WoodenElevatorItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PortableSteamEngineItem>(1), 
                new CraftingElement<GearboxItem>(typeof(MechanicsSkill), 10, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<CelluloseFiberItem>(typeof(MechanicsSkill), 20, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<LumberItem>(typeof(MechanicsSkill), 30, MechanicsSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(25);

            this.Initialize(Localizer.DoStr("Wooden Elevator"), typeof(WoodenElevatorRecipe));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }

}