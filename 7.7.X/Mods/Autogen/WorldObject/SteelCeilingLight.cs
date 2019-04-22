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
    public partial class SteelCeilingLightObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Steel Ceiling Light"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SteelCeilingLightItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Lights");                                 
            GetComponent<PowerConsumptionComponent>().Initialize(250);                      
            GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        
            GetComponent<HousingComponent>().Set(SteelCeilingLightItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(2000)]
    public partial class SteelCeilingLightItem :
        WorldObjectItem<SteelCeilingLightObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Steel Ceiling Light"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A more modern way to light up a room."); } }

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


    [RequiresSkill(typeof(AdvancedSmeltingSkill), 3)]
    public partial class SteelCeilingLightRecipe : Recipe
    {
        public SteelCeilingLightRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SteelCeilingLightItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LightBulbItem>(), 
                new CraftingElement<SteelItem>(typeof(AdvancedSmeltingSkill), 10, AdvancedSmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<PlasticItem>(typeof(AdvancedSmeltingSkill), 10, AdvancedSmeltingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(SteelCeilingLightRecipe), Item.Get<SteelCeilingLightItem>().UILink(), 10, typeof(AdvancedSmeltingSkill), typeof(AdvancedSmeltingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Steel Ceiling Light"), typeof(SteelCeilingLightRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }
}