namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Serialization;

    [RequiresSkill(typeof(BasicEngineeringSkill), 1)]   
    public partial class RoadToolRecipe : Recipe
    {
        public RoadToolRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RoadToolItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(BasicEngineeringSkill), 6, BasicEngineeringSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(5);
            this.Initialize(Localizer.DoStr("Road Tool"), typeof(RoadToolRecipe));

            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }


    [Serialized]
    [Weight(1000)]      
    [Currency]              
    public partial class RoadToolItem :
    ToolItem                        
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Road Tool"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Used to press roads into dirt and stone rubble."); } }

    }

}