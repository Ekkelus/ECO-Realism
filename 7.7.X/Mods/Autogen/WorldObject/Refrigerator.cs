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
            GetComponent<MinimapComponent>().Initialize("Housing");                                 
            GetComponent<PowerConsumptionComponent>().Initialize(100);                      
            GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        
            GetComponent<HousingComponent>().Set(RefrigeratorItem.HousingVal);
            GetComponent<PropertyAuthComponent>().Initialize();

            var storage = GetComponent<PublicStorageComponent>();
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


    [RequiresSkill(typeof(IndustrySkill), 3)]
    public partial class RefrigeratorRecipe : Recipe
    {
        public RefrigeratorRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<RefrigeratorItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(IndustrySkill), 20, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<CircuitItem>(typeof(IndustrySkill), 5, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<HingeItem>(typeof(IndustrySkill), 8, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<RadiatorItem>(typeof(IndustrySkill), 5, IndustrySkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(RefrigeratorRecipe), Item.Get<RefrigeratorItem>().UILink(), 10, typeof(IndustrySkill), typeof(IndustryFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Refrigerator"), typeof(RefrigeratorRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }
}