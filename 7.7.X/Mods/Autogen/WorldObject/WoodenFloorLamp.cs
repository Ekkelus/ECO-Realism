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
    public partial class WoodenFloorLampObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wooden Floor Lamp"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WoodenFloorLampItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Lights");                                 
            GetComponent<PowerConsumptionComponent>().Initialize(250);                      
            GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        
            GetComponent<HousingComponent>().Set(WoodenFloorLampItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class WoodenFloorLampItem :
        WorldObjectItem<WoodenFloorLampObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wooden Floor Lamp"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A more modern way to light up a room. This time from the floor."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 4,                                   
                                                    TypeForRoomLimit = "Lights", 
                                                    DiminishingReturnPercent = 0.8f    
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(250))); } }  
    }


    [RequiresSkill(typeof(LumberSkill), 3)]
    public partial class WoodenFloorLampRecipe : Recipe
    {
        public WoodenFloorLampRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<WoodenFloorLampItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LightBulbItem>(), 
                new CraftingElement<LumberItem>(typeof(LumberSkill), 10, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(LumberSkill), 20, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<ClothItem>(typeof(LumberSkill), 10, LumberSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(WoodenFloorLampRecipe), Item.Get<WoodenFloorLampItem>().UILink(), 10, typeof(AdvancedSmeltingSkill), typeof(AdvancedSmeltingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Wooden Floor Lamp"), typeof(WoodenFloorLampRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}