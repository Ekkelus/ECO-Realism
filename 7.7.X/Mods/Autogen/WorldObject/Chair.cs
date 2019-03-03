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
    [RequireRoomVolume(8)]                              
    [RequireRoomMaterialTier(0.8f)]        
    public partial class ChairObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Chair"); } } 

        public virtual Type RepresentedItemType { get { return typeof(ChairItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<HousingComponent>().Set(ChairItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1000)]
    public partial class ChairItem : WorldObjectItem<ChairObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Chair"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A raised surface supported by legs. Without the back, it might be a stool."); } }

        static ChairItem()
        {
            
        }
        
        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 1f,
                                                    TypeForRoomLimit = "Seating",
                                                    DiminishingReturnPercent = 0.9f
                                                };}}       
    }


    [RequiresSkill(typeof(WoodworkingSkill), 2)]
    public partial class ChairRecipe : Recipe
    {
        public ChairRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ChairItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(WoodworkingEfficiencySkill), 10, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(WoodworkingEfficiencySkill), 20, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PlantFibersItem>(typeof(WoodworkingEfficiencySkill), 20, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GlueItem>(typeof(WoodworkingEfficiencySkill), 2, WoodworkingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ChairRecipe), Item.Get<ChairItem>().UILink(), 5, typeof(WoodworkingSpeedSkill));
            this.Initialize(Localizer.DoStr("Chair"), typeof(ChairRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}