namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

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
            this.GetComponent<MinimapComponent>().Initialize("Misc");                                 
            this.GetComponent<HousingComponent>().Set(LatrineItem.HousingVal);                                



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

        static LatrineItem()
        {
            
        }

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
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LatrineItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(HewingSkill), 25, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<GlueItem>(typeof(HewingSkill), 2, HewingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(LatrineRecipe), Item.Get<LatrineItem>().UILink(), 5, typeof(HewingSkill));
            this.Initialize(Localizer.DoStr("Latrine"), typeof(LatrineRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}