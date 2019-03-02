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
    public partial class BisonMountObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Bison Mount"); } } 

        public virtual Type RepresentedItemType { get { return typeof(BisonMountItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Misc");                                 
            this.GetComponent<HousingComponent>().Set(BisonMountItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class BisonMountItem :
        WorldObjectItem<BisonMountObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Bison Mount"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A fluffy, but very dead, bison head on a mount."); } }

        static BisonMountItem()
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


    [RequiresSkill(typeof(TailoringSkill), 4)]
    public partial class BisonMountRecipe : Recipe
    {
        public BisonMountRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BisonMountItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BisonCarcassItem>(1), 
                new CraftingElement<BoardItem>(typeof(TailoringSkill), 5, TailoringSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(TailoringSkill), 8, TailoringSkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(15, TailoringSkill.MultiplicativeStrategy, typeof(TailoringSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(BisonMountRecipe), Item.Get<BisonMountItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<BisonMountItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Bison Mount"), typeof(BisonMountRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    }
}