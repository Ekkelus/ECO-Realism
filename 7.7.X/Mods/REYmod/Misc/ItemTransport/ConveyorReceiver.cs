namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Gameplay.Pipes.LiquidComponents;
    using Shared.Math;
    using Shared.Localization;
    using Shared.Serialization;
    using Shared.Utils;
    using Gameplay.Wires;
    using System.Linq;
    using Core.Utils;
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
        private Inventory Storage { get { return GetComponent<PublicStorageComponent>().Inventory; } }
        private Inventory LinkedStorage { get { return GetComponent<LinkComponent>().GetSortedLinkedInventories(OwnerUser); } }
        private Inventory tmpInventory; 

        public WireInput inputWire;
        IEnumerable<WireConnection> IWireContainer.Wires { get { return inputWire.SingleItemAsEnumerable(); } }



        private static Type[] fuelTypeList = new[]
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
            GetComponent<PropertyAuthComponent>().Initialize();
            GetComponent<LinkComponent>().Initialize(2);
            GetComponent<AttachmentComponent>().Initialize();
            GetComponent<PublicStorageComponent>().Initialize(2);
            timer = new Timer(30000); //push items out only once very 30 seconds to reduce craftingqueue spam and lag
            timer.AutoReset = true;
            timer.Elapsed += CustomTick;
            timer.Start();






        }

        private void CustomTick(object sender, ElapsedEventArgs e)
        {
            if (Enabled)
            {
                Storage.MoveAsManyItemsAsPossible(LinkedStorage, OwnerUser);
            }
        }

        private int OnReceive(ItemStack arg)
        {
            if (!Enabled) return 0;
            ValResult<int> receivedItems;
                tmpInventory.Clear();
                tmpInventory.AddItems(arg);
                receivedItems = tmpInventory.MoveAsManyItemsAsPossible(Storage, OwnerUser);
            if (receivedItems.Success) return receivedItems.Val;
            return 0;
        }

        private int CanReceive(Type arg)
        {
            if (!Enabled) return 0;

            Item item = (Item)Activator.CreateInstance(arg);

            if (item.IsLiquid())
            {
                return 0;
            }
            int canreceive = 0;
            bool firsttry = true;

            




            while (true)
            {
                if (Storage.Stacks.Any(x => x.Empty))
                {
                    ChatUtils.SendMessageToAll("CANRECEIVE " + item.Name(1) +": EmptyStack! return " + item.MaxStackSize);
                    return item.MaxStackSize;
                  //  return Math.Min(arg.Item.MaxStackSize, arg.Quantity);
                }                
                IEnumerable<ItemStack> matchingStacks = Storage.Stacks.Where(x => (x.Item.Type == arg) && x.Quantity < x.Item.MaxStackSize);
                matchingStacks.ForEach(x => canreceive += (x.Item.MaxStackSize - x.Quantity));

                if ((canreceive == 0) && firsttry)
                {
                    Storage.MoveAsManyItemsAsPossible(LinkedStorage, OwnerUser);
                    firsttry = false;
                }
                else
                {
                    ChatUtils.SendMessageToAll("CANRECEIVE " + item.Name(1) + ": return " + canreceive);
                    return canreceive;
                }
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

    [RequiresSkill(typeof(MechanicsSkill), 4)]
    public partial class ConveyorReceiverRecipe : Recipe
    {
        public ConveyorReceiverRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<ConveyorReceiverItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(MechanicsSkill), 10, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<GearboxItem>(typeof(MechanicsSkill), 4, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<PistonItem>(typeof(MechanicsSkill), 2, MechanicsSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(ConveyorReceiverRecipe), Item.Get<ConveyorReceiverItem>().UILink(), 1, typeof(SmeltingSkill), typeof(SmeltingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Conveyor Exit Point"), typeof(ConveyorReceiverRecipe));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }

}