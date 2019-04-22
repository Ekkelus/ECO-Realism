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
    public partial class BisonMountObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Bison Mount"); } } 

        public virtual Type RepresentedItemType { get { return typeof(BisonMountItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Misc");                                 
            GetComponent<HousingComponent>().Set(BisonMountItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class BisonMountItem :
        WorldObjectItem<BisonMountObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Bison Mount"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A fluffy, but very dead, bison head on a mount."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 5,                                   
                                                    TypeForRoomLimit = "Decoration", 
                                                    DiminishingReturnPercent = 0.2f    
        };}}
    }


    [RequiresSkill(typeof(TailoringSkill), 4)]
    public partial class BisonMountRecipe : Recipe
    {
        public BisonMountRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<BisonMountItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BisonCarcassItem>(), 
                new CraftingElement<BoardItem>(typeof(TailoringSkill), 5, TailoringSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(TailoringSkill), 8, TailoringSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(BisonMountRecipe), Item.Get<BisonMountItem>().UILink(), 15, typeof(TailoringSkill), typeof(TailoringFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Bison Mount"), typeof(BisonMountRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    }
}