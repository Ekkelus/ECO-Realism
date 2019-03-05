namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Housing;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Gameplay.Systems.Tooltip;
    using Shared.Localization;
    using Shared.Serialization;
    using Shared.Utils;

    [Serialized]    
    [RequireComponent(typeof(OnOffComponent))]                   
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(FuelSupplyComponent))]                      
    [RequireComponent(typeof(FuelConsumptionComponent))]                 
    [RequireComponent(typeof(HousingComponent))]                  
    public partial class CandleStandObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Candle Stand"); } } 

        public virtual Type RepresentedItemType { get { return typeof(CandleStandItem); } } 

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
            GetComponent<HousingComponent>().Set(CandleStandItem.HousingVal);
            GetComponent<PropertyAuthComponent>().Initialize();



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(500)]
    public partial class CandleStandItem : WorldObjectItem<CandleStandObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Candle Stand"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A fancy candelabra for providing small amounts of light."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Lights", 
                                                    DiminishingReturnPercent = 0.8f    
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w from fuel"), Text.Info(1))); } } 
    }


    [RequiresSkill(typeof(SmeltingSkill), 1)]
    public partial class CandleStandRecipe : Recipe
    {
        public CandleStandRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<CandleStandItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(SmeltingSkill), 5, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<TallowItem>(typeof(SmeltingSkill), 2, SmeltingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(CandleStandRecipe), Item.Get<CandleStandItem>().UILink(), 1, typeof(SmeltingSkill));
            Initialize(Localizer.DoStr("Candle Stand"), typeof(CandleStandRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}