namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.World.Blocks;

    [Serialized]
    [Weight(25000)]  
    public class TruckItem : WorldObjectItem<TruckObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Truck"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Modern truck for hauling sizable loads."); } }
    }

    [RequiresSkill(typeof(IndustrialEngineeringSkill), 0)] 
    public class TruckRecipe : Recipe
    {
        public TruckRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<TruckItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteamTruckItem>(1),
                new CraftingElement<CombustionEngineItem>(1),
                new CraftingElement<RadiatorItem>(1), 
                new CraftingElement<GearboxItem>(typeof(IndustrialEngineeringEfficiencySkill), 10, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CelluloseFiberItem>(typeof(IndustrialEngineeringEfficiencySkill), 20, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(IndustrialEngineeringEfficiencySkill), 40, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GlassItem>(typeof(IndustrialEngineeringEfficiencySkill), 20, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<RubberWheelItem>(4),
                new CraftingElement<RivetItem>(typeof(IndustrialEngineeringEfficiencySkill), 18, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(25);

            this.Initialize(Localizer.Do("Truck"), typeof(TruckRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
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
    [RequireComponent(typeof(ModularStockpileComponent))]   
    [RequireComponent(typeof(TailingsReportComponent))]     
    public partial class TruckObject : PhysicsWorldObject, IRepresentsItem
    {
        static TruckObject()
        {
            WorldObject.AddOccupancy<TruckObject>(new List<BlockOccupancy>(0));
        }

        private static Dictionary<Type, float> roadEfficiency = new Dictionary<Type, float>()
        {
            { typeof(GrassBlock) , 0.7f}, { typeof(SandBlock) , 0.3f},
            { typeof(DesertSandBlock) , 0.3f}, { typeof(WetlandsBlock) , 0.5f},
            { typeof(SnowBlock) , 0.5f}, { typeof(DirtBlock) , 0.8f},
            { typeof(ForestSoilBlock) , 0.6f},
            { typeof(DirtRoadBlock), 1.0f }, { typeof(DirtRoadWorldObjectBlock), 1.0f },
            { typeof(StoneRoadBlock), 1.4f }, { typeof(StoneRoadWorldObjectBlock), 1.4f },
            { typeof(AsphaltRoadBlock), 1.8f }, { typeof(AsphaltRoadWorldObjectBlock), 1.8f }
        };
        public override LocString DisplayName { get { return Localizer.DoStr("Truck"); } }
        public Type RepresentedItemType { get { return typeof(TruckItem); } }

        private static Type[] fuelTypeList = new Type[]
        {
            typeof(PetroleumItem),
typeof(GasolineItem),
        };

        private TruckObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(36, 8000000);           
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);           
            this.GetComponent<FuelConsumptionComponent>().Initialize(25);    
            this.GetComponent<VehicleComponent>().Initialize(25, 1, roadEfficiency, 2);
            this.GetComponent<AirPollutionComponent>().Initialize(0.5f);            
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(2,2,3));  
        }
    }
}