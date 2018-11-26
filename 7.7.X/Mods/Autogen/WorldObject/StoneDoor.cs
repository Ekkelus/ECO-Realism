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
    public partial class StoneDoorObject : 
        DoorObject, 
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Stone Door"); } } 

        public override Type RepresentedItemType { get { return typeof(StoneDoorItem); } } 


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
    [Weight(2000)]
    public partial class StoneDoorItem :
        WorldObjectItem<StoneDoorObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Stone Door"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A heavy stone door."); } }

        [Tooltip(100)]
        public string TierTooltip()
        {
            return "<i>Tier 1 building material</i>";
        }


        static StoneDoorItem()
        {
            
        }

    }


    [RequiresSkill(typeof(StoneworkingSkill), 2)]
    public partial class StoneDoorRecipe : Recipe
    {
        public StoneDoorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<StoneDoorItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(StoneworkingEfficiencySkill), 40, StoneworkingEfficiencySkill.MultiplicativeStrategy),   
                new CraftingElement<HingeItem>(typeof(WoodworkingEfficiencySkill), 2, WoodworkingEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(3, StoneworkingSpeedSkill.MultiplicativeStrategy, typeof(StoneworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(StoneDoorRecipe), Item.Get<StoneDoorItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<StoneDoorItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.Do("Stone Door"), typeof(StoneDoorRecipe));
            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }
}