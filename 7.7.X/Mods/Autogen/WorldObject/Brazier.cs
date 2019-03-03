namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Property;
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
    [RequireComponent(typeof(SolidGroundComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomVolume(4)]
    public partial class BrazierObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Brazier"); } } 

        public virtual Type RepresentedItemType { get { return typeof(BrazierItem); } } 

        private static Type[] fuelTypeList = new Type[]
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
            this.GetComponent<MinimapComponent>().Initialize("Lights");                                 
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);                           
            this.GetComponent<FuelConsumptionComponent>().Initialize(1);                    
            this.GetComponent<HousingComponent>().Set(BrazierItem.HousingVal);
            this.GetComponent<PropertyAuthComponent>().Initialize();



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(2000)]
    public partial class BrazierItem : WorldObjectItem<BrazierObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Brazier"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A metal stand which can hold burning fuel to provide light."); } }

        static BrazierItem()
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


    [RequiresSkill(typeof(SmeltingSkill), 4)]
    public partial class BrazierRecipe : Recipe
    {
        public BrazierRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BrazierItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(SmeltingSkill), 10, SmeltingSkill.MultiplicativeStrategy),   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BrazierRecipe), Item.Get<BrazierItem>().UILink(), 1, typeof(SmeltingSkill));
            this.Initialize(Localizer.DoStr("Brazier"), typeof(BrazierRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}