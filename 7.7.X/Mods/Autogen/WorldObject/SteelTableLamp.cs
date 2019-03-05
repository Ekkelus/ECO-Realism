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
    public partial class SteelTableLampObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Steel Table Lamp"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SteelTableLampItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Lights");                                 
            GetComponent<PowerConsumptionComponent>().Initialize(250);                      
            GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        
            GetComponent<HousingComponent>().Set(SteelTableLampItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1000)]
    public partial class SteelTableLampItem :
        WorldObjectItem<SteelTableLampObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Steel Table Lamp"); } } 
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


    [RequiresSkill(typeof(AdvancedSmeltingSkill), 1)]
    public partial class SteelTableLampRecipe : Recipe
    {
        public SteelTableLampRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SteelTableLampItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LightBulbItem>(), 
                new CraftingElement<SteelItem>(typeof(AdvancedSmeltingSkill), 8, AdvancedSmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<PlasticItem>(typeof(AdvancedSmeltingSkill), 5, AdvancedSmeltingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(SteelTableLampRecipe), Item.Get<SteelTableLampItem>().UILink(), 10, typeof(AdvancedSmeltingSkill));
            Initialize(Localizer.DoStr("Steel Table Lamp"), typeof(SteelTableLampRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }
}