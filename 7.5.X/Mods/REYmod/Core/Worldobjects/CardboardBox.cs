namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Shared.Localization;
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
    [RequireComponent(typeof(PublicStorageComponent))]                
    public partial class CardboardBoxObject : WorldObject
    {
        public override string FriendlyName { get { return "Cardboard Box"; } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Storage");
			this.GetComponent<PropertyAuthComponent>().Initialize(AuthModeType.Inherited);

            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(10);
            storage.Storage.AddInvRestriction(new NotCarriedRestriction()); // can't store block or large items


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class CardboardBoxItem : WorldObjectItem<CardboardBoxObject>
    {
        public override string FriendlyName { get { return "Cardboard Box"; } } 
        public override string Description { get { return "A container you can store items in."; } }

        static CardboardBoxItem()
        {
            WorldObject.AddOccupancy<CardboardBoxObject>(new List<BlockOccupancy>(){
            new BlockOccupancy(new Vector3i(0, 0, 0)),
            });
        }
        
    }


    [RequiresSkill(typeof(PaperSkill), 1)]
    public partial class CardboardBoxRecipe : Recipe
    {
        public CardboardBoxRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CardboardBoxItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PaperItem>(typeof(PaperEfficiencySkill), 10, PaperEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GlueItem>(typeof(PaperEfficiencySkill), 1, PaperEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(2, PaperSpeedSkill.MultiplicativeStrategy, typeof(PaperSpeedSkill), Localizer.Do("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(CardboardBoxRecipe), Item.Get<CardboardBoxItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<CardboardBoxItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Cardboard Box", typeof(CardboardBoxRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}