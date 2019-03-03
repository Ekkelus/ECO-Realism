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
            this.GetComponent<HousingComponent>().Set(TableItem.HousingVal);                                


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

        static TableItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Table", 
                                                    DiminishingReturnPercent = 0.6f    
        };}}
    }


    [RequiresSkill(typeof(WoodworkingSkill), 2)]
    public partial class TableRecipe : Recipe
    {
        public TableRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<TableItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(WoodworkingEfficiencySkill), 10, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(WoodworkingEfficiencySkill), 30, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(WoodworkingEfficiencySkill), 8, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GlueItem>(typeof(WoodworkingEfficiencySkill), 4, WoodworkingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(TableRecipe), Item.Get<TableItem>().UILink(), 5, typeof(WoodworkingSpeedSkill));
            this.Initialize(Localizer.DoStr("Table"), typeof(TableRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}