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
    [RequireRoomMaterialTier(2)]        
    public partial class CastIronStoveObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cast Iron Stove"); } } 

        public virtual Type RepresentedItemType { get { return typeof(CastIronStoveItem); } } 

        private static Type[] fuelTypeList = new Type[]
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
            this.GetComponent<MinimapComponent>().Initialize("Cooking");                                 
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);                           
            this.GetComponent<FuelConsumptionComponent>().Initialize(10);                    
            this.GetComponent<HousingComponent>().Set(CastIronStoveItem.HousingVal);                                



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

        static CastIronStoveItem()
        {
            
        }
        
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


    [RequiresSkill(typeof(MetalworkingSkill), 1)]
    public partial class CastIronStoveRecipe : Recipe
    {
        public CastIronStoveRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CastIronStoveItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(MetalworkingEfficiencySkill), 20, MetalworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<HingeItem>(typeof(MetalworkingEfficiencySkill), 6, MetalworkingEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(15, MetalworkingSpeedSkill.MultiplicativeStrategy, typeof(MetalworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(CastIronStoveRecipe), Item.Get<CastIronStoveItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<CastIronStoveItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.Do("Cast Iron Stove"), typeof(CastIronStoveRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}