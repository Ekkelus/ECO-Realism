namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

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
            this.GetComponent<MinimapComponent>().Initialize("Housing");                                 
            this.GetComponent<HousingComponent>().Set(StuffedElkItem.HousingVal);                                


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

        static StuffedElkItem()
        {
            
        }

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
            this.Products = new CraftingElement[]
            {
                new CraftingElement<StuffedElkItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ElkCarcassItem>(1),
                new CraftingElement<PlantFibersItem>(typeof(TailoringSkill), 100, TailoringSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(TailoringSkill), 8, TailoringSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(StuffedElkRecipe), Item.Get<StuffedElkItem>().UILink(), 40, typeof(TailoringSkill));
            this.Initialize(Localizer.DoStr("Stuffed Elk"), typeof(StuffedElkRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    }
}