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
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(45)]                              
    [RequireRoomMaterialTier(1.5f)]        
    public partial class CurrencyExchangeObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Currency Exchange"); } } 

        public virtual Type RepresentedItemType { get { return typeof(CurrencyExchangeItem); } } 


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
    [Weight(3000)]
    public partial class CurrencyExchangeItem : WorldObjectItem<CurrencyExchangeObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Currency Exchange"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Allows players to exchange currency."); } }

        static CurrencyExchangeItem()
        {
            
        }

    }


    [RequiresSkill(typeof(SmeltingSkill), 3)]
    public partial class CurrencyExchangeRecipe : Recipe
    {
        public CurrencyExchangeRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CurrencyExchangeItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<GoldIngotItem>(typeof(SmeltingSkill), 10, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<BrickItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<LumberItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(30, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(CurrencyExchangeRecipe), Item.Get<CurrencyExchangeItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<CurrencyExchangeItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Currency Exchange"), typeof(CurrencyExchangeRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}