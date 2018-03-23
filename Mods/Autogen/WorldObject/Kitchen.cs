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
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    
    [Serialized]    
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(HousingComponent))]                          
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                              
    [RequireRoomMaterialTier(2, 18)]        
    public partial class KitchenObject : WorldObject
    {
        public override string FriendlyName { get { return "Kitchen"; } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Cooking");                                 
            this.GetComponent<HousingComponent>().Set(KitchenItem.HousingVal);                                



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class KitchenItem : WorldObjectItem<KitchenObject>
    {
        public override string FriendlyName { get { return "Kitchen"; } } 
        public override string Description { get { return "A prep area for food which allows for more complex dishes."; } }

        static KitchenItem()
        {
            
        }
        
        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Kitchen",
                                                    Val = 1,
                                                    TypeForRoomLimit = "",
                                                    DiminishingReturnPercent = 0.75f
                                                };}}       
    }


    [RequiresSkill(typeof(LumberWoodworkingSkill), 1)]
    public partial class KitchenRecipe : Recipe
    {
        public KitchenRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<KitchenItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(LumberWoodworkingEfficiencySkill), 10, LumberWoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<LumberItem>(typeof(LumberWoodworkingEfficiencySkill), 20, LumberWoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CopperPipeItem>(typeof(LumberWoodworkingEfficiencySkill), 8, LumberWoodworkingEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(120, LumberWoodworkingSpeedSkill.MultiplicativeStrategy, typeof(LumberWoodworkingSpeedSkill), "craft time");
            SkillModifiedValueManager.AddBenefitForObject(typeof(KitchenRecipe), Item.Get<KitchenItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<KitchenItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Kitchen", typeof(KitchenRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}