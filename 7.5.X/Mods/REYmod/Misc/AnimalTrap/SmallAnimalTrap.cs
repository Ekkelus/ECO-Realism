using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eco.Shared.Localization;
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
using Eco.Mods.TechTree;
using Eco.Core.Utils;
using Eco.Gameplay.Systems.Chat;
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
        public override string FriendlyName { get { return "Small Animal Trap"; } }

        private Type RequiredSkill = typeof(TrapperSkill);
        private int RequiredLevel = 1;
        private Type[] allowedItems = new Type[]
            {
            typeof(HareCarcassItem),
            typeof(TurkeyCarcassItem),
            typeof(FoxCarcassItem),
            typeof(RuinedCarcassItem),
            };
        

        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Economy");
            this.GetComponent<PropertyAuthComponent>().Initialize(AuthModeType.Inherited);
            this.GetComponent<PublicStorageComponent>().Initialize(1);
            this.GetComponent<PublicStorageComponent>().Storage.AddInvRestriction(new StackLimitRestriction(1));
            this.GetComponent<PublicStorageComponent>().Storage.AddInvRestriction(new SpecificItemTypesRestriction(allowedItems));
            ThreadSafeAction<User> action = this.GetComponent<PublicStorageComponent>().Storage.OnChanged;
            action.Add(InventoryChanged);
        }

        private void InventoryChanged(User obj)
        {
            Inventory inventory = this.GetComponent<PublicStorageComponent>().Storage;
            User owner = this.OwnerUser;
            User creator = this.Creator;
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
            this.GetComponent<AnimalTrapComponent>().Initialize(new List<string>() { "Hare", "Turkey", "Fox" });
        }


    }

    [Serialized]
    [Weight(1000)]
    public partial class SmallAnimalTrapItem : WorldObjectItem<SmallAnimalTrapObject>
    {
        public override string FriendlyName { get { return "Small Animal Trap"; } }
        public override string Description { get { return "A trap to catch small animals as they run around. "; } }

        static SmallAnimalTrapItem()
        {
            WorldObject.AddOccupancy<SmallAnimalTrapObject>(new List<BlockOccupancy>(){
            new BlockOccupancy(new Vector3i(0, 0, 0)),
            });
        }

    }


    [RequiresSkill(typeof(TrapperSkill), 1)]
    public partial class SmallAnimalTrapRecipe : Recipe
    {
        public SmallAnimalTrapRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SmallAnimalTrapItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(20),
            };
            this.CraftMinutes = new ConstantValue(10);
            this.Initialize("Small Animal Trap", typeof(SmallAnimalTrapRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}
