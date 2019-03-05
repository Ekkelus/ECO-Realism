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
    [RequireRoomMaterialTier(3)]        
    public partial class LaboratoryObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Laboratory"); } } 

        public virtual Type RepresentedItemType { get { return typeof(LaboratoryItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Cooking");                                 
            GetComponent<PowerConsumptionComponent>().Initialize(250);                      
            GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(5000)]
    public partial class LaboratoryItem : WorldObjectItem<LaboratoryObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Laboratory"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("For researching the science side of cooking. Science rules!"); } }

        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(250))); } }  
    }


    [RequiresSkill(typeof(IndustrySkill), 2)]
    public partial class LaboratoryRecipe : Recipe
    {
        public LaboratoryRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<LaboratoryItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(IndustrySkill), 30, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<GlassJarItem>(typeof(IndustrySkill), 15, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<HingeItem>(typeof(IndustrySkill), 8, IndustrySkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(LaboratoryRecipe), Item.Get<LaboratoryItem>().UILink(), 120, typeof(IndustrySkill));
            Initialize(Localizer.DoStr("Laboratory"), typeof(LaboratoryRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }
}