namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Housing;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Property;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Gameplay.Systems.Tooltip;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                              
    [RequireRoomMaterialTier(2)]        
    public partial class ElectronicsAssemblyObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Electronics Assembly"); } } 

        public virtual Type RepresentedItemType { get { return typeof(ElectronicsAssemblyItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Crafting");                                 
            GetComponent<HousingComponent>().Set(ElectronicsAssemblyItem.HousingVal);                                



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(5000)]
    public partial class ElectronicsAssemblyItem : WorldObjectItem<ElectronicsAssemblyObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Electronics Assembly"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A set of machinery to create electronics."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
    }


    [RequiresSkill(typeof(ElectronicsSkill), 0)]
    public partial class ElectronicsAssemblyRecipe : Recipe
    {
        public ElectronicsAssemblyRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<ElectronicsAssemblyItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(ElectronicsSkill), 20, ElectronicsSkill.MultiplicativeStrategy),
				new CraftingElement<RivetItem>(typeof(ElectronicsSkill), 10, ElectronicsSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(ElectronicsAssemblyRecipe), Item.Get<ElectronicsAssemblyItem>().UILink(), 60, typeof(ElectronicsSkill), typeof(ElectronicsFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Electronics Assembly"), typeof(ElectronicsAssemblyRecipe));
            CraftingComponent.AddRecipe(typeof(ElectricMachinistTableObject), this);
        }
    }
}