using System;
using System.Collections.Generic;
using Eco.Shared.Localization;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Mods.TechTree;
using Eco.Core.Utils;
using REYmod.Utils;

namespace REYmod.Mods.ECO_Realism.Worldobjects
{
    [Serialized]
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    public class MailboxObject : WorldObject
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Animal Trap"); } }


        private Type[] allowedItems = new[]
            {
            typeof(HareCarcassItem),
            typeof(TurkeyCarcassItem),
            typeof(FoxCarcassItem),
            typeof(RuinedCarcassItem),
            };
        

        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Economy");
            GetComponent<PropertyAuthComponent>().Initialize();




        }




        public override void Destroy()
        {
            base.Destroy();
        }


        protected override void PostInitialize()
        {
            base.PostInitialize();
        }


    }

    [Serialized]
    [Weight(1000)]
    public partial class MailboxItem : WorldObjectItem<MailboxObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Animal Trap"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A trap to catch small animals as they run around. "); } }

        static MailboxItem()
        {
            WorldObject.AddOccupancy<MailboxObject>(new List<BlockOccupancy>(){
            new BlockOccupancy(new Vector3i(0, 0, 0)),
            });
        }

    }



    public partial class MailboxRecipe : Recipe
    {
        public MailboxRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<MailboxItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(20),
            };
            CraftMinutes = new ConstantValue(10);
            Initialize(Localizer.DoStr("Small Animal Trap"), typeof(MailboxRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}
