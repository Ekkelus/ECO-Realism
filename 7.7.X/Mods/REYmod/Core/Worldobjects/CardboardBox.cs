namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using System.Collections.Generic;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Math;
    using Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(PublicStorageComponent))]                
    public partial class CardboardBoxObject : WorldObject
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cardboard Box"); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Storage");
			GetComponent<PropertyAuthComponent>().Initialize();

            var storage = GetComponent<PublicStorageComponent>();
            storage.Initialize(10);
            storage.Storage.AddInvRestriction(new NotCarriedRestriction()); // can't store block or large items


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class CardboardBoxItem : WorldObjectItem<CardboardBoxObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cardboard Box"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A container you can store items in."); } }

        static CardboardBoxItem()
        {
            WorldObject.AddOccupancy<CardboardBoxObject>(new List<BlockOccupancy>(){
            new BlockOccupancy(new Vector3i(0, 0, 0)),
            });
        }
        
    }


    [RequiresSkill(typeof(PaperMillingSkill), 1)]
    public partial class CardboardBoxRecipe : Recipe
    {
        public CardboardBoxRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<CardboardBoxItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<PaperItem>(typeof(PaperMillingSkill), 10, PaperMillingSkill.MultiplicativeStrategy),
                new CraftingElement<GlueItem>(typeof(PaperMillingSkill), 1, PaperMillingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(CardboardBoxRecipe), Item.Get<CardboardBoxItem>().UILink(), 2, typeof(PaperMillingSkill));
            Initialize(Localizer.DoStr("Cardboard Box"), typeof(CardboardBoxRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}