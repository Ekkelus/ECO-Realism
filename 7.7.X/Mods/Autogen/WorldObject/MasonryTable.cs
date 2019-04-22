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
    public partial class MasonryTableObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Masonry Table"); } } 

        public virtual Type RepresentedItemType { get { return typeof(MasonryTableItem); } } 


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
    public partial class MasonryTableItem : WorldObjectItem<MasonryTableObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Masonry Table"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A workstation for hewing and shaping stone into usable objects."); } }
    }


    [RequiresSkill(typeof(MortaringSkill), 0)]
    public partial class MasonryTableRecipe : Recipe
    {
        public MasonryTableRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<MasonryTableItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(MortaringSkill), 40, MortaringSkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(MortaringSkill), 10, MortaringSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(MasonryTableRecipe), Item.Get<MasonryTableItem>().UILink(), 1, typeof(MortaringSkill), typeof(MortaringFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Masonry Table"), typeof(MasonryTableRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}