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
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                              
    [RequireRoomMaterialTier(1.8f)]        
    public partial class KitchenObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Kitchen"); } } 

        public virtual Type RepresentedItemType { get { return typeof(KitchenItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Cooking");                                 
            this.GetComponent<HousingComponent>().Set(KitchenItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(5000)]
    public partial class KitchenItem : WorldObjectItem<KitchenObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Kitchen"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A prep area for food which allows for more complex dishes."); } }

        static KitchenItem()
        {
            
        }
        
        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Kitchen",
                                                    Val = 3,                                   
                                                    TypeForRoomLimit = "Cooking", 
                                                    DiminishingReturnPercent = 0.3f    
        };}}
    }


    [RequiresSkill(typeof(LumberSkill), 1)]
    public partial class KitchenRecipe : Recipe
    {
        public KitchenRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<KitchenItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(LumberSkill), 10, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<LumberItem>(typeof(LumberSkill), 20, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<CopperPipeItem>(typeof(LumberSkill), 8, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(LumberSkill), 16, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<GlassJarItem>(typeof(LumberSkill), 8, LumberSkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(120, LumberSkill.MultiplicativeStrategy, typeof(LumberSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(KitchenRecipe), Item.Get<KitchenItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<KitchenItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Kitchen"), typeof(KitchenRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}