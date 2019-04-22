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
    public partial class LatrineObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wooden Latrine"); } }

        public virtual Type RepresentedItemType { get { return typeof(LatrineItem); } }

        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Misc");                                 
            GetComponent<HousingComponent>().Set(LatrineItem.HousingVal);                                



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1000)]
    public partial class LatrineItem : WorldObjectItem<LatrineObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wooden Latrine"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A wooden potty."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Bathroom",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Toilet", 
                                                    DiminishingReturnPercent = 0.1f    
        };}}
    }


    [RequiresSkill(typeof(HewingSkill), 4)]
    public partial class LatrineRecipe : Recipe
    {
        public LatrineRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<LatrineItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(HewingSkill), 25, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<GlueItem>(typeof(HewingSkill), 2, HewingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(LatrineRecipe), Item.Get<LatrineItem>().UILink(), 5, typeof(HewingSkill), typeof(HewingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Latrine"), typeof(LatrineRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}