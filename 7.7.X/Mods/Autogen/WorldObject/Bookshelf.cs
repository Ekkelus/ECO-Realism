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
    [RequireComponent(typeof(HousingComponent))]                          
    [RequireComponent(typeof(PublicStorageComponent))]                
    [RequireComponent(typeof(RoomRequirementsComponent))]
	[RequireComponent(typeof(SolidGroundComponent))] 
    [RequireRoomContainment]
    [RequireRoomVolume(4)]                              
    public partial class BookshelfObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Bookshelf"); } } 

        public virtual Type RepresentedItemType { get { return typeof(BookshelfItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Misc");                                 
            GetComponent<HousingComponent>().Set(BookshelfItem.HousingVal);
            GetComponent<PropertyAuthComponent>().Initialize();

            var storage = GetComponent<PublicStorageComponent>();
            storage.Initialize(8);
            storage.Storage.AddInvRestriction(new NotCarriedRestriction()); // can't store block or large items


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(2000)]
    public partial class BookshelfItem : WorldObjectItem<BookshelfObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Bookshelf"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A place to store knowledge and information; leads to the town hall."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Shelves", 
                                                    DiminishingReturnPercent = 0.7f    
        };}}
    }


    [RequiresSkill(typeof(LumberSkill), 2)]
    public partial class BookshelfRecipe : Recipe
    {
        public BookshelfRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<BookshelfItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LumberItem>(typeof(LumberSkill), 20, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<BookItem>(typeof(LumberSkill), 5, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(LumberSkill), 10, LumberSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(BookshelfRecipe), Item.Get<BookshelfItem>().UILink(), 1, typeof(LumberSkill), typeof(LumberFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Bookshelf"), typeof(BookshelfRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}