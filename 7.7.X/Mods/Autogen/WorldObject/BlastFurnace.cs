namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;

    [Serialized]    
    [RequireComponent(typeof(PipeComponent))]                    
    [RequireComponent(typeof(AttachmentComponent))]              
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(FuelSupplyComponent))]                      
    [RequireComponent(typeof(FuelConsumptionComponent))]                 
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class BlastFurnaceObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Blast Furnace"); } } 

        public virtual Type RepresentedItemType { get { return typeof(BlastFurnaceItem); } } 

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
            this.GetComponent<FuelConsumptionComponent>().Initialize(50);                    
            this.GetComponent<HousingComponent>().Set(BlastFurnaceItem.HousingVal);
            this.GetComponent<PropertyAuthComponent>().Initialize();


            var tankList = new List<LiquidTank>();
            
            tankList.Add(new LiquidProducer("Chimney", typeof(SmogItem), 100,
                    null,                                                                
                    this.Occupancy.Find(x => x.Name == "ChimneyOut"),   
                        (float)(1 * SmogItem.SmogItemsPerCO2PPM) / TimeUtil.SecondsPerHour)); 
            
            
            
            this.GetComponent<PipeComponent>().Initialize(tankList);

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(20000)]
    public partial class BlastFurnaceItem : WorldObjectItem<BlastFurnaceObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Blast Furnace"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A superior replacement for the bloomery that can produce steel."); } }

        static BlastFurnaceItem()
        {
            
            
            
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w from fuel"), Text.Info(50))); } } 
    }


    [RequiresSkill(typeof(SmeltingSkill), 4)]
    public partial class BlastFurnaceRecipe : Recipe
    {
        public BlastFurnaceRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BlastFurnaceItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BrickItem>(typeof(SmeltingSkill), 10, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<IronPipeItem>(typeof(SmeltingSkill), 10, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<IronPlateItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<ScrewsItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BlastFurnaceRecipe), Item.Get<BlastFurnaceItem>().UILink(), 60, typeof(SmeltingSkill));
            this.Initialize(Localizer.DoStr("Blast Furnace"), typeof(BlastFurnaceRecipe));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }
}