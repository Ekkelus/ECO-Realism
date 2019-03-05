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
    public partial class TallowCandleObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tallow Candle"); } } 

        public virtual Type RepresentedItemType { get { return typeof(TallowCandleItem); } } 

        private static Type[] fuelTypeList = new[]
        {
            typeof(TallowItem),
            typeof(OilItem),
            typeof(BeeswaxItem),
        };

        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Lights");                                 
            GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);                           
            GetComponent<FuelConsumptionComponent>().Initialize(0.2f);                    
            GetComponent<HousingComponent>().Set(TallowCandleItem.HousingVal);
            GetComponent<PropertyAuthComponent>().Initialize();



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(250)]
    public partial class TallowCandleItem : WorldObjectItem<TallowCandleObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tallow Candle"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A candle which can burn tallow to produce a small amount of light."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 1,                                   
                                                    TypeForRoomLimit = "Lights", 
                                                    DiminishingReturnPercent = 0.8f    
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w from fuel"), Text.Info(1))); } } 
    }


    public partial class TallowCandleRecipe : Recipe
    {
        public TallowCandleRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<TallowCandleItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<TallowItem>(3),   
            };
            CraftMinutes = new ConstantValue(2.5f);
            Initialize(Localizer.DoStr("Tallow Candle"), typeof(TallowCandleRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}