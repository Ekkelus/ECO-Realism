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
    using REYmod.Core.ChatCommandsNamespace;
    using REYmod.Utils;


    //This InfoBox object demonstrates the use of auto-generated UI for components, check out the attributes assigned to the members and compare to the UI it creates.
    //A InfoBox object will, when triggered (either by a user paying for it or by the owner using it), send an activation to any touching objects, using wires to transmit.  
    //Use /testInfoBoxs to get an example in-game.
    [Serialized]
    [Category("Hidden")]
    public class InfoBoxItem : WorldObjectItem<InfoBoxObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("InfoBox"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Toggle on any touching wires and electronic objects."); } }
    }

    [Serialized]
    [RequireComponent(typeof(InfoBoxComponent))]
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(MustBeOwnedComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    public class InfoBoxObject : WorldObject
    {
        static InfoBoxObject()
        {
            AddOccupancyList(typeof(InfoBoxObject), new BlockOccupancy(Vector3i.Zero, typeof(SolidWorldObjectBlock)));
        }

        protected override void PostInitialize()
        {
            if (this.isFirstInitialization)
            {
                this.GetComponent<PropertyAuthComponent>().SetPublic();
            }
        }

        public override LocString DisplayName { get { return Localizer.DoStr("InfoBox"); } }
    }


    [Serialized, AutogenClass, AutogenName("InfoBox")]
    [Tag("Economy")]
    public class InfoBoxComponent : WorldObjectComponent
    {
        [SyncToView, Autogen, AutoRPC, Serialized, OwnerEditable] public Range X { get; set; }
        [SyncToView, Autogen, AutoRPC, Serialized, OwnerEditable] public float Z { get; set; }

        [Serialized] double worldTimeToReactivate = 0f;

        public InfoBoxComponent()
        {

        }

        [RPC, Autogen, GuestEditable, OwnerEditable, AutogenName("Show Rules")]
        public void ShowRules(Player player)
        {
            ChatCommands.Rules(player.User);
        }

        [RPC, Autogen, GuestEditable, OwnerEditable, AutogenName("Show Top Houses")]
        public void ShowTopHouses(Player player)
        {
            ChatCommands.HouseRanking(player.User);
        }

        [RPC, Autogen, GuestEditable, OwnerEditable, AutogenName("Show Characterinfo")]
        public void ShowAvatar(Player player)
        {
            ChatCommands.Avatarcmd(player.User);
        }

        [RPC, Autogen, GuestEditable, OwnerEditable, AutogenName("Do NOT click here!")]
        public void GetCustomTitle(Player player)
        {
            if (this.Parent.Name.ToLower().Contains("admin"))
            {
                ChatUtils.SendMessage(player, "You can't use the phrase \"admin\" or \"mod\" in your title.");
                return;
            }
            ChatCommands.SetTitle(player.User, player.User, this.Parent.Name);
            this.Parent.Destroy();
        }

        [RPC, Autogen, GuestEditable, OwnerEditable, AutogenName("Do NOT click here!")]
        public void Teleport(Player player)
        {
            if (this.Parent.Name.ToLower().Contains("admin"))
            {
                ChatUtils.SendMessage(player, "You can't use the phrase \"admin\" or \"mod\" in your title.");
                return;
            }
            ChatCommands.SetTitle(player.User, player.User, this.Parent.Name);
            this.Parent.Destroy();
        }






    }
}