namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Housing;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Property;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
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
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(45)]                              
    [RequireRoomMaterialTier(1.8f)]        
    public partial class BakeryOvenObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Bakery Oven"); } } 

        public virtual Type RepresentedItemType { get { return typeof(BakeryOvenItem); } } 

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
            GetComponent<HousingComponent>().Set(BakeryOvenItem.HousingVal);
            GetComponent<PropertyAuthComponent>().Initialize();



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(10000)]
    public partial class BakeryOvenItem : WorldObjectItem<BakeryOvenObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Bakery Oven"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A solidly built brick oven useful for baking all manner of treats."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Kitchen",
                                                    Val = 3,
                                                    TypeForRoomLimit = "Baking",
                                                    DiminishingReturnPercent = 0.3f
                                                };}}       
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w from fuel"), Text.Info(10))); } } 
    }


    [RequiresSkill(typeof(MortaringSkill), 2)]
    public partial class BakeryOvenRecipe : Recipe
    {
        public BakeryOvenRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<BakeryOvenItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BrickItem>(typeof(MortaringSkill), 30, MortaringSkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(MortaringSkill), 6, MortaringSkill.MultiplicativeStrategy),
                new CraftingElement<IronIngotItem>(typeof(MortaringSkill), 10, MortaringSkill.MultiplicativeStrategy),
                new CraftingElement<HingeItem>(typeof(MortaringSkill), 10, MortaringSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(BakeryOvenRecipe), Item.Get<BakeryOvenItem>().UILink(), 20, typeof(MortaringSkill), typeof(MortaringFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Bakery Oven"), typeof(BakeryOvenRecipe));
            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }
}