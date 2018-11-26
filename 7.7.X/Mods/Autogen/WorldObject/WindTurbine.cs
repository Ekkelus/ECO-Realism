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
    using REYmod.Utils;

    [Serialized]    
    [RequireComponent(typeof(OnOffComponent))]                   
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerGeneratorComponent))]         
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(SpecificGroundComponent))]
    public partial class WindTurbineObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wind Turbine"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WindTurbineItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Power");                                 
            this.GetComponent<PowerGridComponent>().Initialize(30, new ElectricPower());        
            this.GetComponent<PowerGeneratorComponent>().Initialize(750);                       
            this.GetComponent<HousingComponent>().Set(WindTurbineItem.HousingVal);
            this.GetComponent<SpecificGroundComponent>().Initialize(typeof(ReinforcedConcreteItem));



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(10000)]
    public partial class WindTurbineItem : WorldObjectItem<WindTurbineObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wind Turbine"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Generates electrical power from the wind. Needs to be placed on 3x3 reinforced concrete."); } }

        static WindTurbineItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
        
        [Tooltip(8)] private LocString PowerProductionTooltip  { get { return new LocString(string.Format(Localizer.DoStr("Produces: {0}w"), Text.Info(1000))); } } 
    }


    [RequiresSkill(typeof(MechanicalEngineeringSkill), 4)]
    public partial class WindTurbineRecipe : Recipe
    {
        public WindTurbineRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WindTurbineItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(MechanicsAssemblyEfficiencySkill), 50, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GearboxItem>(typeof(MechanicsAssemblyEfficiencySkill), 5, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CircuitItem>(typeof(MechanicsAssemblyEfficiencySkill), 10, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<ReinforcedConcreteItem>(typeof(MechanicsAssemblyEfficiencySkill), 6, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(50, MechanicsAssemblySpeedSkill.MultiplicativeStrategy, typeof(MechanicsAssemblySpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(WindTurbineRecipe), Item.Get<WindTurbineItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<WindTurbineItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.Do("Wind Turbine"), typeof(WindTurbineRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }
}