namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.DynamicValues;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Shared.Serialization;

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
            Products = new CraftingElement[]
            {
                new CraftingElement<WoodenElevatorItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<PortableSteamEngineItem>(), 
                new CraftingElement<GearboxItem>(typeof(MechanicsSkill), 10, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<CelluloseFiberItem>(typeof(MechanicsSkill), 20, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<LumberItem>(typeof(MechanicsSkill), 30, MechanicsSkill.MultiplicativeStrategy),
            };
            CraftMinutes = new ConstantValue(25);

            Initialize(Localizer.DoStr("Wooden Elevator"), typeof(WoodenElevatorRecipe));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }

}