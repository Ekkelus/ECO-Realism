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
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(PublicStorageComponent))]                
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(8)]                              
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class IceboxObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Icebox"); } } 

        public virtual Type RepresentedItemType { get { return typeof(IceboxItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Housing");                                 
            this.GetComponent<HousingComponent>().Set(IceboxItem.HousingVal);
            this.GetComponent<PropertyAuthComponent>().Initialize();

            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(8);
            storage.Storage.AddInvRestriction(new NotCarriedRestriction()); // can't store block or large items


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(3000)]
    public partial class IceboxItem : WorldObjectItem<IceboxObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Icebox"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A box of ice. It's in the name!"); } }

        static IceboxItem()
        {
            
        }
        
        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Kitchen",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Food Storage", 
                                                    DiminishingReturnPercent = 0.3f    
        };}}
    }


    [RequiresSkill(typeof(HewingSkill), 4)]
    public partial class IceboxRecipe : Recipe
    {
        public IceboxRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<IceboxItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<HingeItem>(typeof(HewingSkill), 4, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(HewingSkill), 8, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<GlueItem>(typeof(HewingSkill), 2, HewingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(IceboxRecipe), Item.Get<IceboxItem>().UILink(), 5, typeof(HewingSkill));
            this.Initialize(Localizer.DoStr("Icebox"), typeof(IceboxRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}