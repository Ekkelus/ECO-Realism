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
    using Shared.Math;
    using Shared.Localization;
    using Shared.Serialization;
    using World.Blocks;

    [Serialized]
    [Weight(25000)]  
    public class TruckItem : WorldObjectItem<TruckObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Truck"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Modern truck for hauling sizable loads."); } }
    }

    [RequiresSkill(typeof(IndustrySkill), 0)] 
    public class TruckRecipe : Recipe
    {
        public TruckRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<TruckItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteamTruckItem>(),
                new CraftingElement<CombustionEngineItem>(),
                new CraftingElement<RadiatorItem>(), 
                new CraftingElement<GearboxItem>(typeof(IndustrySkill), 10, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<CelluloseFiberItem>(typeof(IndustrySkill), 20, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(IndustrySkill), 40, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<GlassItem>(typeof(IndustrySkill), 20, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<RubberWheelItem>(4),
                new CraftingElement<RivetItem>(typeof(IndustrySkill), 18, IndustrySkill.MultiplicativeStrategy),
            };
            CraftMinutes = new ConstantValue(25);

            Initialize(Localizer.DoStr("Truck"), typeof(TruckRecipe));
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
            AddOccupancy<TruckObject>(new List<BlockOccupancy>(0));
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

        private static Type[] fuelTypeList = new[]
        {
            typeof(PetroleumItem),
typeof(GasolineItem),
        };

        private TruckObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            GetComponent<PublicStorageComponent>().Initialize(36, 8000000);           
            GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);           
            GetComponent<FuelConsumptionComponent>().Initialize(25);    
            GetComponent<VehicleComponent>().Initialize(25, 1, roadEfficiency, 2);
            GetComponent<AirPollutionComponent>().Initialize(0.5f);            
            GetComponent<StockpileComponent>().Initialize(new Vector3i(2,2,3));  
        }
    }
}