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
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(45)]                              
    [RequireRoomMaterialTier(1)]        
    public partial class TailoringTableObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tailoring Table"); } } 

        public virtual Type RepresentedItemType { get { return typeof(TailoringTableItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Crafting");                                 



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(5000)]
    public partial class TailoringTableItem : WorldObjectItem<TailoringTableObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tailoring Table"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Spearhead the fashion movement with the finest clothes and hair!"); } }
    }


    [RequiresSkill(typeof(HewingSkill), 1)]
    public partial class TailoringTableRecipe : Recipe
    {
        public TailoringTableRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<TailoringTableItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(HewingSkill), 40, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<PlantFibersItem>(typeof(HewingSkill), 50, HewingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(TailoringTableRecipe), Item.Get<TailoringTableItem>().UILink(), 60, typeof(HewingSkill), typeof(HewingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Tailoring Table"), typeof(TailoringTableRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}