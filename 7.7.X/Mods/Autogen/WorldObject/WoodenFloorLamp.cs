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
    public partial class WoodenFloorLampObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wooden Floor Lamp"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WoodenFloorLampItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Lights");                                 
            this.GetComponent<PowerConsumptionComponent>().Initialize(250);                      
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        
            this.GetComponent<HousingComponent>().Set(WoodenFloorLampItem.HousingVal);                                


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

        static WoodenFloorLampItem()
        {
            
        }

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
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WoodenFloorLampItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LightBulbItem>(1), 
                new CraftingElement<LumberItem>(typeof(LumberSkill), 10, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(LumberSkill), 20, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<ClothItem>(typeof(LumberSkill), 10, LumberSkill.MultiplicativeStrategy),   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(WoodenFloorLampRecipe), Item.Get<WoodenFloorLampItem>().UILink(), 10, typeof(AdvancedSmeltingSkill));
            this.Initialize(Localizer.DoStr("Wooden Floor Lamp"), typeof(WoodenFloorLampRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}