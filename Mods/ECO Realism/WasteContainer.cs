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



    public class TailingsOnlyRestriction : IInventoryRestriction
    {
        public string Message { get; set; }

        public TailingsOnlyRestriction()
        {
            this.Message = "This inventory only accepts Tailings";
        }



        public int MaxAccepted(Item item, int currentQuantity)
        {
            if (item.Type == typeof(TailingsItem))
            {
                return item.MaxStackSize - currentQuantity;
            }
            else return 0;
        }
    }


    [Serialized]
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    public partial class WasteContainerObject : WorldObject
    {
        public override string FriendlyName { get { return "Storage Chest"; } }


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Storage");

            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(10);
            storage.Storage.AddRestriction(new TailingsOnlyRestriction()); // can't store block or large items


        }

        public override void Destroy()
        {
            base.Destroy();
        }

    }

    [Serialized]
    public partial class WasteContainerItem : WorldObjectItem<WasteContainerObject>
    {
        public override string FriendlyName { get { return "Wate Container"; } }
        public override string Description { get { return "A container for storing Tailings."; } }

        static WasteContainerItem()
        {

        }

    }


    [RequiresSkill(typeof(BasicCraftingSkill), 1)]
    public partial class WasteContainerRecipe : Recipe
    {
        public WasteContainerRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WasteContainerItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(BasicCraftingEfficiencySkill), 5, BasicCraftingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<IronIngotItem>(typeof(BasicCraftingEfficiencySkill), 3, BasicCraftingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<HingeItem>(typeof(BasicCraftingEfficiencySkill), 2, BasicCraftingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(BasicCraftingEfficiencySkill), 10, BasicCraftingEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(2, BasicCraftingSpeedSkill.MultiplicativeStrategy, typeof(BasicCraftingSpeedSkill), "craft time");
            SkillModifiedValueManager.AddBenefitForObject(typeof(WasteContainerRecipe), Item.Get<WasteContainerItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<WasteContainerItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Waste Container", typeof(WasteContainerRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}