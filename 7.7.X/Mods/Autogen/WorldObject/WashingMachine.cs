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
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerConsumptionComponent))]                     
    [RequireComponent(typeof(HousingComponent))]                          
    [RequireComponent(typeof(RoomRequirementsComponent))]
	[RequireComponent(typeof(SolidGroundComponent))] 
    [RequireRoomContainment]
    [RequireRoomVolume(8)]                              
    [RequireRoomMaterialTier(2.5f)]        
    public partial class WashingMachineObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Washing Machine"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WashingMachineItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Housing");                                 
            GetComponent<PowerConsumptionComponent>().Initialize(100);                      
            GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        
            GetComponent<HousingComponent>().Set(WashingMachineItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(3000)]
    public partial class WashingMachineItem : WorldObjectItem<WashingMachineObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Washing Machine"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Why hand scrub your clothes on a washboard when you could throw them into this magical cleaning machine?"); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Bathroom",
                                                    Val = 20,                                   
                                                    TypeForRoomLimit = "Washing", 
                                                    DiminishingReturnPercent = 0.3f    
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(100))); } }  
    }


    [RequiresSkill(typeof(IndustrySkill), 3)]
    public partial class WashingMachineRecipe : Recipe
    {
        public WashingMachineRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<WashingMachineItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(IndustrySkill), 20, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<CircuitItem>(typeof(IndustrySkill), 5, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<HingeItem>(typeof(IndustrySkill), 4, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<GlassItem>(typeof(IndustrySkill), 4, IndustrySkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(WashingMachineRecipe), Item.Get<WashingMachineItem>().UILink(), 10, typeof(IndustrySkill));
            Initialize(Localizer.DoStr("Washing Machine"), typeof(WashingMachineRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }
}