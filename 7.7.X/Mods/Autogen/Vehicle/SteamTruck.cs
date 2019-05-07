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
    public class SteamTruckItem : WorldObjectItem<SteamTruckObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Steam Truck"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Stream truck"); } }
    }

    [RequiresSkill(typeof(MechanicsSkill), 0)] 
    public class SteamTruckRecipe : Recipe
    {
        public SteamTruckRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SteamTruckItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<PortableSteamEngineItem>(),
                new CraftingElement<IronWheelItem>(4),
                new CraftingElement<IronAxleItem>(), 
                new CraftingElement<IronPlateItem>(typeof(MechanicsSkill), 30, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<IronPipeItem>(typeof(MechanicsSkill), 20, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<ScrewsItem>(typeof(MechanicsSkill), 40, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<LumberItem>(typeof(MechanicsSkill), 30, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<LeatherHideItem>(typeof(MechanicsSkill), 20, MechanicsSkill.MultiplicativeStrategy),
            };
            CraftMinutes = new ConstantValue(25);

            Initialize(Localizer.DoStr("Steam Truck"), typeof(SteamTruckRecipe));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
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
    public partial class SteamTruckObject : PhysicsWorldObject, IRepresentsItem
    {
        static SteamTruckObject()
        {
            AddOccupancy<SteamTruckObject>(new List<BlockOccupancy>(0));
        }


        public override LocString DisplayName { get { return Localizer.DoStr("Steam Truck"); } }
        public Type RepresentedItemType { get { return typeof(SteamTruckItem); } }

        private static Type[] fuelTypeList = new[]
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
            
            GetComponent<PublicStorageComponent>().Initialize(24, 5000000);           
            GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);           
            GetComponent<FuelConsumptionComponent>().Initialize(25);    
            GetComponent<AirPollutionComponent>().Initialize(0.03f);            
            GetComponent<VehicleComponent>().Initialize(18, 2, 2);
            GetComponent<StockpileComponent>().Initialize(new Vector3i(2,2,3));  
        }
    }
}