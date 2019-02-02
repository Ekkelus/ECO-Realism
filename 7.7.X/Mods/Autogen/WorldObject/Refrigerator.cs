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
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerConsumptionComponent))]                     
    [RequireComponent(typeof(HousingComponent))]                          
    [RequireComponent(typeof(PublicStorageComponent))]                
    [RequireComponent(typeof(RoomRequirementsComponent))]
	[RequireComponent(typeof(SolidGroundComponent))] 
    [RequireRoomContainment]
    [RequireRoomVolume(8)]                              
    [RequireRoomMaterialTier(2.5f)]        
    public partial class RefrigeratorObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Refrigerator"); } } 

        public virtual Type RepresentedItemType { get { return typeof(RefrigeratorItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Housing");                                 
            this.GetComponent<PowerConsumptionComponent>().Initialize(100);                      
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        
            this.GetComponent<HousingComponent>().Set(RefrigeratorItem.HousingVal);
            this.GetComponent<PropertyAuthComponent>().Initialize();

            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(8);
            storage.Storage.AddInvRestriction(new NotCarriedRestriction()); // can't store block or large items


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(3000)]
    public partial class RefrigeratorItem : WorldObjectItem<RefrigeratorObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Refrigerator"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("An icebox from the future with significantly better cooling properties."); } }

        static RefrigeratorItem()
        {
            
        }
        
        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Kitchen",
                                                    Val = 10,                                   
                                                    TypeForRoomLimit = "Food Storage", 
                                                    DiminishingReturnPercent = 0.3f    
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(100))); } }  
    }


    [RequiresSkill(typeof(IndustrialEngineeringSkill), 3)]
    public partial class RefrigeratorRecipe : Recipe
    {
        public RefrigeratorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RefrigeratorItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(IndustrialEngineeringEfficiencySkill), 20, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CircuitItem>(typeof(IndustrialEngineeringEfficiencySkill), 5, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<HingeItem>(typeof(IndustrialEngineeringEfficiencySkill), 8, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<RadiatorItem>(typeof(IndustrialEngineeringEfficiencySkill), 5, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(10, IndustrialEngineeringSpeedSkill.MultiplicativeStrategy, typeof(IndustrialEngineeringSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(RefrigeratorRecipe), Item.Get<RefrigeratorItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<RefrigeratorItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Refrigerator"), typeof(RefrigeratorRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }
}