namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Housing;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Property;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Gameplay.Systems.Tooltip;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(HousingComponent))]                          
    [RequireComponent(typeof(RoomRequirementsComponent))]
	[RequireComponent(typeof(SolidGroundComponent))] 
    [RequireRoomContainment]
    [RequireRoomVolume(16)]                              
    [RequireRoomMaterialTier(0.8f)]        
    public partial class WoodenFabricBedObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wooden Fabric Bed"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WoodenFabricBedItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Misc");                                 
            GetComponent<HousingComponent>().Set(WoodenFabricBedItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(3000)]
    public partial class WoodenFabricBedItem : WorldObjectItem<WoodenFabricBedObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wooden Fabric Bed"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A much more comfortable bed made with fabric."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Bedroom",
                                                    Val = 4,                                   
                                                    TypeForRoomLimit = "Bed", 
                                                    DiminishingReturnPercent = 0.4f    
        };}}
    }


    [RequiresSkill(typeof(LumberSkill), 4)]
    public partial class WoodenFabricBedRecipe : Recipe
    {
        public WoodenFabricBedRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<WoodenFabricBedItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LumberItem>(typeof(LumberSkill), 20, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<ClothItem>(typeof(LumberSkill), 20, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(LumberSkill), 16, LumberSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(WoodenFabricBedRecipe), Item.Get<WoodenFabricBedItem>().UILink(), 5, typeof(LumberSkill), typeof(LumberFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Wooden Fabric Bed"), typeof(WoodenFabricBedRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}