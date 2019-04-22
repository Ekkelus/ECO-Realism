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
    public partial class WallCandleObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wall Candle"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WallCandleItem); } } 

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
            GetComponent<HousingComponent>().Set(WallCandleItem.HousingVal);
            GetComponent<PropertyAuthComponent>().Initialize();



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(500)]
    public partial class WallCandleItem : WorldObjectItem<WallCandleObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wall Candle"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A wall mounted candle."); } }

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


    [RequiresSkill(typeof(SmeltingSkill), 3)]
    public partial class WallCandleRecipe : Recipe
    {
        public WallCandleRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<WallCandleItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(SmeltingSkill), 5, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<TallowItem>(typeof(SmeltingSkill), 2, SmeltingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(WallCandleRecipe), Item.Get<WallCandleItem>().UILink(), 1, typeof(SmeltingSkill), typeof(SmeltingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Wall Candle"), typeof(WallCandleRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}