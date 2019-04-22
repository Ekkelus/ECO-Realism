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
    using Shared.Math;
    using Shared.Localization;
    using Shared.Serialization;
    using Shared.Utils;
    using Gameplay.Pipes;
    using Gameplay.Wires;
    using System.Timers;

    [Serialized]
    [RequireComponent(typeof(MustBeOwnedComponent))]
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
        IEnumerable<WireConnection> IWireContainer.Wires { get { return outputWire.SingleItemAsEnumerable(); } }
        private Inventory Storage { get { return GetComponent<PublicStorageComponent>().Inventory; } }
        private Inventory LinkedStorage { get { return GetComponent<LinkComponent>().GetSortedLinkedInventories(OwnerUser); } }
        private int pullcounter = 0;

        public override LocString DisplayName { get { return Localizer.DoStr("ConveyorSender"); } } 

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
        {
            outputWire = new WireOutput(this, typeof(PipeBlock), new Ray(0, 0, 0, Direction.Up));                 
            GetComponent<PropertyAuthComponent>().Initialize();
            GetComponent<LinkComponent>().Initialize(2);
            GetComponent<AttachmentComponent>().Initialize();
            GetComponent<PublicStorageComponent>().Initialize(2);
            timer = new Timer(1000);
            timer.AutoReset = true;
            timer.Elapsed += CustomTick;
            timer.Start();



        }

        private void CustomTick(object sender, ElapsedEventArgs e)
        {          
            if (!Enabled) return;
            int mult = 1;
            int unitstosend = 8;
            pullcounter++;  
            if (pullcounter >= 30) // dont pull item into its internal inventory too often to reduce lag
            {
                LinkedStorage.MoveAsManyItemsAsPossible(Storage, OwnerUser);
                pullcounter = 0;
            }
            IEnumerable<ItemStack> nonempty = Storage.NonEmptyStacks;
            nonempty.ForEach(stack =>
            {
                int stackbefore = stack.Quantity;
                if (stack.Item.IsCarried) mult = 5; else mult = 1; // transport less carried items per second than normal ones, maybe restrict based on mass?
                if (unitstosend / mult < 1) return;
                outputWire.SendItemConsume(stack,unitstosend/mult); // thats where the items will be sent over the syste,
                unitstosend -= (stackbefore - stack.Quantity) * mult;
                if (stack.Quantity <= 0)
                {
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
        public override LocString DisplayName { get { return Localizer.DoStr("Conveyor Entry Point"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Entry Point of a itempipe system. (Name not final, im open to suggestions)"); } }

        static ConveyorSenderItem()
        {
            WorldObject.AddOccupancy<ConveyorSenderObject>(new List<BlockOccupancy>(){
            new BlockOccupancy(new Vector3i(0, 0, 0),typeof(PipeSlotBlock)),
            });
        }

       
    }


    [RequiresSkill(typeof(MechanicsSkill), 4)]
    public partial class ConveyorSenderRecipe : Recipe
    {
        public ConveyorSenderRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<ConveyorSenderItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(MechanicsSkill), 10, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<GearboxItem>(typeof(MechanicsSkill), 4, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<PistonItem>(typeof(MechanicsSkill), 2, MechanicsSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(ConveyorSenderRecipe), Item.Get<ConveyorSenderItem>().UILink(), 1, typeof(SmeltingSkill), typeof(SmeltingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Conveyor Entry Point"), typeof(ConveyorSenderRecipe));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }

}