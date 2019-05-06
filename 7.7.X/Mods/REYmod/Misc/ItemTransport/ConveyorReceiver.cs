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
	using Eco.Shared.Localization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    using Eco.Gameplay.Wires;
    using System.Linq;
    using Eco.Core.Utils;
    using System.Timers;
    using REYmod.Utils;

    [Serialized]
    [RequireComponent(typeof(MustBeOwnedComponent))]
    [RequireComponent(typeof(OnOffComponent))]                   
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                 
    [RequireComponent(typeof(PipeComponent))]   
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    public partial class ConveyorReceiverObject : 
        WorldObject, IWireContainer 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("ConveyorReceiver"); } }
        public Timer timer;
        private Inventory Storage { get { return this.GetComponent<PublicStorageComponent>().Inventory; } }
        private Inventory LinkedStorage { get { return this.GetComponent<LinkComponent>().GetSortedLinkedInventories(this.OwnerUser); } }
        private Inventory tmpInventory; 

        public WireInput inputWire;
        IEnumerable<WireConnection> IWireContainer.Wires { get { return this.inputWire.SingleItemAsEnumerable(); } }



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
        {;
            tmpInventory = new AuthorizationInventory(1, this);
            inputWire = WireInput.CreatePipeInput(this, "Input", new Ray(0, 0, 0, Direction.Up), CanReceive, OnReceive);                    
            this.GetComponent<PropertyAuthComponent>().Initialize();
            this.GetComponent<LinkComponent>().Initialize(2);
            this.GetComponent<AttachmentComponent>().Initialize();
            this.GetComponent<PublicStorageComponent>().Initialize(2);
            timer = new Timer(30000); //push items out only once very 30 seconds to reduce craftingqueue spam and lag
            timer.AutoReset = true;
            timer.Elapsed += CustomTick;
            timer.Start();






        }

        private void CustomTick(object sender, ElapsedEventArgs e)
        {
            if (this.Enabled)
            {
                this.Storage.MoveAsManyItemsAsPossible(LinkedStorage, this.OwnerUser);
            }
        }

        private int OnReceive(ItemStack arg)
        {
            if (!this.Enabled) return 0;
            ValResult<int> receivedItems;
                tmpInventory.Clear();
                tmpInventory.AddItems(arg);
                receivedItems = tmpInventory.MoveAsManyItemsAsPossible(Storage, this.OwnerUser);
            if (receivedItems.Success) return receivedItems.Val;
            return 0;
        }

        private int CanReceive(ItemStack arg)
        {
            if (!this.Enabled) return 0;
            if (arg.Item.IsLiquid())
            {
                return 0;
            }
            int canreceive = 0;
            bool firsttry = true;

            while (true)
            {
                if (Storage.Stacks.Any(x => x.Empty))
                {
                    return Math.Min(arg.Item.MaxStackSize, arg.Quantity);
                }                
                IEnumerable<ItemStack> matchingStacks = Storage.Stacks.Where(x => (x.Item == arg.Item) && x.Quantity < x.Item.MaxStackSize);
                matchingStacks.ForEach(x => canreceive += (x.Item.MaxStackSize - x.Quantity));

                if ((canreceive == 0) && firsttry)
                {
                    this.Storage.MoveAsManyItemsAsPossible(LinkedStorage, this.OwnerUser);
                    firsttry = false;
                }
                else return Math.Min(canreceive, arg.Quantity);
            }
            
        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(2000)]
    public partial class ConveyorReceiverItem : WorldObjectItem<ConveyorReceiverObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Conveyor Exit Point"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Exit Point of a itempipe system. (Name not final, im open to suggestions)"); } }

        static ConveyorReceiverItem()
        {
            WorldObject.AddOccupancy<ConveyorReceiverObject>(new List<BlockOccupancy>(){
            new BlockOccupancy(new Vector3i(0, 0, 0),typeof(PipeSlotBlock)),
            });
        }

       
    }

    [RequiresSkill(typeof(MechanicalEngineeringSkill), 4)]
    public partial class ConveyorReceiverRecipe : Recipe
    {
        public ConveyorReceiverRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ConveyorReceiverItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(MechanicsAssemblyEfficiencySkill), 10, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GearboxItem>(typeof(MechanicsAssemblyEfficiencySkill), 4, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PistonItem>(typeof(MechanicsAssemblyEfficiencySkill), 2, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(1, MetalworkingSpeedSkill.MultiplicativeStrategy, typeof(MetalworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(ConveyorReceiverRecipe), Item.Get<ConveyorReceiverItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<ConveyorReceiverItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Conveyor Exit Point", typeof(ConveyorReceiverRecipe));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }

}