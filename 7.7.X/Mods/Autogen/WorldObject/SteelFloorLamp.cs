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
    [RequireComponent(typeof(OnOffComponent))]                   
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerConsumptionComponent))]                     
    [RequireComponent(typeof(HousingComponent))]                  
    public partial class SteelFloorLampObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Steel Floor Lamp"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SteelFloorLampItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Lights");                                 
            GetComponent<PowerConsumptionComponent>().Initialize(250);                      
            GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        
            GetComponent<HousingComponent>().Set(SteelFloorLampItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(2000)]
    public partial class SteelFloorLampItem :
        WorldObjectItem<SteelFloorLampObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Steel Floor Lamp"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A more modern way to light up a room. This time from the floor."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 5,                                   
                                                    TypeForRoomLimit = "Lights", 
                                                    DiminishingReturnPercent = 0.8f    
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(250))); } }  
    }


    [RequiresSkill(typeof(AdvancedSmeltingSkill), 2)]
    public partial class SteelFloorLampRecipe : Recipe
    {
        public SteelFloorLampRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SteelFloorLampItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LightBulbItem>(), 
                new CraftingElement<SteelItem>(typeof(AdvancedSmeltingSkill), 10, AdvancedSmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<PlasticItem>(typeof(AdvancedSmeltingSkill), 10, AdvancedSmeltingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(SteelFloorLampRecipe), Item.Get<SteelFloorLampItem>().UILink(), 10, typeof(AdvancedSmeltingSkill), typeof(AdvancedSmeltingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Steel Floor Lamp"), typeof(SteelFloorLampRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }
}