namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.DynamicValues;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Skills;
    using Shared.Localization;
    using Shared.Serialization;
    using World.Blocks;

    [Serialized]
    [Weight(15000)]  
    public class PoweredCartItem : WorldObjectItem<PoweredCartObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Powered Cart"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Large cart for hauling sizable loads."); } }
    }

    [RequiresSkill(typeof(MechanicsSkill), 0)] 
    public class PoweredCartRecipe : Recipe
    {
        public PoweredCartRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<PoweredCartItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<CombustionEngineItem>(),
                new CraftingElement<LumberItem>(typeof(MechanicsSkill), 20, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<ClothItem>(typeof(MechanicsSkill), 20, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<RivetItem>(typeof(MechanicsSkill), 5, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<RubberWheelItem>(typeof(MechanicsSkill), 3, MechanicsSkill.MultiplicativeStrategy),
            };
            CraftMinutes = new ConstantValue(25);

            Initialize(Localizer.DoStr("Powered Cart"), typeof(PoweredCartRecipe));
            CraftingComponent.AddRecipe(typeof(WainwrightTableObject), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]              
    [RequireComponent(typeof(FuelConsumptionComponent))]         
    [RequireComponent(typeof(PublicStorageComponent))]      
    [RequireComponent(typeof(MovableLinkComponent))]        
    [RequireComponent(typeof(AirPollutionComponent))]       
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]     
    public partial class PoweredCartObject : PhysicsWorldObject, IRepresentsItem
    {
        static PoweredCartObject()
        {
            AddOccupancy<PoweredCartObject>(new List<BlockOccupancy>(0));
        }


        public override LocString DisplayName { get { return Localizer.DoStr("Powered Cart"); } }
        public Type RepresentedItemType { get { return typeof(PoweredCartItem); } }

        private static Type[] fuelTypeList = new[]
        {
            typeof(PetroleumItem),
typeof(GasolineItem),
        };

        private PoweredCartObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            GetComponent<PublicStorageComponent>().Initialize(20, 3000000);            
            GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);           
            GetComponent<FuelConsumptionComponent>().Initialize(40);    
            GetComponent<AirPollutionComponent>().Initialize(0.2f);            
            GetComponent<VehicleComponent>().Initialize(15, 1, 1);
        }
    }
}