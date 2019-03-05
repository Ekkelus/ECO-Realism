namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class FramedGlassDoorObject : 
        DoorObject, 
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Framed Glass Door"); } } 

        public override Type RepresentedItemType { get { return typeof(FramedGlassDoorItem); } } 


        protected override void Initialize()
        {
            base.Initialize(); 


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1500)]
    public partial class FramedGlassDoorItem : WorldObjectItem<FramedGlassDoorObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Framed Glass Door"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A beautiful glass door made of steel and glass."); } }
    }


    [RequiresSkill(typeof(GlassworkingSkill), 1)]
    public partial class FramedGlassDoorRecipe : Recipe
    {
        public FramedGlassDoorRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<FramedGlassDoorItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<FramedGlassItem>(typeof(GlassworkingSkill), 10, GlassworkingSkill.MultiplicativeStrategy),   
                new CraftingElement<HingeItem>(typeof(HewingSkill), 2, HewingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(FramedGlassDoorRecipe), Item.Get<FramedGlassDoorItem>().UILink(), 20, typeof(GlassworkingSkill));
            Initialize(Localizer.DoStr("Framed Glass Door"), typeof(FramedGlassDoorRecipe));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }
}