namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    
    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(HousingComponent))]                          
    [RequireComponent(typeof(RoomRequirementsComponent))]
	[RequireComponent(typeof(SolidGroundComponent))] 
    [RequireRoomContainment]
    [RequireRoomVolume(16)]                              
    [RequireRoomMaterialTier(1, 6)]        
    public partial class WoodenFabricBedObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override string FriendlyName { get { return "Wooden Fabric Bed"; } } 

        public virtual Type RepresentedItemType { get { return typeof(WoodenFabricBedItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Misc");                                 
            this.GetComponent<HousingComponent>().Set(WoodenFabricBedItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(3000)]
    public partial class WoodenFabricBedItem : WorldObjectItem<WoodenFabricBedObject>
    {
        public override string FriendlyName { get { return "Wooden Fabric Bed"; } } 
        public override string Description { get { return "A much more comfortable bed made with fabric."; } }

        static WoodenFabricBedItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Bedroom",
                                                    Val = 4,                                   
                                                    TypeForRoomLimit = "Bed", 
                                                    DiminishingReturnPercent = 0.4f    
        };}}
    }


    [RequiresSkill(typeof(LumberWoodworkingSkill), 4)]
    public partial class WoodenFabricBedRecipe : Recipe
    {
        public WoodenFabricBedRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WoodenFabricBedItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LumberItem>(typeof(LumberWoodworkingEfficiencySkill), 20, LumberWoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<ClothItem>(typeof(LumberWoodworkingEfficiencySkill), 20, LumberWoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(LumberWoodworkingEfficiencySkill), 16, LumberWoodworkingEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(5, LumberWoodworkingSpeedSkill.MultiplicativeStrategy, typeof(LumberWoodworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(WoodenFabricBedRecipe), Item.Get<WoodenFabricBedItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<WoodenFabricBedItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Wooden Fabric Bed", typeof(WoodenFabricBedRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}