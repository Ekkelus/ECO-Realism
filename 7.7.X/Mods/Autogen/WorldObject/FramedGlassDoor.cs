namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

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

        [Tooltip(100)]
        public string TierTooltip()
        {
            return "<i>Tier 4 building material</i>";
        }

        static FramedGlassDoorItem()
        {
            
        }

    }


    [RequiresSkill(typeof(GlassworkingSkill), 1)]
    public partial class FramedGlassDoorRecipe : Recipe
    {
        public FramedGlassDoorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<FramedGlassDoorItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FramedGlassItem>(typeof(GlassProductionEfficiencySkill), 10, GlassProductionEfficiencySkill.MultiplicativeStrategy),   
                new CraftingElement<HingeItem>(typeof(WoodworkingEfficiencySkill), 2, WoodworkingEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(20, GlassProductionSpeedSkill.MultiplicativeStrategy, typeof(GlassProductionSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(FramedGlassDoorRecipe), Item.Get<FramedGlassDoorItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<FramedGlassDoorItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.Do("Framed Glass Door"), typeof(FramedGlassDoorRecipe));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }
}