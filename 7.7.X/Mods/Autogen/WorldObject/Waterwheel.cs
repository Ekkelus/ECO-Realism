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
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerGeneratorComponent))]         
    [RequireComponent(typeof(HousingComponent))]                  
    public partial class WaterwheelObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Waterwheel"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WaterwheelItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Power");                                 
            GetComponent<PowerGridComponent>().Initialize(10, new MechanicalPower());        
            GetComponent<PowerGeneratorComponent>().Initialize(200);                       
            GetComponent<HousingComponent>().Set(WaterwheelItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(10000)]
    public partial class WaterwheelItem : WorldObjectItem<WaterwheelObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Waterwheel"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Use the power of flowing water to produce mechanical power."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
        
        [Tooltip(8)] private LocString PowerProductionTooltip  { get { return new LocString(string.Format(Localizer.DoStr("Produces: {0}w"), Text.Info(200))); } } 
    }


    [RequiresSkill(typeof(BasicEngineeringSkill), 2)]
    public partial class WaterwheelRecipe : Recipe
    {
        public WaterwheelRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<WaterwheelItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(BasicEngineeringSkill), 30, BasicEngineeringSkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(BasicEngineeringSkill), 50, BasicEngineeringSkill.MultiplicativeStrategy),
                new CraftingElement<WoodenGearItem>(typeof(BasicEngineeringSkill), 1, BasicEngineeringSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(WaterwheelRecipe), Item.Get<WaterwheelItem>().UILink(), 30, typeof(BasicEngineeringSkill));
            Initialize(Localizer.DoStr("Waterwheel"), typeof(WaterwheelRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}