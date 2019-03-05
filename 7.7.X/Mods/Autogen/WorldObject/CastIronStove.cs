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
    [RequireRoomMaterialTier(2)]        
    public partial class CastIronStoveObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cast Iron Stove"); } } 

        public virtual Type RepresentedItemType { get { return typeof(CastIronStoveItem); } } 

        private static Type[] fuelTypeList = new[]
        {
            typeof(LogItem),
            typeof(LumberItem),
            typeof(CharcoalItem),
            typeof(ArrowItem),
            typeof(BoardItem),
            typeof(CoalItem),
        };

        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Cooking");                                 
            GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);                           
            GetComponent<FuelConsumptionComponent>().Initialize(10);                    
            GetComponent<HousingComponent>().Set(CastIronStoveItem.HousingVal);                                



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(10000)]
    public partial class CastIronStoveItem : WorldObjectItem<CastIronStoveObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cast Iron Stove"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("The perfect stove for the fledgling chef."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Kitchen",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Cooking", 
                                                    DiminishingReturnPercent = 0.3f    
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w from fuel"), Text.Info(10))); } } 
    }


    [RequiresSkill(typeof(SmeltingSkill), 1)]
    public partial class CastIronStoveRecipe : Recipe
    {
        public CastIronStoveRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<CastIronStoveItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<HingeItem>(typeof(SmeltingSkill), 6, SmeltingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(CastIronStoveRecipe), Item.Get<CastIronStoveItem>().UILink(), 15, typeof(SmeltingSkill));
            Initialize(Localizer.DoStr("Cast Iron Stove"), typeof(CastIronStoveRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}