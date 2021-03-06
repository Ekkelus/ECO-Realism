﻿using System;
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
    [RequireComponent(typeof(AnimalTrapComponent))]
    public class SmallAnimalTrapObject : WorldObject
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Animal Trap"); } }

        private Type RequiredSkill = typeof(HuntingSkill);
        private int RequiredLevel = 1;
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
            GetComponent<PublicStorageComponent>().Initialize(1);
            GetComponent<PublicStorageComponent>().Storage.AddInvRestriction(new StackLimitRestriction(1));
            GetComponent<PublicStorageComponent>().Storage.AddInvRestriction(new SpecificItemTypesRestriction(allowedItems));
            ThreadSafeAction<User> action = GetComponent<PublicStorageComponent>().Storage.OnChanged;
            action.Add(InventoryChanged);
        }

        private void InventoryChanged(User obj)
        {
            Inventory inventory = GetComponent<PublicStorageComponent>().Storage;
            User owner = OwnerUser;
            User creator = Creator;
            if (owner == null && creator == null) return;
            if (owner == null) owner = creator;
            if (creator == null) creator = owner;


            if(!SkillUtils.UserHasSkill(owner,RequiredSkill,RequiredLevel)&& (!SkillUtils.UserHasSkill(creator, RequiredSkill, RequiredLevel)))
            {
                if ((inventory.TotalNumberOfItems(typeof(RuinedCarcassItem)) == 0) && (!inventory.IsEmpty))
                {
                    InventoryChangeSet changes = new InventoryChangeSet(inventory);
                    changes.Clear();
                    changes.AddItem<RuinedCarcassItem>();
                    Result result = changes.TryApply();
                }
            }
        }

        public override void Destroy()
        {
            base.Destroy();
        }


        protected override void PostInitialize()
        {
            base.PostInitialize();
            GetComponent<AnimalTrapComponent>().Initialize(new List<string>() { "Hare", "Turkey", "Fox" });
        }


    }

    [Serialized]
    [Weight(1000)]
    public partial class SmallAnimalTrapItem : WorldObjectItem<SmallAnimalTrapObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Animal Trap"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A trap to catch small animals as they run around. "); } }

        static SmallAnimalTrapItem()
        {
            WorldObject.AddOccupancy<SmallAnimalTrapObject>(new List<BlockOccupancy>(){
            new BlockOccupancy(new Vector3i(0, 0, 0)),
            });
        }

    }


    [RequiresSkill(typeof(HuntingSkill), 1)]
    public partial class SmallAnimalTrapRecipe : Recipe
    {
        public SmallAnimalTrapRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SmallAnimalTrapItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(20),
            };
            CraftMinutes = new ConstantValue(10);
            Initialize(Localizer.DoStr("Small Animal Trap"), typeof(SmallAnimalTrapRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}
