namespace Eco.Gameplay.Items
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


    public class CustomInventoryRestriction : IInventoryRestriction
    {
        public string Message { get; set; }
        private Type[] acceptedItemsList;

        public CustomInventoryRestriction(Type[] allowedItemList) // thats some really messy code i guess but yeah, it works
        {
            this.Message = "This inventory does not accept this item!";
            int i = 0;
            acceptedItemsList = new Type[allowedItemList.Rank];

            foreach (Type type in allowedItemList)
            {
                acceptedItemsList[i] = type;
                i++;
            }
        }

        public CustomInventoryRestriction(Type[] allowedItemList, string errorMessage)
            : this(allowedItemList)
        {
            this.Message = errorMessage;
        }



        public int MaxAccepted(Item item, int currentQuantity)
        {
            
            foreach (Type listItem in acceptedItemsList)
            {
                if (listItem == item.Type)
                {
                    return item.MaxStackSize;
                }
            }
            return 0;
        }
    }
}
