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
    [Weight(15000)]  
    public class WoodCartItem : WorldObjectItem<WoodCartObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wood Cart"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Small cart for hauling small loads."); } }
    }

    [RequiresSkill(typeof(BasicEngineeringSkill), 1)] 
    public class WoodCartRecipe : Recipe
    {
        public WoodCartRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<WoodCartItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<WoodenWheelItem>(2), 
                new CraftingElement<HewnLogItem>(typeof(BasicEngineeringSkill), 30, BasicEngineeringSkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(BasicEngineeringSkill), 50, BasicEngineeringSkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(BasicEngineeringSkill), 2, BasicEngineeringSkill.MultiplicativeStrategy),
                new CraftingElement<IronIngotItem>(typeof(BasicEngineeringSkill), 4, BasicEngineeringSkill.MultiplicativeStrategy),
            };
            CraftMinutes = new ConstantValue(5);

            Initialize(Localizer.DoStr("Wood Cart"), typeof(WoodCartRecipe));
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
            AddOccupancy<WoodCartObject>(new List<BlockOccupancy>(0));
        }


        public override LocString DisplayName { get { return Localizer.DoStr("Wood Cart"); } }
        public Type RepresentedItemType { get { return typeof(WoodCartItem); } }


        private WoodCartObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            GetComponent<PublicStorageComponent>().Initialize(12, 2000000);           
            GetComponent<VehicleComponent>().Initialize(12, 1, 1);
            GetComponent<VehicleComponent>().HumanPowered(1);           
            GetComponent<StockpileComponent>().Initialize(new Vector3i(2,1,2));  
        }
    }
}