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
    public partial class CarpentryTableObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Carpentry Table"); } } 

        public virtual Type RepresentedItemType { get { return typeof(CarpentryTableItem); } } 


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
    public partial class CarpentryTableItem : WorldObjectItem<CarpentryTableObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Carpentry Table"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A table for basic wooden crafts for home improvement and progress."); } }
    }


    [RequiresSkill(typeof(HewingSkill), 0)]
    public partial class CarpentryTableRecipe : Recipe
    {
        public CarpentryTableRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<CarpentryTableItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(HewingSkill), 20, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<StoneItem>(typeof(HewingSkill), 20, HewingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(CarpentryTableRecipe), Item.Get<CarpentryTableItem>().UILink(), 1, typeof(HewingSkill), typeof(HewingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Carpentry Table"), typeof(CarpentryTableRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}