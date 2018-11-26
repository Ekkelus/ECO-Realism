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
    [RequireRoomMaterialTier(0.9f)]        
    public partial class WainwrightTableObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wainwright Table"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WainwrightTableItem); } } 


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
    public partial class WainwrightTableItem : WorldObjectItem<WainwrightTableObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wainwright Table"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A set of smoothing and woodworking tools that assist in creating wheels and transportation."); } }

        static WainwrightTableItem()
        {
            
        }

    }


    [RequiresSkill(typeof(WoodworkingSkill), 0)]
    public partial class WainwrightTableRecipe : Recipe
    {
        public WainwrightTableRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WainwrightTableItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(WoodworkingEfficiencySkill), 50, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(WoodworkingEfficiencySkill), 40, WoodworkingEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(10, WoodworkingSpeedSkill.MultiplicativeStrategy, typeof(WoodworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(WainwrightTableRecipe), Item.Get<WainwrightTableItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<WainwrightTableItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.Do("Wainwright Table"), typeof(WainwrightTableRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}