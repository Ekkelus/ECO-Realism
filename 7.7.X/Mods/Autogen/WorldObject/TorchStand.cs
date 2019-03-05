namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.DynamicValues;
    using Gameplay.Housing;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Systems.Tooltip;
    using Shared.Localization;
    using Shared.Serialization;
    using Shared.Utils;

    [Serialized]    
    [RequireComponent(typeof(OnOffComponent))]                   
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(FuelSupplyComponent))]                      
    [RequireComponent(typeof(FuelConsumptionComponent))]                 
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class TorchStandObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Torch Stand"); } } 

        public virtual Type RepresentedItemType { get { return typeof(TorchStandItem); } } 

        private static Type[] fuelTypeList = new[]
        {
            typeof(TorchItem),
        };

        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Lights");                                 
            GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);                           
            GetComponent<FuelConsumptionComponent>().Initialize(0.5f);                    
            GetComponent<HousingComponent>().Set(TorchStandItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1000)]
    public partial class TorchStandItem :
        WorldObjectItem<TorchStandObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Torch Stand"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A stand for a torch."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 1,                                   
                                                    TypeForRoomLimit = "Lights", 
                                                    DiminishingReturnPercent = 0.8f    
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w from fuel"), Text.Info(0.5f))); } } 
    }


    public partial class TorchStandRecipe : Recipe
    {
        public TorchStandRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<TorchStandItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(10),
                new CraftingElement<RopeItem>(),
            };
            CraftMinutes = new ConstantValue(2); 
            Initialize(Localizer.DoStr("Torch Stand"), typeof(TorchStandRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}