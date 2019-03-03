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
            this.GetComponent<MinimapComponent>().Initialize("Cooking");                                 
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);                           
            this.GetComponent<FuelConsumptionComponent>().Initialize(10);                    
            this.GetComponent<HousingComponent>().Set(BakeryOvenItem.HousingVal);
            this.GetComponent<PropertyAuthComponent>().Initialize();



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

        static BakeryOvenItem()
        {
            
        }

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
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BakeryOvenItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BrickItem>(typeof(MortaringSkill), 30, MortaringSkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(MortaringSkill), 6, MortaringSkill.MultiplicativeStrategy),
                new CraftingElement<IronIngotItem>(typeof(MortaringSkill), 10, MortaringSkill.MultiplicativeStrategy),
                new CraftingElement<HingeItem>(typeof(MortaringSkill), 10, MortaringSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BakeryOvenRecipe), Item.Get<BakeryOvenItem>().UILink(), 20, typeof(MortaringSkill));
            this.Initialize(Localizer.DoStr("Bakery Oven"), typeof(BakeryOvenRecipe));
            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }
}