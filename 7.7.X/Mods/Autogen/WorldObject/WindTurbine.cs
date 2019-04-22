namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Housing;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Gameplay.Systems.Tooltip;
    using Shared.Localization;
    using Shared.Serialization;
    using Shared.Utils;
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
            GetComponent<MinimapComponent>().Initialize("Power");                                 
            GetComponent<PowerGridComponent>().Initialize(30, new ElectricPower());        
            GetComponent<PowerGeneratorComponent>().Initialize(750);                       
            GetComponent<HousingComponent>().Set(WindTurbineItem.HousingVal);
            GetComponent<SpecificGroundComponent>().Initialize(typeof(ReinforcedConcreteItem));



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

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
        
        [Tooltip(8)] private LocString PowerProductionTooltip  { get { return new LocString(string.Format(Localizer.DoStr("Produces: {0}w"), Text.Info(1000))); } } 
    }


    [RequiresSkill(typeof(MechanicsSkill), 4)]
    public partial class WindTurbineRecipe : Recipe
    {
        public WindTurbineRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<WindTurbineItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(MechanicsSkill), 50, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<GearboxItem>(typeof(MechanicsSkill), 5, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<CircuitItem>(typeof(MechanicsSkill), 10, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<ReinforcedConcreteItem>(typeof(MechanicsSkill), 6, MechanicsSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(WindTurbineRecipe), Item.Get<WindTurbineItem>().UILink(), 50, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Wind Turbine"), typeof(WindTurbineRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }
}