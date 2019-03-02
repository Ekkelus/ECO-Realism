namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
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
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                              
    public partial class MasonryTableObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Masonry Table"); } } 

        public virtual Type RepresentedItemType { get { return typeof(MasonryTableItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Crafting");                                 



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(5000)]
    public partial class MasonryTableItem : WorldObjectItem<MasonryTableObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Masonry Table"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A workstation for hewing and shaping stone into usable objects."); } }

        static MasonryTableItem()
        {
            
        }

    }


    [RequiresSkill(typeof(MortaringSkill), 0)]
    public partial class MasonryTableRecipe : Recipe
    {
        public MasonryTableRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<MasonryTableItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(MortaringSkill), 40, MortaringSkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(MortaringSkill), 10, MortaringSkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(1, MortaringSkill.MultiplicativeStrategy, typeof(MortaringSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(MasonryTableRecipe), Item.Get<MasonryTableItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<MasonryTableItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Masonry Table"), typeof(MasonryTableRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}