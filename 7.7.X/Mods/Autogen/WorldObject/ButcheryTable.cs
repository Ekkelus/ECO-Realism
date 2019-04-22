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
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                              
    [RequireRoomMaterialTier(0.8f)]        
    public partial class ButcheryTableObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Butchery Table"); } } 

        public virtual Type RepresentedItemType { get { return typeof(ButcheryTableItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Cooking");                                 
            GetComponent<HousingComponent>().Set(ButcheryTableItem.HousingVal);                                



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(5000)]
    public partial class ButcheryTableItem : WorldObjectItem<ButcheryTableObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Butchery Table"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A block and cleaver to process raw meat into fancier dishes."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Kitchen",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Cooking", 
                                                    DiminishingReturnPercent = 0.3f    
        };}}
    }


    [RequiresSkill(typeof(HewingSkill), 3)]
    public partial class ButcheryTableRecipe : Recipe
    {
        public ButcheryTableRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<ButcheryTableItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(HewingSkill), 20, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(HewingSkill), 20, HewingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(ButcheryTableRecipe), Item.Get<ButcheryTableItem>().UILink(), 5, typeof(HewingSkill), typeof(HewingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Butchery Table"), typeof(ButcheryTableRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}