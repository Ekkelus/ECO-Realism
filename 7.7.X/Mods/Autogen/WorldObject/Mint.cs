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
    [RequireRoomMaterialTier(1.2f)]        
    public partial class MintObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mint"); } } 

        public virtual Type RepresentedItemType { get { return typeof(MintItem); } } 


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
    [Weight(10000)]
    public partial class MintItem : WorldObjectItem<MintObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mint"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Allows for the creation of currency."); } }

        static MintItem()
        {
            
        }
        
    }


    [RequiresSkill(typeof(MetalworkingSkill), 2)]
    public partial class MintRecipe : Recipe
    {
        public MintRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<MintItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(MetalworkingEfficiencySkill), 20, MetalworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GoldIngotItem>(typeof(MetalworkingEfficiencySkill), 10, MetalworkingEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(60, MetalworkingSpeedSkill.MultiplicativeStrategy, typeof(MetalworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(MintRecipe), Item.Get<MintItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<MintItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Mint", typeof(MintRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}