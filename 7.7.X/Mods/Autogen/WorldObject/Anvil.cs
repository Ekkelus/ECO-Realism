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
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                              
    [RequireRoomMaterialTier(1)]        
    public partial class AnvilObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Anvil"); } } 

        public virtual Type RepresentedItemType { get { return typeof(AnvilItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Crafting");                                 
            GetComponent<HousingComponent>().Set(AnvilItem.HousingVal);                                



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(5000)]
    public partial class AnvilItem : WorldObjectItem<AnvilObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Anvil"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A solid shaped piece of metal used to hammer ingots into tools and other useful things."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
    }


    [RequiresSkill(typeof(SmeltingSkill), 1)]
    public partial class AnvilRecipe : Recipe
    {
        public AnvilRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<AnvilItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(AnvilRecipe), Item.Get<AnvilItem>().UILink(), 20, typeof(SmeltingSkill));
            Initialize(Localizer.DoStr("Anvil"), typeof(AnvilRecipe));
            CraftingComponent.AddRecipe(typeof(BloomeryObject), this);
        }
    }
}