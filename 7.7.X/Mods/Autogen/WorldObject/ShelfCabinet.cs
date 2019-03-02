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
	[RequireComponent(typeof(SolidGroundComponent))] 
    [RequireRoomContainment]
    [RequireRoomVolume(8)]                              
    public partial class ShelfCabinetObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Shelf Cabinet"); } } 

        public virtual Type RepresentedItemType { get { return typeof(ShelfCabinetItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Housing");                                 
            this.GetComponent<HousingComponent>().Set(ShelfCabinetItem.HousingVal);
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
    public partial class ShelfCabinetItem : WorldObjectItem<ShelfCabinetObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Shelf Cabinet"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("When a shelf and a cabinet aren't enough individually."); } }

        static ShelfCabinetItem()
        {
            
        }
        
        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Shelves", 
                                                    DiminishingReturnPercent = 0.7f    
        };}}
    }


    [RequiresSkill(typeof(LumberSkill), 3)]
    public partial class ShelfCabinetRecipe : Recipe
    {
        public ShelfCabinetRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ShelfCabinetItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LumberItem>(typeof(LumberSkill), 20, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<BookItem>(typeof(LumberSkill), 2, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<HingeItem>(typeof(LumberSkill), 4, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(LumberSkill), 5, LumberSkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(5, LumberSkill.MultiplicativeStrategy, typeof(LumberSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(ShelfCabinetRecipe), Item.Get<ShelfCabinetItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<ShelfCabinetItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Shelf Cabinet"), typeof(ShelfCabinetRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}