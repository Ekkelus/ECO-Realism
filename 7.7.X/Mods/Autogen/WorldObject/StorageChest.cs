namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.DynamicValues;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Skills;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(PublicStorageComponent))]                
    public partial class StorageChestObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Storage Chest"); } } 

        public virtual Type RepresentedItemType { get { return typeof(StorageChestItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Storage");                                 
            var storage = GetComponent<PublicStorageComponent>();
            storage.Initialize(25);
            storage.Storage.AddInvRestriction(new NotCarriedRestriction()); // can't store block or large items


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1500)]
    public partial class StorageChestItem :
        WorldObjectItem<StorageChestObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Storage Chest"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A container you can store items in."); } }
    }

    [RequiresSkill(typeof(HewingSkill),1)]
    public partial class StorageChestRecipe : Recipe
    {
        public StorageChestRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<StorageChestItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(HewingSkill), 5, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<HingeItem>(typeof(HewingSkill), 2, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<IronIngotItem>(typeof(HewingSkill), 3, HewingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = new ConstantValue(2); 
            Initialize(Localizer.DoStr("Storage Chest"), typeof(StorageChestRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}