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
    public partial class StuffedElkObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Stuffed Elk"); } } 

        public virtual Type RepresentedItemType { get { return typeof(StuffedElkItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Housing");                                 
            GetComponent<HousingComponent>().Set(StuffedElkItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class StuffedElkItem :
        WorldObjectItem<StuffedElkObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Stuffed Elk"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("It looks so real!"); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 5,                                   
                                                    TypeForRoomLimit = "Decoration", 
                                                    DiminishingReturnPercent = 0.2f    
        };}}
    }


    [RequiresSkill(typeof(TailoringSkill), 1)]
    public partial class StuffedElkRecipe : Recipe
    {
        public StuffedElkRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<StuffedElkItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<ElkCarcassItem>(),
                new CraftingElement<PlantFibersItem>(typeof(TailoringSkill), 100, TailoringSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(TailoringSkill), 8, TailoringSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(StuffedElkRecipe), Item.Get<StuffedElkItem>().UILink(), 40, typeof(TailoringSkill), typeof(TailoringFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Stuffed Elk"), typeof(StuffedElkRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    }
}