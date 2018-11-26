namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
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
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class FishTrapObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Fish Trap"); } } 

        public virtual Type RepresentedItemType { get { return typeof(FishTrapItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Economy");                                 



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1000)]
    public partial class FishTrapItem : WorldObjectItem<FishTrapObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Fish Trap"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A trap to catch fish as they swim. "); } }

        static FishTrapItem()
        {
            
        }
        
    }


    [RequiresSkill(typeof(FishingSkill), 3)]
    public partial class FishTrapRecipe : Recipe
    {
        public FishTrapRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<FishTrapItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(FishingSkill), 20, FishingSkill.MultiplicativeStrategy),
                new CraftingElement<RopeItem>(typeof(FishingSkill), 4, FishingSkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(10, FishingSkill.MultiplicativeStrategy, typeof(FishingSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(FishTrapRecipe), Item.Get<FishTrapItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<FishTrapItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.Do("Fish Trap"), typeof(FishTrapRecipe));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
}