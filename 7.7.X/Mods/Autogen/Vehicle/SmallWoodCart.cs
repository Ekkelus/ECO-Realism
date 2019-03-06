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
    [Weight(5000)]  
    public class SmallWoodCartItem : WorldObjectItem<SmallWoodCartObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Wood Cart"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A small wood cart for hauling minimal loads."); } }
    }

    [RequiresSkill(typeof(HewingSkill), 1)] 
    public class SmallWoodCartRecipe : Recipe
    {
        public SmallWoodCartRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SmallWoodCartItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<WoodenWheelItem>(2),
                new CraftingElement<HewnLogItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(HewingSkill), 15, HewingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = new ConstantValue(5);

            Initialize(Localizer.DoStr("Small Wood Cart"), typeof(SmallWoodCartRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]      
    [RequireComponent(typeof(MovableLinkComponent))]        
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]     
    public partial class SmallWoodCartObject : PhysicsWorldObject, IRepresentsItem
    {
        static SmallWoodCartObject()
        {
            AddOccupancy<SmallWoodCartObject>(new List<BlockOccupancy>(0));
        }


        public override LocString DisplayName { get { return Localizer.DoStr("Small Wood Cart"); } }
        public Type RepresentedItemType { get { return typeof(SmallWoodCartItem); } }


        private SmallWoodCartObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            GetComponent<PublicStorageComponent>().Initialize(8, 1400000);           
            GetComponent<VehicleComponent>().Initialize(10, 1, 1);
            GetComponent<VehicleComponent>().HumanPowered(0.5f);           
        }
    }
}