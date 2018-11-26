namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(OnOffComponent))]                   
    [RequireComponent(typeof(PropertyAuthComponent))]
    public partial class LargeWindowedLumberDoorObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Windowed Lumber Door"); } } 

        public virtual Type RepresentedItemType { get { return typeof(LargeWindowedLumberDoorItem); } } 


        protected override void Initialize()
        {


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class LargeWindowedLumberDoorItem :
        WorldObjectItem<LargeWindowedLumberDoorObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Windowed Lumber Door"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A large door."); } }

        static LargeWindowedLumberDoorItem()
        {
            
        }

        
    }


    public partial class LargeWindowedLumberDoorRecipe : Recipe
    {
        public LargeWindowedLumberDoorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LargeWindowedLumberDoorItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LumberItem>(typeof(LumberWoodworkingEfficiencySkill), 20, LumberWoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GlassItem>(typeof(LumberWoodworkingEfficiencySkill), 10, LumberWoodworkingEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(30, LumberWoodworkingSpeedSkill.MultiplicativeStrategy, typeof(LumberWoodworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(LargeWindowedLumberDoorRecipe), Item.Get<LargeWindowedLumberDoorItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<LargeWindowedLumberDoorItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.Do("Large Windowed Lumber Door"), typeof(LargeWindowedLumberDoorRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}