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
            this.GetComponent<MinimapComponent>().Initialize("Lights");                                 
            this.GetComponent<PowerConsumptionComponent>().Initialize(250);                      
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        
            this.GetComponent<HousingComponent>().Set(SteelFloorLampItem.HousingVal);                                


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

        static SteelFloorLampItem()
        {
            
        }

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
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SteelFloorLampItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LightBulbItem>(1), 
                new CraftingElement<SteelItem>(typeof(AdvancedSmeltingSkill), 10, AdvancedSmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<PlasticItem>(typeof(AdvancedSmeltingSkill), 10, AdvancedSmeltingSkill.MultiplicativeStrategy),   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(SteelFloorLampRecipe), Item.Get<SteelFloorLampItem>().UILink(), 10, typeof(AdvancedSmeltingSkill));
            this.Initialize(Localizer.DoStr("Steel Floor Lamp"), typeof(SteelFloorLampRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }
}