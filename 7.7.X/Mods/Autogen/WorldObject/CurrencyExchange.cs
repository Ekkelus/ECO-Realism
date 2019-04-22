namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Property;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;
    using Shared.Serialization;

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
            GetComponent<MinimapComponent>().Initialize("Economy");                                 



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
    }


    [RequiresSkill(typeof(SmeltingSkill), 3)]
    public partial class CurrencyExchangeRecipe : Recipe
    {
        public CurrencyExchangeRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<CurrencyExchangeItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<GoldIngotItem>(typeof(SmeltingSkill), 10, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<BrickItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<LumberItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(CurrencyExchangeRecipe), Item.Get<CurrencyExchangeItem>().UILink(), 30, typeof(SmeltingSkill), typeof(SmeltingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Currency Exchange"), typeof(CurrencyExchangeRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}