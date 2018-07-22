namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Components.VehicleModules;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Math;
    using Eco.Shared.Networking;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World.Blocks;

    [Serialized]
    [Weight(25000)]  
    public class SteamTruckItem : WorldObjectItem<SteamTruckObject>
    {
        public override string FriendlyName         { get { return "Steam Truck"; } }
        public override string Description          { get { return "Stream truck"; } }
    }

    [RequiresSkill(typeof(MechanicalEngineeringSkill), 0)] 
    public class SteamTruckRecipe : Recipe
    {
        public SteamTruckRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SteamTruckItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PortableSteamEngineItem>(1),
                new CraftingElement<IronWheelItem>(4),
                new CraftingElement<IronAxleItem>(1), 
                new CraftingElement<IronPlateItem>(typeof(MechanicsAssemblyEfficiencySkill), 30, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<IronPipeItem>(typeof(MechanicsAssemblyEfficiencySkill), 20, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<ScrewsItem>(typeof(MechanicsAssemblyEfficiencySkill), 40, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<LumberItem>(typeof(MechanicsAssemblyEfficiencySkill), 30, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<LeatherHideItem>(typeof(MechanicsAssemblyEfficiencySkill), 20, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(25);

            this.Initialize("Steam Truck", typeof(SteamTruckRecipe));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]              
    [RequireComponent(typeof(FuelConsumptionComponent))]         
    [RequireComponent(typeof(PublicStorageComponent))]      
    [RequireComponent(typeof(MovableLinkComponent))]        
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(ModularStockpileComponent))]   
    [RequireComponent(typeof(TailingsReportComponent))]     
    public partial class SteamTruckObject : PhysicsWorldObject
    {
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
        public override string FriendlyName { get { return "Steam Truck"; } }

        private static Type[] fuelTypeList = new Type[]
        {
            typeof(LogItem),
typeof(LumberItem),
typeof(CharcoalItem),
typeof(ArrowItem),
typeof(BoardItem),
typeof(CoalItem),
        };

        private SteamTruckObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(24, 5000000);           
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);           
            this.GetComponent<FuelConsumptionComponent>().Initialize(25);    
            this.GetComponent<VehicleComponent>().Initialize(18, 1, roadEfficiency, 2);
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(2,2,3));  
        }
    }
}