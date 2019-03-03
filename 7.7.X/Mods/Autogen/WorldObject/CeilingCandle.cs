namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;

    [Serialized]    
    [RequireComponent(typeof(OnOffComponent))]                   
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(FuelSupplyComponent))]                      
    [RequireComponent(typeof(FuelConsumptionComponent))]                 
    [RequireComponent(typeof(HousingComponent))]                  
    public partial class CeilingCandleObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Ceiling Candle"); } } 

        public virtual Type RepresentedItemType { get { return typeof(CeilingCandleItem); } } 

        private static Type[] fuelTypeList = new Type[]
        {
            typeof(TallowItem),
            typeof(OilItem),
            typeof(BeeswaxItem),
        };

        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Lights");                                 
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);                           
            this.GetComponent<FuelConsumptionComponent>().Initialize(0.2f);                    
            this.GetComponent<HousingComponent>().Set(CeilingCandleItem.HousingVal);
            this.GetComponent<PropertyAuthComponent>().Initialize();



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(500)]
    public partial class CeilingCandleItem : WorldObjectItem<CeilingCandleObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Ceiling Candle"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A fancy hanging candelabra."); } }

        static CeilingCandleItem()
        {
            
        }

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
    public partial class CeilingCandleRecipe : Recipe
    {
        public CeilingCandleRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CeilingCandleItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(SmeltingSkill), 5, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<TallowItem>(typeof(SmeltingSkill), 2, SmeltingSkill.MultiplicativeStrategy),   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CeilingCandleRecipe), Item.Get<CeilingCandleItem>().UILink(), 1, typeof(SmeltingSkill));
            this.Initialize(Localizer.DoStr("Ceiling Candle"), typeof(CeilingCandleRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}