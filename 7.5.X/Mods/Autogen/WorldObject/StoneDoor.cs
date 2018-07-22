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
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class StoneDoorObject : 
        DoorObject 
    {
        public override string FriendlyName { get { return "Stone Door"; } } 


        protected override void Initialize()
        {


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(2000)]
    public partial class StoneDoorItem :
        WorldObjectItem<StoneDoorObject> 
    {
        public override string FriendlyName { get { return "Stone Door"; } } 
        public override string Description  { get { return  "A heavy stone door."; } }

        [Tooltip(100)]
        public string TierTooltip()
        {
            return "<i>Tier 1 building material</i>";
        }


        static StoneDoorItem()
        {
            
        }

    }


    [RequiresSkill(typeof(StoneworkingSkill), 2)]
    public partial class StoneDoorRecipe : Recipe
    {
        public StoneDoorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<StoneDoorItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(StoneworkingEfficiencySkill), 40, StoneworkingEfficiencySkill.MultiplicativeStrategy),   
                new CraftingElement<HingeItem>(typeof(WoodworkingEfficiencySkill), 2, WoodworkingEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(3, StoneworkingSpeedSkill.MultiplicativeStrategy, typeof(StoneworkingSpeedSkill), Localizer.Do("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(StoneDoorRecipe), Item.Get<StoneDoorItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<StoneDoorItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Stone Door", typeof(StoneDoorRecipe));
            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }
}