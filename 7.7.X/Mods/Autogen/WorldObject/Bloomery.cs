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

        private static Type[] fuelTypeList = new Type[]
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
            this.GetComponent<MinimapComponent>().Initialize("Crafting");                                 
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);                           
            this.GetComponent<FuelConsumptionComponent>().Initialize(10);                    
            this.GetComponent<HousingComponent>().Set(BloomeryItem.HousingVal);
            this.GetComponent<PropertyAuthComponent>().Initialize();



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

        static BloomeryItem()
        {
            
        }
        
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
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BloomeryItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BrickItem>(typeof(MortaringSkill), 10, MortaringSkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(MortaringSkill), 20, MortaringSkill.MultiplicativeStrategy),
                new CraftingElement<SandItem>(typeof(MortaringSkill), 10, MortaringSkill.MultiplicativeStrategy),   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BloomeryRecipe), Item.Get<BloomeryItem>().UILink(), 10, typeof(MortaringSkill));
            this.Initialize(Localizer.DoStr("Bloomery"), typeof(BloomeryRecipe));
            CraftingComponent.AddRecipe(typeof(KilnObject), this);
        }
    }
}