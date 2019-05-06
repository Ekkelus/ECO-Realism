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



    [Serialized]
    public class AutoMinerItem : WorldObjectItem<AutoMinerObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("AutoMiner"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Mines automatically"); } }
    }

    [Serialized]
    [RequireComponent(typeof(AutoMinerComponent))]
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(MustBeOwnedComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]
    [RequireComponent(typeof(LinkComponent))]
    public class AutoMinerObject : WorldObject
    {
        //private int tickcounter = 0;


        static AutoMinerObject()
        {
            AddOccupancyList(typeof(AutoMinerObject), new BlockOccupancy(Vector3i.Zero, typeof(SolidWorldObjectBlock)));
        }

        private static Type[] fuelTypeList = new Type[]
{
            typeof(CoalItem),
            typeof(LogItem),
            typeof(GasolineItem),
};
        private Timer timer;

        public override LocString DisplayName { get { return Localizer.DoStr("AutoMiner"); } }




        protected override void Initialize()
        {
            base.Initialize();
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);
            this.GetComponent<AutoMinerComponent>().Initialize(2,15,30);
            //this.GetComponent<FuelConsumptionComponent>().Initialize(50);

            timer = new Timer(1000);
            timer.AutoReset = true;
            timer.Elapsed += CustomTick;
            timer.Start();

        }


        public void CustomTick(object sender, ElapsedEventArgs e)
        {
            if (!Enabled) return;
            this.GetComponent<AutoMinerComponent>().MineNext();
        }

        public override void Tick() // timer or tickcount? what is better?
        {
            base.Tick();
            //tickcounter++;
            //if (tickcounter < 10) return; // 10 ticks are roughly 1 second
            //tickcounter = 0;
            //if (!Enabled) return;
            //this.GetComponent<AutoMinerComponent>().MineNext();


        }


        // the return value is a bit unclear, it returns true when the block should be skipped and false when a block has been mined or there is a reason to stop mining



        public override void Destroy()
        {
            timer.Stop();
            timer.Dispose();
            base.Destroy();
        }



    }




    [Serialized]
    [Tag("Economy")]
    [RequireComponent(typeof(StatusComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]
    [RequireComponent(typeof(LinkComponent))]
    public class AutoMinerComponent : WorldObjectComponent
    {

        private Inventory LinkedStorage { get { return this.Parent.GetComponent<LinkComponent>().GetSortedLinkedInventories(this.Parent.OwnerUser); } }


        public int R { get; set; }
        public int CurX { get; set; }
        public int CurY { get; set; }
        public int CurZ { get; set; }

        public Vector3i CurPos { get { return new Vector3i(CurX, CurY, CurZ); } }


        private int fuelconsumption;

        private int hitsneeded;
        private int hitsperformed;
        private StatusElement status;
        private bool enabled;

        public override bool Enabled { get { return enabled; } }

        public override void Initialize()
        {
            Initialize(radius:2); //init with defaultvalues
        }

        public void Initialize(int radius = 2,int fuelperhit = 15, int secondsperblock = 30) 
        {
            base.Initialize();
            if(this.status==null) this.status = this.Parent.GetComponent<StatusComponent>().CreateStatusElement();
            enabled = true;
            status.SetStatusMessage(this.Enabled, Localizer.DoStr("Drill OK!"), Localizer.DoStr("Mining has not started yet"));
            Resetpos();

            hitsperformed = 0;
            hitsneeded = secondsperblock;
            fuelconsumption = fuelperhit;
            R = radius;
        }

        public void StopMining(string reason ="Mining operation halted!")
        {
            if (!enabled) return;
            enabled = false;
            status.SetStatusMessage(this.Enabled, Localizer.DoStr("Mining."), Localizer.DoStr(reason));

        }

        public AutoMinerComponent()
        {

        }




        public void Resetpos()
        {
            CurX = Parent.Position3i.X-R;
            CurY = Parent.Position3i.Y-1;
            CurZ = Parent.Position3i.Z-R;

        }

        public void UpdateStatus(string extrainfo="")
        {
            string x = extrainfo;
            x += "Currently Mining: ";
            Block block = World.GetBlock(CurPos);
            string[] tmpstrings;
            string blockname;
            if (block != null)
            {
                tmpstrings = block.GetType().ToString().Split('.');
                blockname = tmpstrings[tmpstrings.Length - 1];
                tmpstrings = blockname.SplitOnCapitals();
                blockname = "";
                foreach (string s in tmpstrings)
                {
                    if (s != "Block") blockname += s + " ";
                }
                x += blockname;
            }
            x += " at " + CurPos.ToString();
            x += " Progress: " + (hitsperformed * 100 / hitsneeded) + "%";
            


            status.SetStatusMessage(this.Enabled, Localizer.DoStr(x));

        }


        public void MineNext()
        {
            while (TryMine(CurPos))
            {
                CurX++;

                int deltaX = CurX - this.Parent.Position3i.X;




                if (deltaX > R)
                {
                    CurX = this.Parent.Position3i.X - R;
                    CurZ++;
                }
                int deltaZ = CurZ - this.Parent.Position3i.Z;
                if (deltaZ > R)
                {
                    CurZ = this.Parent.Position3i.Z - R;
                    CurY--;
                }
            }

        }


        public bool TryMine(Vector3i pos)
        {
            Block block = World.GetBlock(pos);

            if (block is ImpenetrableStoneBlock)
            {
                StopMining("Mining operation halted! Hit bedrock!");
                return false;
            }
            //if (pos.XZ.WrappedDistance(this.Position3i.XZ) >= 20)
            //{
            //    StopMining("Mining operation halted! Too far away");
            //    return false;
            //}



            if (block == null || block is EmptyBlock || block is WorldObjectBlock || block is WaterBlock)
            {
                return true; //skip when worlobject, water or air
            }

            Item blockItem = BlockItem.CreatingItem(block.GetType());
            int amount = 1;


            if (block is IRepresentsItem) 
            {
                //ChatUtils.SendMessageToAll("RepresentsItem");
                blockItem = Item.Create((block as IRepresentsItem).RepresentedItemType);
                if(block.Is<Minable>()) amount = 4; // ore, coal or stone, could also limit to ore and coal by stringcomparison

            }


            if ((block.Is<Diggable>() || block.Is<Minable>()) && blockItem != null)
            {

                if ((this.Parent.GetComponent<FuelSupplyComponent>().Energy + this.Parent.GetComponent<FuelSupplyComponent>().EnergyInSupply) < fuelconsumption)
                {
                    // ChatUtils.SendMessageToAll("Not enough fuel");
                    UpdateStatus("NO FUEL! ");
                    return false;
                }
                if (++hitsperformed < hitsneeded)
                {
                    this.Parent.GetComponent<FuelSupplyComponent>().TryConsumeFuel(fuelconsumption);
                    UpdateStatus();
                    return false;                    
                }
                


                Result result = LinkedStorage.TryAddItems(blockItem.Type, amount);

                if (result.Success)
                {
                    UpdateStatus();
                    this.Parent.GetComponent<FuelSupplyComponent>().TryConsumeFuel(fuelconsumption);
                    World.DeleteBlock(pos);
                    hitsperformed = 0;
                    Resetpos();
                    return false;
                }
                else
                {
                    //ChatUtils.SendMessageToAll("No Space");
                    hitsperformed--;
                    UpdateStatus("STORAGE FULL! ");
                    return false;
                }

            }
            //else ChatUtils.SendMessageToAll("No valid Block");


            return true;
        }


    }
}


