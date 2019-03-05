namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Players;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]
    [Weight(100)]                                          
    public partial class YeastItem :
        FoodItem            
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Yeast"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A fungus that acts as an amazing leavening agent."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 0, Protein = 8, Vitamins = 7};
        public override float Calories                          { get { return 60; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedBakingSkill), 2)]    
    public partial class YeastRecipe : Recipe
    {
        public YeastRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<YeastItem>(),
               
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SugarItem>(typeof(AdvancedBakingSkill), 10, AdvancedBakingSkill.MultiplicativeStrategy), 
            };
            CraftMinutes = CreateCraftTimeValue(typeof(YeastRecipe), Item.Get<YeastItem>().UILink(), 5, typeof(AdvancedBakingSkill)); 
            Initialize(Localizer.DoStr("Yeast"), typeof(YeastRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}