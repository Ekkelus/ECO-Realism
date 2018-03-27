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
    public partial class ToxicWasteBarrelObject : WorldObject
    {
        public override string FriendlyName { get { return "Toxic Waste Barrel"; } }
        private static Type[] itemToStoreList = new Type[]
        {
            typeof(TailingsItem),
        }; 

        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Storage");
			this.GetComponent<PropertyAuthComponent>().Initialize(AuthModeType.Inherited);
			
            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(4);
            storage.Storage.AddRestriction(new CustomInventoryRestriction(itemToStoreList,"You can only store Tailings in that barrel!")); // can't store anything besides tailings


        }

        public override void Destroy()
        {
            base.Destroy();
        }

    }

    [Serialized]
    public partial class ToxicWasteBarrelItem : WorldObjectItem<ToxicWasteBarrelObject>
    {
        public override string FriendlyName { get { return "Toxic Waste Barrel"; } }
        public override string Description { get { return "A container for storing Tailings."; } }

        static ToxicWasteBarrelItem()
        {
            WorldObject.AddOccupancy<ToxicWasteBarrelObject>(new List<BlockOccupancy>(){
            new BlockOccupancy(new Vector3i(0, 0, 0)),
            });
        }

    }


    [RequiresSkill(typeof(MetalworkingSkill), 1)]
    public partial class ToxicWasteBarrelRecipe : Recipe
    {
        public ToxicWasteBarrelRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ToxicWasteBarrelItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(MetalworkingEfficiencySkill), 4, MetalworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CopperIngotItem>(typeof(MetalworkingEfficiencySkill), 2, MetalworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<LumberItem>(typeof(MetalworkingEfficiencySkill), 2, MetalworkingEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(2, BasicCraftingSpeedSkill.MultiplicativeStrategy, typeof(BasicCraftingSpeedSkill), Localizer.Do("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(ToxicWasteBarrelRecipe), Item.Get<ToxicWasteBarrelItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<ToxicWasteBarrelItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Toxic Waste Barrel", typeof(ToxicWasteBarrelRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}