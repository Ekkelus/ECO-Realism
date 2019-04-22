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
    public partial class WoodenTableLampObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wooden Table Lamp"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WoodenTableLampItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Lights");                                 
            GetComponent<PowerConsumptionComponent>().Initialize(250);                      
            GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        
            GetComponent<HousingComponent>().Set(WoodenTableLampItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class WoodenTableLampItem :
        WorldObjectItem<WoodenTableLampObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wooden Table Lamp"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("For late night studying. Or working. Or anything, really."); } }

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


    [RequiresSkill(typeof(LumberSkill), 2)]
    public partial class WoodenTableLampRecipe : Recipe
    {
        public WoodenTableLampRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<WoodenTableLampItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LightBulbItem>(), 
                new CraftingElement<LumberItem>(typeof(LumberSkill), 5, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(LumberSkill), 10, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<ClothItem>(typeof(LumberSkill), 10, LumberSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(WoodenTableLampRecipe), Item.Get<WoodenTableLampItem>().UILink(), 10, typeof(AdvancedSmeltingSkill), typeof(AdvancedSmeltingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Wooden Table Lamp"), typeof(WoodenTableLampRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}