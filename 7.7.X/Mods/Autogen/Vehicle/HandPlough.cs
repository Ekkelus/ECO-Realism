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

    [Serialized]
    [Weight(15000)]  
    public class HandPloughItem : WorldObjectItem<HandPloughObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Hand Plough"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A tool that tills the field for farming."); } }
    }

    [RequiresSkill(typeof(BasicEngineeringSkill), 1)] 
    public class HandPloughRecipe : Recipe
    {
        public HandPloughRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<HandPloughItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<WoodenWheelItem>(), 
                new CraftingElement<HewnLogItem>(typeof(BasicEngineeringSkill), 10, BasicEngineeringSkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(BasicEngineeringSkill), 50, BasicEngineeringSkill.MultiplicativeStrategy),
                new CraftingElement<IronIngotItem>(typeof(BasicEngineeringSkill), 20, BasicEngineeringSkill.MultiplicativeStrategy),
            };
            CraftMinutes = new ConstantValue(5);

            Initialize(Localizer.DoStr("Hand Plough"), typeof(HandPloughRecipe));
            CraftingComponent.AddRecipe(typeof(WainwrightTableObject), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    public partial class HandPloughObject : PhysicsWorldObject, IRepresentsItem
    {
        static HandPloughObject()
        {
            AddOccupancy<HandPloughObject>(new List<BlockOccupancy>(0));
        }

        private static Dictionary<Type, float> roadEfficiency = new Dictionary<Type, float>()
        {
            { typeof(DirtRoadBlock), 1 }, { typeof(DirtRoadWorldObjectBlock), 1 },
            { typeof(StoneRoadBlock), 1.2f }, { typeof(StoneRoadWorldObjectBlock), 1.2f },
            { typeof(AsphaltRoadBlock), 1.4f }, { typeof(AsphaltRoadWorldObjectBlock), 1.4f }
        };

        public override LocString DisplayName { get { return Localizer.DoStr("Hand Plough"); } }
        public Type RepresentedItemType { get { return typeof(HandPloughItem); } }


        private HandPloughObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            GetComponent<VehicleComponent>().Initialize(10, 1, 1);
            GetComponent<VehicleComponent>().HumanPowered(1);           
        }
    }
}