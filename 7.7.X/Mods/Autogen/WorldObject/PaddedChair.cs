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
    [RequireRoomVolume(4)]                              
    public partial class PaddedChairObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Padded Chair"); } } 

        public virtual Type RepresentedItemType { get { return typeof(PaddedChairItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Housing");                                 
            this.GetComponent<HousingComponent>().Set(PaddedChairItem.HousingVal);                                



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(3000)]
    public partial class PaddedChairItem : WorldObjectItem<PaddedChairObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Padded Chair"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A comfy chair to rest in."); } }

        static PaddedChairItem()
        {
            
        }
        
        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Seating", 
                                                    DiminishingReturnPercent = 0.8f    
        };}}
    }


    [RequiresSkill(typeof(TailoringSkill), 3)]
    public partial class PaddedChairRecipe : Recipe
    {
        public PaddedChairRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PaddedChairItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(TailoringSkill), 5, TailoringSkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(TailoringSkill), 20, TailoringSkill.MultiplicativeStrategy),
                new CraftingElement<ClothItem>(typeof(TailoringSkill), 10, TailoringSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(TailoringSkill), 6, TailoringSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(PaddedChairRecipe), Item.Get<PaddedChairItem>().UILink(), 5, typeof(TailoringSkill));
            this.Initialize(Localizer.DoStr("Padded Chair"), typeof(PaddedChairRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    }
}