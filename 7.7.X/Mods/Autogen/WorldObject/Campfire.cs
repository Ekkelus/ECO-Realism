namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.DynamicValues;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Systems.Tooltip;
    using Shared.Localization;
    using Shared.Serialization;
    using Shared.Utils;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(FuelSupplyComponent))]                      
    [RequireComponent(typeof(FuelConsumptionComponent))]                 
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class CampfireObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Campfire"); } } 

        public virtual Type RepresentedItemType { get { return typeof(CampfireItem); } } 

        private static Type[] fuelTypeList = new[]
        {
            typeof(LogItem),
            typeof(LumberItem),
            typeof(CharcoalItem),
            typeof(ArrowItem),
            typeof(BoardItem),
            typeof(CoalItem),
            typeof(WoodPulpItem),
        };

        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Cooking");                                 
            GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);                           
            GetComponent<FuelConsumptionComponent>().Initialize(10);                    


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1000)]
    public partial class CampfireItem :
        WorldObjectItem<CampfireObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Campfire"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Cook like a caveman on an uneven fire."); } }


        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w from fuel"), Text.Info(10))); } } 
    }


    public partial class CampfireRecipe : Recipe
    {
        public CampfireRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<CampfireItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(3),
                new CraftingElement<StoneItem>(12),                                                                    
            };
            CraftMinutes = new ConstantValue(1); 
            Initialize(Localizer.DoStr("Campfire"), typeof(CampfireRecipe));
            CraftingComponent.AddRecipe(typeof(CampsiteObject), this);
        }
    }
}