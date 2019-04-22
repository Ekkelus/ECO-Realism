namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Housing;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Gameplay.Systems.Tooltip;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(HousingComponent))]                  
    public partial class ElkMountObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Elk Mount"); } } 

        public virtual Type RepresentedItemType { get { return typeof(ElkMountItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Misc");                                 
            GetComponent<HousingComponent>().Set(ElkMountItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class ElkMountItem :
        WorldObjectItem<ElkMountObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Elk Mount"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A hunting trophy for your house."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 5,                                   
                                                    TypeForRoomLimit = "Decoration", 
                                                    DiminishingReturnPercent = 0.2f    
        };}}
    }


    [RequiresSkill(typeof(TailoringSkill), 3)]
    public partial class ElkMountRecipe : Recipe
    {
        public ElkMountRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<ElkMountItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<ElkCarcassItem>(), 
                new CraftingElement<BoardItem>(typeof(TailoringSkill), 5, TailoringSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(TailoringSkill), 8, TailoringSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(ElkMountRecipe), Item.Get<ElkMountItem>().UILink(), 15, typeof(TailoringSkill), typeof(TailoringFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Elk Mount"), typeof(ElkMountRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    }
}