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
    [Weight(15000)]  
    public class WoodCartItem : WorldObjectItem<WoodCartObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wood Cart"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Small cart for hauling small loads."); } }
    }

    [RequiresSkill(typeof(PrimitiveMechanicsSkill), 1)] 
    public class WoodCartRecipe : Recipe
    {
        public WoodCartRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WoodCartItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<WoodenWheelItem>(2), 
                new CraftingElement<HewnLogItem>(typeof(PrimitiveMechanicsEfficiencySkill), 30, PrimitiveMechanicsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(PrimitiveMechanicsEfficiencySkill), 50, PrimitiveMechanicsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(PrimitiveMechanicsEfficiencySkill), 2, PrimitiveMechanicsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<IronIngotItem>(typeof(PrimitiveMechanicsEfficiencySkill), 4, PrimitiveMechanicsEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(5);

            this.Initialize(Localizer.Do("Wood Cart"), typeof(WoodCartRecipe));
            CraftingComponent.AddRecipe(typeof(WainwrightTableObject), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]      
    [RequireComponent(typeof(MovableLinkComponent))]        
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(ModularStockpileComponent))]   
    [RequireComponent(typeof(TailingsReportComponent))]     
    public partial class WoodCartObject : PhysicsWorldObject, IRepresentsItem
    {
        static WoodCartObject()
        {
            WorldObject.AddOccupancy<WoodCartObject>(new List<BlockOccupancy>(0));
        }

        private static Dictionary<Type, float> roadEfficiency = new Dictionary<Type, float>()
        {
            { typeof(GrassBlock) , 0.7f}, { typeof(SandBlock) , 0.3f},
            { typeof(DesertSandBlock) , 0.3f}, { typeof(WetlandsBlock) , 0.5f},
            { typeof(SnowBlock) , 0.5f}, { typeof(DirtBlock) , 0.8f},
            { typeof(ForestSoilBlock) , 0.6f},
            { typeof(DirtRoadBlock), 1 }, { typeof(DirtRoadWorldObjectBlock), 1 },
            { typeof(StoneRoadBlock), 1.2f }, { typeof(StoneRoadWorldObjectBlock), 1.2f },
            { typeof(AsphaltRoadBlock), 1.4f }, { typeof(AsphaltRoadWorldObjectBlock), 1.4f }
        };
        public override LocString DisplayName { get { return Localizer.DoStr("Wood Cart"); } }
        public Type RepresentedItemType { get { return typeof(WoodCartItem); } }


        private WoodCartObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(12, 2000000);           
            this.GetComponent<VehicleComponent>().Initialize(12, 1, roadEfficiency, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(1);           
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(2,1,2));  
        }
    }
}