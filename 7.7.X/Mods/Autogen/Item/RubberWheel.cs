namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;
    using Shared.Serialization;

    [RequiresSkill(typeof(IndustrySkill), 2)]   
    public partial class RubberWheelRecipe : Recipe
    {
        public RubberWheelRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<RubberWheelItem>(),          
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SyntheticRubberItem>(typeof(IndustrySkill), 10, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<IronWheelItem>(),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(RubberWheelRecipe), Item.Get<RubberWheelItem>().UILink(), 5, typeof(IndustrySkill));    
            Initialize(Localizer.DoStr("Rubber Wheel"), typeof(RubberWheelRecipe));

            CraftingComponent.AddRecipe(typeof(WainwrightTableObject), this);
        }
    }


    [Serialized]
    [Weight(2000)]      
    [Currency]              
    public partial class RubberWheelItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Rubber Wheel"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

    }

}