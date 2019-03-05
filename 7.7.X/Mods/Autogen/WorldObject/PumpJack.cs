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
            GetComponent<MinimapComponent>().Initialize("Crafting");                                 
            GetComponent<PowerConsumptionComponent>().Initialize(100);                      
            GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        
            GetComponent<HousingComponent>().Set(PumpJackItem.HousingVal);                                


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

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(100))); } }  
    }


    [RequiresSkill(typeof(MechanicsSkill), 4)]
    public partial class PumpJackRecipe : Recipe
    {
        public PumpJackRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<PumpJackItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<ConcreteItem>(typeof(MechanicsSkill), 14, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<GearboxItem>(typeof(MechanicsSkill), 4, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<PistonItem>(typeof(MechanicsSkill), 6, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<CopperPipeItem>(typeof(MechanicsSkill), 6, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<WaterwheelItem>(typeof(MechanicsSkill), 2, MechanicsSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(PumpJackRecipe), Item.Get<PumpJackItem>().UILink(), 50, typeof(MechanicsSkill));
            Initialize(Localizer.DoStr("Pump Jack"), typeof(PumpJackRecipe));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }
}