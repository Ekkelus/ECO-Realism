// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.




namespace REYmod.Core
{
    using Eco.Core.Controller;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Shared.Math;
    using Eco.Shared.Networking;
    using Eco.Shared.Serialization;
    using Eco.Shared.Localization;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Systems;
    using System.ComponentModel;
    using REYmod.Utils;
    using Eco.Mods.TechTree;
    using System;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Shared.Utils;
    using Eco.Core.Utils;
    using System.Timers;


    //This AutoMiner object demonstrates the use of auto-generated UI for components, check out the attributes assigned to the members and compare to the UI it creates.
    //A AutoMiner object will, when triggered (either by a user paying for it or by the owner using it), send an activation to any touching objects, using wires to transmit.  
    //Use /testAutoMiners to get an example in-game.
    [Serialized]
    [Category("Hidden")]
    public class AutoMinerItem : WorldObjectItem<AutoMinerObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("AutoMiner"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Toggle on any touching wires and electronic objects."); } }
    }

    [Serialized]
    [RequireComponent(typeof(AutoMinerSettingsComponent))]
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(MustBeOwnedComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]
    [RequireComponent(typeof(LinkComponent))]
    //[RequireComponent(typeof(FuelConsumptionComponent))]
    public class AutoMinerObject : WorldObject
    {
        private Inventory LinkedStorage { get { return this.GetComponent<LinkComponent>().GetSortedLinkedInventories(this.OwnerUser); } }
        private Timer timer;


        static AutoMinerObject()
        {
            AddOccupancyList(typeof(AutoMinerObject), new BlockOccupancy(Vector3i.Zero, typeof(SolidWorldObjectBlock)));
        }

        private static Type[] fuelTypeList = new Type[]
{
            typeof(CoalItem),
            typeof(ArrowItem),
};


        public override LocString DisplayName { get { return Localizer.DoStr("AutoMiner"); } }

        public bool UpdateEnabled()
        {
            foreach (WorldObjectComponent component in this.GetComponents<WorldObjectComponent>())
            {
                if(!component.Enabled)
                { this.enabled = false;
                    return false;
                }
            }
            this.enabled = true;
            return true;
        }


        protected override void Initialize()
        {
            base.Initialize();
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);
            this.GetComponent<AutoMinerSettingsComponent>().Initialize();
            //this.GetComponent<FuelConsumptionComponent>().Initialize(50);


            timer = new Timer(1000); 
            timer.AutoReset = true;
            timer.Elapsed += CustomTick;
            timer.Start();
        }

        private void CustomTick(object sender, ElapsedEventArgs e)
        {
            if (!UpdateEnabled()) return;

            //ChatUtils.SendMessageToAll(this.GetComponent<FuelSupplyComponent>().Energy + " + " + this.GetComponent<FuelSupplyComponent>().EnergyInSupply);


            AutoMinerSettingsComponent autoMiner = this.GetComponent<AutoMinerSettingsComponent>();
            while (TryMine(new Vector3i(autoMiner.intX, autoMiner.intY, autoMiner.intZ)))
            {
                autoMiner.CurX++;

                int deltaX = autoMiner.intX - this.Position3i.X;
                int radius = (int)Math.Floor(autoMiner.R);



                if (deltaX > radius)
                {
                    autoMiner.CurX = this.Position3i.X - radius;
                    autoMiner.CurZ++;
                }
                int deltaZ = autoMiner.intZ - this.Position3i.Z;
                if (deltaZ > radius)
                {
                    autoMiner.CurZ = this.Position3i.Z - radius;
                    autoMiner.CurY--;
                }
            }

        }


        // the return value is a bit unclear, it returns true when the block should be skipped and false when a block has been mined or there is a reason to stop mining
        public bool TryMine(Vector3i pos)
        {
            Block block = World.GetBlock(pos);

            if (block is ImpenetrableStoneBlock)
            {
                GetComponent<AutoMinerSettingsComponent>().StopMining("Mining operation halted! Hit bedrock!");
                return false;
            }
            if (pos.XZ.WrappedDistance(this.Position3i.XZ) >= 20)
            {
                GetComponent<AutoMinerSettingsComponent>().StopMining("Mining operation halted! Too far away");
                return false;
            }

            

            if (block == null || block is WorldObjectBlock)
            {

                return true;
            }

            Item blockItem = BlockItem.CreatingItem(block.GetType());
            int amount = 1;


            if (block is IRepresentsItem)
            {
                //ChatUtils.SendMessageToAll("RepresentsItem");
                blockItem = Item.Create((block as IRepresentsItem).RepresentedItemType);
                amount = 3;
            }


            if ((block.Is<Diggable>() || block.Is<Minable>()) && blockItem != null)
            {
                if ((this.GetComponent<FuelSupplyComponent>().Energy + this.GetComponent<FuelSupplyComponent>().EnergyInSupply) < 1000)
                {
                   // ChatUtils.SendMessageToAll("Not enough fuel");
                    return false;
                }
                Result result = LinkedStorage.TryAddItems(blockItem.Type, amount);

                    if (result.Success)
                    {
                        this.GetComponent<FuelSupplyComponent>().TryConsumeFuel(1000);
                        World.DeleteBlock(pos);
                        return false;
                    }
                    else
                    {
                        //ChatUtils.SendMessageToAll("No Space");
                        return false;
                    }
            }
            //else ChatUtils.SendMessageToAll("No valid Block");


            return true;
        }


        public override void Destroy()
        {
            timer.Stop();
            timer.Dispose();
            base.Destroy();
        }



    }




    [Serialized, AutogenClass, ViewDisplayName("AutoMiner")]
    [Tag("Economy")]
    [RequireComponent(typeof(StatusComponent))]
    public class AutoMinerSettingsComponent : WorldObjectComponent
    {
        [SyncToView, Autogen, AutoRPC, Serialized, ViewDisplayName("Radius")] public float R { get; set; }

        [SyncToView, Autogen, AutoRPC,OwnerReadOnly, GuestHidden, Serialized, ViewDisplayName("X")] public float CurX { get; set; }
        [SyncToView, Autogen, AutoRPC, OwnerReadOnly, GuestHidden, Serialized, ViewDisplayName("Y")] public float CurY { get; set; }
        [SyncToView, Autogen, AutoRPC, OwnerReadOnly, GuestHidden, Serialized, ViewDisplayName("Z")] public float CurZ { get; set; }


        public int intX { get { return (int)Math.Floor(CurX); } }
        public int intY { get { return (int)Math.Floor(CurY); } }
        public int intZ { get { return (int)Math.Floor(CurZ); } }

        private StatusElement status;
        private bool enabled;

        public override bool Enabled { get { return enabled; } }

        public override void Initialize()
        {
            //base.Initialize();
            if(this.status==null) this.status = this.Parent.GetComponent<StatusComponent>().CreateStatusElement();
            enabled = false;
            status.SetStatusMessage(this.Enabled, Localizer.DoStr("Mining."), Localizer.DoStr("Mining has not started yet"));
            Resetpos(null);
        }

        public void StopMining(string reason ="Mining operation halted!")
        {
            if (!enabled) return;
            enabled = false;
            status.SetStatusMessage(this.Enabled, Localizer.DoStr("Mining."), Localizer.DoStr(reason));

        }

        public AutoMinerSettingsComponent()
        {

        }

        [RPC, Autogen]
        public void StartMining(Player player)
        {
            this.enabled = true;
            status.SetStatusMessage(this.Enabled, Localizer.DoStr("Mining."), Localizer.DoStr("Mining has not started yet"));
            //ChatUtils.SendMessageToAll("AUTOMINER: " + this.Parent.Enabled);
            foreach (WorldObjectComponent component in this.Parent.GetComponents<WorldObjectComponent>())
            {
                //ChatUtils.SendMessageToAll(component.GetType() + "     " + component.Enabled);
            }
        }

        [RPC, Autogen, GuestEditable]
        public void Resetpos(Player player)
        {
            CurX = Parent.Position3i.X-R;
            CurY = Parent.Position3i.Y-1;
            CurZ = Parent.Position3i.Z-R;
            //if (player != null) player.RPC("YellTimber");
            if (player != null) ChatUtils.SendMessage(player, "Mining started");
        }



    }
}


