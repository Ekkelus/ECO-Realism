namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using REYmod.Utils;

    [Serialized]
    [Weight(100)]                                          
    [Fuel(4000)]
    [AllergyIgnore]
    public partial class OilItem :
        FoodItem            
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Oil"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Oil"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A plant fat extracted for use in cooking."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 15, Protein = 0, Vitamins = 0};
        public override float Calories                          { get { return 120; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(MillProcessingSkill), 2)]    
    public partial class OilRecipe : Recipe
    {
        public OilRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<OilItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CerealGermItem>(typeof(MillProcessingEfficiencySkill), 30, MillProcessingEfficiencySkill.MultiplicativeStrategy), 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(OilRecipe), Item.Get<OilItem>().UILink(), 5, typeof(MillProcessingSpeedSkill)); 
            this.Initialize("Oil", typeof(OilRecipe));
            CraftingComponent.AddRecipe(typeof(MillObject), this);
        }
    }
}