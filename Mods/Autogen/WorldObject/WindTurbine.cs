namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    
    [Serialized]    
    [RequireComponent(typeof(OnOffComponent))]    
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerGeneratorComponent))]         
    [RequireComponent(typeof(HousingComponent))]                          
    public partial class WindTurbineObject : WorldObject
    {
        public override string FriendlyName { get { return "Wind Turbine"; } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Power");                                 
            this.GetComponent<PowerGridComponent>().Initialize(30, new ElectricPower());        
            this.GetComponent<PowerGeneratorComponent>().Initialize(750);                       
            this.GetComponent<HousingComponent>().Set(WindTurbineItem.HousingVal);                                



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class WindTurbineItem : WorldObjectItem<WindTurbineObject>
    {
        public override string FriendlyName { get { return "Wind Turbine"; } } 
        public override string Description { get { return "Generates electrical power from the wind."; } }

        static WindTurbineItem()
        {
            
        }
        
        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    Val = 0,
                                                    TypeForRoomLimit = "",
                                                    DiminishingReturnPercent = 0
                                                };}}       
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
            SkillModifiedValue value = new SkillModifiedValue(50, MechanicsAssemblySpeedSkill.MultiplicativeStrategy, typeof(MechanicsAssemblySpeedSkill), "craft time");
            SkillModifiedValueManager.AddBenefitForObject(typeof(WindTurbineRecipe), Item.Get<WindTurbineItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<WindTurbineItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Wind Turbine", typeof(WindTurbineRecipe));
            CraftingComponent.AddRecipe(typeof(MachineShopObject), this);
        }
    }
}