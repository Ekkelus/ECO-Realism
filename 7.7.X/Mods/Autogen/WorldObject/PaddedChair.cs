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
    [RequireRoomVolume(4)]                              
    public partial class PaddedChairObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Padded Chair"); } } 

        public virtual Type RepresentedItemType { get { return typeof(PaddedChairItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Housing");                                 
            GetComponent<HousingComponent>().Set(PaddedChairItem.HousingVal);                                



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
            Products = new CraftingElement[]
            {
                new CraftingElement<PaddedChairItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(TailoringSkill), 5, TailoringSkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(TailoringSkill), 20, TailoringSkill.MultiplicativeStrategy),
                new CraftingElement<ClothItem>(typeof(TailoringSkill), 10, TailoringSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(TailoringSkill), 6, TailoringSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(PaddedChairRecipe), Item.Get<PaddedChairItem>().UILink(), 5, typeof(TailoringSkill));
            Initialize(Localizer.DoStr("Padded Chair"), typeof(PaddedChairRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    }
}