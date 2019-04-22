namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Property;
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
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(45)]                              
    [RequireRoomMaterialTier(0.9f)]        
    public partial class KilnObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Kiln"); } } 

        public virtual Type RepresentedItemType { get { return typeof(KilnItem); } } 

        private static Type[] fuelTypeList = new[]
        {
            typeof(LogItem),
            typeof(LumberItem),
            typeof(CharcoalItem),
            typeof(ArrowItem),
            typeof(BoardItem),
            typeof(CoalItem),
            typeof(WoodPulpItem),
        };

        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Crafting");                                 
            GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);                           
            GetComponent<FuelConsumptionComponent>().Initialize(50);
            GetComponent<PropertyAuthComponent>().Initialize();



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(10000)]
    public partial class KilnItem : WorldObjectItem<KilnObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Kiln"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Useful for baking bricks and finishing pottery."); } }


        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w from fuel"), Text.Info(50))); } } 
    }


    [RequiresSkill(typeof(MortaringSkill), 4)]
    public partial class KilnRecipe : Recipe
    {
        public KilnRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<KilnItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(MortaringSkill), 60, MortaringSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(KilnRecipe), Item.Get<KilnItem>().UILink(), 30, typeof(MortaringSkill), typeof(MortaringFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Kiln"), typeof(KilnRecipe));
            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }
}