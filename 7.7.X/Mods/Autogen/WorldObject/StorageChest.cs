namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

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
            this.GetComponent<MinimapComponent>().Initialize("Storage");                                 
            var storage = this.GetComponent<PublicStorageComponent>();
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

        static StorageChestItem()
        {
            
        }

    }

    [RequiresSkill(typeof(HewingSkill),1)]
    public partial class StorageChestRecipe : Recipe
    {
        public StorageChestRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<StorageChestItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(HewingSkill), 5, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<HingeItem>(typeof(HewingSkill), 2, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<IronIngotItem>(typeof(HewingSkill), 3, HewingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(2); 
            this.Initialize(Localizer.DoStr("Storage Chest"), typeof(StorageChestRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}