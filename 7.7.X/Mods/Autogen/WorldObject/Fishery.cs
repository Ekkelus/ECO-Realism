namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.DynamicValues;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Property;
    using Gameplay.Skills;
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
    [RequireRoomMaterialTier(0.5f)]        
    public partial class FisheryObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Fishery"); } } 

        public virtual Type RepresentedItemType { get { return typeof(FisheryItem); } } 


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
    public partial class FisheryItem : WorldObjectItem<FisheryObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Fishery"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A place to create fishing poles and traps."); } }
    }


    [RequiresSkill(typeof(HuntingSkill), 1)]
    public partial class FisheryRecipe : Recipe
    {
        public FisheryRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<FisheryItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(HuntingSkill), 20, HuntingSkill.MultiplicativeStrategy),
				new CraftingElement<RopeItem>(typeof(HuntingSkill), 3, HuntingSkill.MultiplicativeStrategy),				
            };
            CraftMinutes = new ConstantValue(1);
            Initialize(Localizer.DoStr("Fishery"), typeof(FisheryRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}