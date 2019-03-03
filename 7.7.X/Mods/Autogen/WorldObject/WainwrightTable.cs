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
            this.GetComponent<MinimapComponent>().Initialize("Crafting");                                 



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

        static WainwrightTableItem()
        {
            
        }

    }


    [RequiresSkill(typeof(HewingSkill), 0)]
    public partial class WainwrightTableRecipe : Recipe
    {
        public WainwrightTableRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WainwrightTableItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(HewingSkill), 50, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(HewingSkill), 40, HewingSkill.MultiplicativeStrategy),   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(WainwrightTableRecipe), Item.Get<WainwrightTableItem>().UILink(), 10, typeof(HewingSkill));
            this.Initialize(Localizer.DoStr("Wainwright Table"), typeof(WainwrightTableRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}