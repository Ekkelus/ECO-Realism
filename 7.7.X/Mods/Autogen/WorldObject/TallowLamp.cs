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
    public partial class TallowLampObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tallow Lamp"); } } 

        public virtual Type RepresentedItemType { get { return typeof(TallowLampItem); } } 

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
            GetComponent<HousingComponent>().Set(TallowLampItem.HousingVal);
            GetComponent<PropertyAuthComponent>().Initialize();



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(500)]
    public partial class TallowLampItem : WorldObjectItem<TallowLampObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tallow Lamp"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A pottery lamp. Fuel with tallow."); } }

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


    [RequiresSkill(typeof(MortaringSkill), 1)]
    public partial class TallowLampRecipe : Recipe
    {
        public TallowLampRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<TallowLampItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SandItem>(typeof(MortaringSkill), 4, MortaringSkill.MultiplicativeStrategy),
                new CraftingElement<TallowItem>(typeof(MortaringSkill), 2, MortaringSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(TallowLampRecipe), Item.Get<TallowLampItem>().UILink(), 2, typeof(MortaringSkill));
            Initialize(Localizer.DoStr("Tallow Lamp"), typeof(TallowLampRecipe));
            CraftingComponent.AddRecipe(typeof(KilnObject), this);
        }
    }
}