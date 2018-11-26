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
    public partial class SteelTableLampObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Steel Table Lamp"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SteelTableLampItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Lights");                                 
            this.GetComponent<PowerConsumptionComponent>().Initialize(250);                      
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        
            this.GetComponent<HousingComponent>().Set(SteelTableLampItem.HousingVal);                                


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

        static SteelTableLampItem()
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


    [RequiresSkill(typeof(SteelworkingSkill), 1)]
    public partial class SteelTableLampRecipe : Recipe
    {
        public SteelTableLampRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SteelTableLampItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LightBulbItem>(1), 
                new CraftingElement<SteelItem>(typeof(SteelworkingEfficiencySkill), 8, SteelworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PlasticItem>(typeof(SteelworkingEfficiencySkill), 5, SteelworkingEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(10, SteelworkingSpeedSkill.MultiplicativeStrategy, typeof(SteelworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(SteelTableLampRecipe), Item.Get<SteelTableLampItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<SteelTableLampItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.Do("Steel Table Lamp"), typeof(SteelTableLampRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }
}