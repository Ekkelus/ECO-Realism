namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
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
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerConsumptionComponent))]                     
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class PumpJackObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Pump Jack"); } } 

        public virtual Type RepresentedItemType { get { return typeof(PumpJackItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Crafting");                                 
            this.GetComponent<PowerConsumptionComponent>().Initialize(100);                      
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        
            this.GetComponent<HousingComponent>().Set(PumpJackItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(20000)]
    public partial class PumpJackItem : WorldObjectItem<PumpJackObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Pump Jack"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Drill, baby! Drill!"); } }

        static PumpJackItem()
        {
            
        }
        
        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(100))); } }  
    }


    [RequiresSkill(typeof(MechanicalEngineeringSkill), 4)]
    public partial class PumpJackRecipe : Recipe
    {
        public PumpJackRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PumpJackItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ConcreteItem>(typeof(MechanicsAssemblyEfficiencySkill), 14, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GearboxItem>(typeof(MechanicsAssemblyEfficiencySkill), 4, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PistonItem>(typeof(MechanicsAssemblyEfficiencySkill), 6, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CopperPipeItem>(typeof(MechanicsAssemblyEfficiencySkill), 6, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<WaterwheelItem>(typeof(MechanicsAssemblyEfficiencySkill), 2, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(50, MechanicsAssemblySpeedSkill.MultiplicativeStrategy, typeof(MechanicsAssemblySpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(PumpJackRecipe), Item.Get<PumpJackItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<PumpJackItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.Do("Pump Jack"), typeof(PumpJackRecipe));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }
}