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
    [RequireComponent(typeof(HousingComponent))]                          
    [RequireComponent(typeof(RoomRequirementsComponent))]
	[RequireComponent(typeof(SolidGroundComponent))] 
    [RequireRoomContainment]
    [RequireRoomVolume(16)]                              
    [RequireRoomMaterialTier(0.8f)]        
    public partial class TableObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Table"); } } 

        public virtual Type RepresentedItemType { get { return typeof(TableItem); } } 


        protected override void Initialize()
        {
            GetComponent<HousingComponent>().Set(TableItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(2000)]
    public partial class TableItem : WorldObjectItem<TableObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Table"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A large table for placing things on."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Table", 
                                                    DiminishingReturnPercent = 0.6f    
        };}}
    }


    [RequiresSkill(typeof(HewingSkill), 2)]
    public partial class TableRecipe : Recipe
    {
        public TableRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<TableItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(HewingSkill), 30, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(HewingSkill), 8, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<GlueItem>(typeof(HewingSkill), 4, HewingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(TableRecipe), Item.Get<TableItem>().UILink(), 5, typeof(HewingSkill));
            Initialize(Localizer.DoStr("Table"), typeof(TableRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}