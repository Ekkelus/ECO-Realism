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
    [RequireRoomMaterialTier(3)]        
    public partial class LaboratoryObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Laboratory"); } } 

        public virtual Type RepresentedItemType { get { return typeof(LaboratoryItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Cooking");                                 
            this.GetComponent<PowerConsumptionComponent>().Initialize(250);                      
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(5000)]
    public partial class LaboratoryItem : WorldObjectItem<LaboratoryObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Laboratory"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("For researching the science side of cooking. Science rules!"); } }

        static LaboratoryItem()
        {
            
        }
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(250))); } }  
    }


    [RequiresSkill(typeof(IndustrySkill), 2)]
    public partial class LaboratoryRecipe : Recipe
    {
        public LaboratoryRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LaboratoryItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(IndustrySkill), 30, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<GlassJarItem>(typeof(IndustrySkill), 15, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<HingeItem>(typeof(IndustrySkill), 8, IndustrySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(120, IndustrySkill.MultiplicativeStrategy, typeof(IndustrySkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(LaboratoryRecipe), Item.Get<LaboratoryItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<LaboratoryItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Laboratory"), typeof(LaboratoryRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }
}