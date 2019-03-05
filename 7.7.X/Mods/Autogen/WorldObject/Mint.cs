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
    [RequireRoomMaterialTier(1.2f)]        
    public partial class MintObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mint"); } } 

        public virtual Type RepresentedItemType { get { return typeof(MintItem); } } 


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
    [Weight(10000)]
    public partial class MintItem : WorldObjectItem<MintObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mint"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Allows for the creation of currency."); } }
    }


    [RequiresSkill(typeof(SmeltingSkill), 2)]
    public partial class MintRecipe : Recipe
    {
        public MintRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<MintItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<GoldIngotItem>(typeof(SmeltingSkill), 10, SmeltingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(MintRecipe), Item.Get<MintItem>().UILink(), 60, typeof(SmeltingSkill));
            Initialize(Localizer.DoStr("Mint"), typeof(MintRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}