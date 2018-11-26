namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(HousingComponent))]                  
    public partial class ElkMountObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Elk Mount"); } } 

        public virtual Type RepresentedItemType { get { return typeof(ElkMountItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Misc");                                 
            this.GetComponent<HousingComponent>().Set(ElkMountItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class ElkMountItem :
        WorldObjectItem<ElkMountObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Elk Mount"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A hunting trophy for your house."); } }

        static ElkMountItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 5,                                   
                                                    TypeForRoomLimit = "Decoration", 
                                                    DiminishingReturnPercent = 0.2f    
        };}}
    }


    [RequiresSkill(typeof(ClothProductionSkill), 3)]
    public partial class ElkMountRecipe : Recipe
    {
        public ElkMountRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ElkMountItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ElkCarcassItem>(1), 
                new CraftingElement<BoardItem>(typeof(ClothProductionEfficiencySkill), 5, ClothProductionEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(ClothProductionEfficiencySkill), 8, ClothProductionEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(15, ClothProductionSpeedSkill.MultiplicativeStrategy, typeof(ClothProductionSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(ElkMountRecipe), Item.Get<ElkMountItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<ElkMountItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Elk Mount", typeof(ElkMountRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    }
}