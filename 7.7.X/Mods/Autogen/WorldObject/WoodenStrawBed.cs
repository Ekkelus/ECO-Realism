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
    public partial class WoodenStrawBedObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wooden Straw Bed"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WoodenStrawBedItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Misc");                                 
            GetComponent<HousingComponent>().Set(WoodenStrawBedItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(3000)]
    public partial class WoodenStrawBedItem : WorldObjectItem<WoodenStrawBedObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wooden Straw Bed"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A nice, scratchy and horrible uncomfortable bed. But at least it keeps you off the ground."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Bedroom",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Bed", 
                                                    DiminishingReturnPercent = 0.4f    
        };}}
    }


    [RequiresSkill(typeof(HewingSkill), 3)]
    public partial class WoodenStrawBedRecipe : Recipe
    {
        public WoodenStrawBedRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<WoodenStrawBedItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(HewingSkill), 20, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<PlantFibersItem>(typeof(HewingSkill), 20, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(HewingSkill), 16, HewingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(WoodenStrawBedRecipe), Item.Get<WoodenStrawBedItem>().UILink(), 5, typeof(HewingSkill));
            Initialize(Localizer.DoStr("Wooden Straw Bed"), typeof(WoodenStrawBedRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}