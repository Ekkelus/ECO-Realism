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
            this.GetComponent<MinimapComponent>().Initialize("Housing");                                 
            this.GetComponent<PowerConsumptionComponent>().Initialize(100);                      
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        
            this.GetComponent<HousingComponent>().Set(WashingMachineItem.HousingVal);                                


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

        static WashingMachineItem()
        {
            
        }

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
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WashingMachineItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(IndustrySkill), 20, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<CircuitItem>(typeof(IndustrySkill), 5, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<HingeItem>(typeof(IndustrySkill), 4, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<GlassItem>(typeof(IndustrySkill), 4, IndustrySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(10, IndustrySkill.MultiplicativeStrategy, typeof(IndustrySkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(WashingMachineRecipe), Item.Get<WashingMachineItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<WashingMachineItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Washing Machine"), typeof(WashingMachineRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }
}