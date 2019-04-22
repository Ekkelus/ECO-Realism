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
    [RequireRoomVolume(25)]                              
    [RequireRoomMaterialTier(0.9f)]        
    public partial class WainwrightTableObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wainwright Table"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WainwrightTableItem); } } 


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
    public partial class WainwrightTableItem : WorldObjectItem<WainwrightTableObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wainwright Table"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A set of smoothing and woodworking tools that assist in creating wheels and transportation."); } }
    }


    [RequiresSkill(typeof(HewingSkill), 0)]
    public partial class WainwrightTableRecipe : Recipe
    {
        public WainwrightTableRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<WainwrightTableItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(HewingSkill), 50, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(HewingSkill), 40, HewingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(WainwrightTableRecipe), Item.Get<WainwrightTableItem>().UILink(), 10, typeof(HewingSkill), typeof(HewingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Wainwright Table"), typeof(WainwrightTableRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}