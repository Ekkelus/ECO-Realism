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
    using Eco.Shared.Utils;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerConsumptionComponent))]                     
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                              
    [RequireRoomMaterialTier(1)]        
    public partial class SawmillObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Sawmill"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SawmillItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Crafting");                                 
            this.GetComponent<PowerConsumptionComponent>().Initialize(100);                      
            this.GetComponent<PowerGridComponent>().Initialize(5, new MechanicalPower());        



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(5000)]
    public partial class SawmillItem : WorldObjectItem<SawmillObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Sawmill"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Used to saw wood into lumber."); } }

        static SawmillItem()
        {
            
        }

        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(75))); } }  
    }


    [RequiresSkill(typeof(LumberWoodworkingSkill), 0)]
    public partial class SawmillRecipe : Recipe
    {
        public SawmillRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SawmillItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(LumberWoodworkingEfficiencySkill), 20, LumberWoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(LumberWoodworkingEfficiencySkill), 30, LumberWoodworkingEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(15, LumberWoodworkingSpeedSkill.MultiplicativeStrategy, typeof(LumberWoodworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(SawmillRecipe), Item.Get<SawmillItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<SawmillItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.Do("Sawmill"), typeof(SawmillRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}