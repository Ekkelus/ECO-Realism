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
    using Eco.Gameplay.Property;
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
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                              
    [RequireRoomMaterialTier(2)]        
    public partial class StoveObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Stove"); } } 

        public virtual Type RepresentedItemType { get { return typeof(StoveItem); } } 

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
            this.GetComponent<MinimapComponent>().Initialize("Cooking");                                 
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);                           
            this.GetComponent<FuelConsumptionComponent>().Initialize(10);                    
            this.GetComponent<HousingComponent>().Set(StoveItem.HousingVal);                                

            var tankList = new List<LiquidTank>();
            
            tankList.Add(new LiquidProducer("Chimney", typeof(SmogItem), 100,
                    null,                                                                
                    this.Occupancy.Find(x => x.Name == "ChimneyOut"),   
                        (float)(0.4f * SmogItem.SmogItemsPerCO2PPM) / TimeUtil.SecondsPerHour)); 
            
            
            
            this.GetComponent<PipeComponent>().Initialize(tankList);

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class StoveItem :
        WorldObjectItem<StoveObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Stove"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A heavy stove for cooking more complex dishes."); } }

        static StoveItem()
        {
            
            
            
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Kitchen",
                                                    Val = 4,                                   
                                                    TypeForRoomLimit = "Cooking", 
                                                    DiminishingReturnPercent = 0.3f    
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w from fuel"), Text.Info(10))); } } 
    }


    [RequiresSkill(typeof(IndustrySkill), 1)]
    public partial class StoveRecipe : Recipe
    {
        public StoveRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<StoveItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HingeItem>(typeof(SmeltingSkill), 4, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<GlassJarItem>(typeof(SmeltingSkill), 4, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<SteelPlateItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<RivetItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy),   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(StoveRecipe), Item.Get<StoveItem>().UILink(), 20, typeof(SmeltingSkill));
            this.Initialize(Localizer.DoStr("Stove"), typeof(StoveRecipe));
            CraftingComponent.AddRecipe(typeof(ElectricMachinistTableObject), this);
        }
    }
}