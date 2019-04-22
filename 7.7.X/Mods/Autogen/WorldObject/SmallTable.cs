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
    [RequireRoomVolume(8)]                              
    public partial class SmallTableObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Table"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SmallTableItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Misc");                                 
            GetComponent<HousingComponent>().Set(SmallTableItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1500)]
    public partial class SmallTableItem : WorldObjectItem<SmallTableObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Table"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("More of a nightstand than a table, really."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 1,                                   
                                                    TypeForRoomLimit = "Table", 
                                                    DiminishingReturnPercent = 0.7f    
        };}}
    }


    [RequiresSkill(typeof(HewingSkill), 2)]
    public partial class SmallTableRecipe : Recipe
    {
        public SmallTableRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SmallTableItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(HewingSkill), 15, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(HewingSkill), 8, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<GlueItem>(typeof(HewingSkill), 2, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<HewnLogItem>(typeof(HewingSkill), 8, HewingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(SmallTableRecipe), Item.Get<SmallTableItem>().UILink(), 5, typeof(HewingSkill), typeof(HewingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Small Table"), typeof(SmallTableRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}