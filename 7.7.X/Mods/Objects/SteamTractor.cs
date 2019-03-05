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
    using Shared.Serialization;
	using Shared.Localization;
    using World.Blocks;

    [Serialized]
    [Weight(25000)]  
    public class SteamTractorItem : WorldObjectItem<SteamTractorObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Steam Tractor"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Steam Tractor"); } }
    }

    [RequiresSkill(typeof(MechanicsSkill), 0)] 
    public class SteamTractorRecipe : Recipe
    {
        public SteamTractorRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SteamTractorItem>(),
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

            Initialize(Localizer.DoStr("Steam Tractor"), typeof(SteamTractorRecipe));
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
    [RequireComponent(typeof(TailingsReportComponent))]     
    [RequireComponent(typeof(ModularVehicleComponent))]     
    public partial class SteamTractorObject : PhysicsWorldObject
    {
        private static Dictionary<Type, float> roadEfficiency = new Dictionary<Type, float>()
        {
            { typeof(GrassBlock) , 0.9f}, { typeof(SandBlock) , 0.5f},
            { typeof(DesertSandBlock) , 0.5f}, { typeof(WetlandsBlock) , 0.7f},
            { typeof(SnowBlock) , 0.7f}, 
            { typeof(ForestSoilBlock) , 0.8f},
            { typeof(DirtRoadBlock), 1.0f }, { typeof(DirtRoadWorldObjectBlock), 1.0f },
            { typeof(StoneRoadBlock), 1.2f }, { typeof(StoneRoadWorldObjectBlock), 1.2f },
            { typeof(AsphaltRoadBlock), 1.4f }, { typeof(AsphaltRoadWorldObjectBlock), 1.4f }
        };
        public override LocString DisplayName { get { return Localizer.DoStr("Steam Tractor"); } }

        private static Type[] fuelTypeList = new[]
        {
            typeof(LogItem),
typeof(LumberItem),
typeof(CharcoalItem),
typeof(ArrowItem),
typeof(BoardItem),
typeof(CoalItem),
        };

        private static Type[] segmentTypeList = new Type[] { };
        private static Type[] attachmentTypeList = new[]
        {
            typeof(SteamTractorPloughItem), typeof(SteamTractorHarvesterItem), typeof(SteamTractorSowerItem)
        };

        private SteamTractorObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            GetComponent<PublicStorageComponent>().Initialize(12, 2500000);           
            GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);           
            GetComponent<FuelConsumptionComponent>().Initialize(25);    
            GetComponent<VehicleComponent>().Initialize(15, 1, roadEfficiency, 2);
            GetComponent<ModularVehicleComponent>().Initialize(0, 1, segmentTypeList, attachmentTypeList);
        }
    }
}
