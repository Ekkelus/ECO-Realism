﻿using Eco.Gameplay.Objects;
using System;
using System.Collections.Generic;
using Eco.Shared.Serialization;
using Eco.Gameplay.Components;
using Eco.World;
using Eco.Shared.Math;
using Eco.Gameplay.Items;

namespace REYmod.Utils
{
    [RequireComponent(typeof(StatusComponent))]
    [Serialized]
    class SpecificGroundComponent : WorldObjectComponent
    {
        private StatusElement status;
        private IEnumerable<Vector3i> positionstocheck;
        private bool ismet = false;
        private BlockItem neededBlockItem;
        private int checkinterval = 100;

        public override bool Enabled { get { return ismet; } }
        public override void Tick()
        {
            checkinterval++;
            if (checkinterval >= 100)
            {
                checkinterval = 0;
                if (CheckGround() != ismet)
                {
                    ismet = !ismet;
                    status.SetStatusMessage(Enabled, "Ground Requirements met" , "Ground Requirements not met!");
                    //ChatManager.ServerMessageToAllAlreadyLocalized(this.Enabled ? "Ground Requirements met" : "Ground Requirements not met!", false);
                }
            }

        }

        public override void Initialize()
        {   }

        public void Initialize(Type blockitem, int grid = 1) //"grid" isnt used yet, it is planned to make it possible to require more than a 3x3 grind (or also only a single block)
        {
            status = Parent.GetComponent<StatusComponent>().CreateStatusElement();
            positionstocheck = World.BlockBelow(Parent.Position3i).XZFullNeighborsAndSelf;
            neededBlockItem = Item.Get(blockitem) as BlockItem;
            ismet = CheckGround();
            status.SetStatusMessage(Enabled, "Ground Requirements met", "Ground Requirements not met!");
        }

        private bool CheckGround()
        {
            bool x = true;
            foreach (Vector3i blockpos in positionstocheck)
            {
                BlockItem blockItem = BlockItem.CreatingItem(World.GetBlock(blockpos).GetType());
                if (blockItem != null)
                {
                    //ChatManager.ServerMessageToAllAlreadyLocalized(blockItem.UILinkAndNumber(1), false);
                    if (blockItem != neededBlockItem)
                    {
                        x = false;
                        //ChatManager.ServerMessageToAllAlreadyLocalized("Not the needed Block! Needs " + neededBlockItem.UILinkAndNumber(1),false);
                    }
                }
                else
                {
                    //ChatManager.ServerMessageToAllAlreadyLocalized("No Blockitem", false);
                    x = false;
                }

            }
            //ChatManager.ServerMessageToAllAlreadyLocalized(x? "Check passed" : "Check NOT passed" , false);
            return x;

        }


    }
}
