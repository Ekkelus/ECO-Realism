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
    using Eco.Gameplay.Wires;
    using System.Timers;
    using System.Linq;

    [Serialized]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    public partial class ConveyorSenderObject :
        WorldObject, IWireContainer
    {
        public Timer timer;
        public WireOutput outputWire;
        IEnumerable<WireConnection> IWireContainer.Wires { get { return this.outputWire.SingleItemAsEnumerable(); } }
        private Inventory Storage { get { return this.GetComponent<PublicStorageComponent>().Inventory; } }
        private Inventory LinkedStorage { get { return this.GetComponent<LinkComponent>().GetSortedLinkedInventories(this.OwnerUser); } }


        public override string FriendlyName { get { return "ConveyorSender"; } } 

        private static Type[] fuelTypeList = new Type[]
        {
            typeof(LogItem),
            typeof(LumberItem),
            typeof(CharcoalItem),
            typeof(ArrowItem),
            typeof(BoardItem),
            typeof(CoalItem),
            typeof(WoodPulpItem),
        };

        protected override void Initialize()
        {
            outputWire = new WireOutput(this, typeof(PipeBlock), new Ray(0, 0, 0, Direction.Up), "Output");                 
            this.GetComponent<PropertyAuthComponent>().Initialize(AuthModeType.Inherited);
            this.GetComponent<LinkComponent>().Initialize(2);
            this.GetComponent<AttachmentComponent>().Initialize();
            this.GetComponent<PublicStorageComponent>().Initialize(2);
            timer = new Timer(1000);
            timer.AutoReset = true;
            timer.Elapsed += CustomTick;
            timer.Start();



        }

        private void CustomTick(object sender, ElapsedEventArgs e)
        {          
            if (!this.Enabled) return;
            int mult = 1;
            int unitstosend = 8;


            this.LinkedStorage.MoveAsManyItemsAsPossible(Storage, this.OwnerUser);
            IEnumerable<ItemStack> nonempty = Storage.NonEmptyStacks;
            //outputWire.UpdateStatus();
            nonempty.ForEach(stack =>
            {
                int stackbefore = stack.Quantity;
                if (stack.Item.IsCarried) mult = 5; else mult = 1;
                if (unitstosend / mult < 1) return;
                Console.WriteLine("Try sending items! " + stack.Quantity + " " + stack.Item.FriendlyNamePlural);
                //Console.WriteLine(outputWire.IsDisconnected);
                outputWire.SendItemConsume(stack,unitstosend/mult);
                unitstosend -= (stackbefore - stack.Quantity) * mult;
                Console.WriteLine(stack.Quantity);
                if (stack.Quantity <= 0)
                {
                    Console.WriteLine("Try removing Stack");
                    Storage.AddItem(stack.Item);
                    Storage.RemoveItem(stack.Item.Type);
                }
            });
        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(2000)]
    public partial class ConveyorSenderItem : WorldObjectItem<ConveyorSenderObject>
    {
        public override string FriendlyName { get { return "ConveyorSender"; } } 
        public override string Description  { get { return  "A metal stand which can hold burning fuel to provide light."; } }

        static ConveyorSenderItem()
        {
            WorldObject.AddOccupancy<ConveyorSenderObject>(new List<BlockOccupancy>(){
            new BlockOccupancy(new Vector3i(0, 0, 0),typeof(PipeSlotBlock)),
            });
        }

       
    }


    [RequiresSkill(typeof(MetalworkingSkill), 4)]
    public partial class ConveyorSenderRecipe : Recipe
    {
        public ConveyorSenderRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ConveyorSenderItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(MetalworkingEfficiencySkill), 10, MetalworkingEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(1, MetalworkingSpeedSkill.MultiplicativeStrategy, typeof(MetalworkingSpeedSkill), Localizer.Do("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(ConveyorSenderRecipe), Item.Get<ConveyorSenderItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<ConveyorSenderItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("ConveyorSender", typeof(ConveyorSenderRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}