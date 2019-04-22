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
    [RequireComponent(typeof(FuelSupplyComponent))]                      
    [RequireComponent(typeof(FuelConsumptionComponent))]                 
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class BloomeryObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Bloomery"); } } 

        public virtual Type RepresentedItemType { get { return typeof(BloomeryItem); } } 

        private static Type[] fuelTypeList = new[]
        {
            typeof(LogItem),
            typeof(LumberItem),
            typeof(CharcoalItem),
            typeof(ArrowItem),
            typeof(BoardItem),
            typeof(CoalItem),
        };

        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Crafting");                                 
            GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);                           
            GetComponent<FuelConsumptionComponent>().Initialize(10);                    
            GetComponent<HousingComponent>().Set(BloomeryItem.HousingVal);
            GetComponent<PropertyAuthComponent>().Initialize();



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(10000)]
    public partial class BloomeryItem : WorldObjectItem<BloomeryObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Bloomery"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A chimney-shaped furnace for smelting ores."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w from fuel"), Text.Info(10))); } } 
    }


    [RequiresSkill(typeof(MortaringSkill), 2)]
    public partial class BloomeryRecipe : Recipe
    {
        public BloomeryRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<BloomeryItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BrickItem>(typeof(MortaringSkill), 10, MortaringSkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(MortaringSkill), 20, MortaringSkill.MultiplicativeStrategy),
                new CraftingElement<SandItem>(typeof(MortaringSkill), 10, MortaringSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(BloomeryRecipe), Item.Get<BloomeryItem>().UILink(), 10, typeof(MortaringSkill), typeof(MortaringFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Bloomery"), typeof(BloomeryRecipe));
            CraftingComponent.AddRecipe(typeof(KilnObject), this);
        }
    }
}