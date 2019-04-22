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
    using Gameplay.Systems.Tooltip;
    using Shared.Localization;
    using Shared.Serialization;
    using Shared.Utils;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerConsumptionComponent))]                     
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                              
    [RequireRoomMaterialTier(1)]        
    public partial class SawmillObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Sawmill"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SawmillItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Crafting");                                 
            GetComponent<PowerConsumptionComponent>().Initialize(100);                      
            GetComponent<PowerGridComponent>().Initialize(5, new MechanicalPower());        



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(5000)]
    public partial class SawmillItem : WorldObjectItem<SawmillObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Sawmill"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Used to saw wood into lumber."); } }


        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(75))); } }  
    }


    [RequiresSkill(typeof(LumberSkill), 0)]
    public partial class SawmillRecipe : Recipe
    {
        public SawmillRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SawmillItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(LumberSkill), 20, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(LumberSkill), 30, LumberSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(SawmillRecipe), Item.Get<SawmillItem>().UILink(), 15, typeof(LumberSkill), typeof(LumberFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Sawmill"), typeof(SawmillRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}