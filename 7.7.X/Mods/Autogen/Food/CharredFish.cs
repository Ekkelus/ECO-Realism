namespace Eco.Mods.TechTree
{
    using Gameplay.Items;
    using Gameplay.Players;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]
    [Weight(500)]                                          
    public partial class CharredFishItem :
        FoodItem            
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Charred Fish"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Charred Fish"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("At least it doesn't have any scales any more."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 4, Protein = 9, Vitamins = 0};
        public override float Calories                          { get { return 550; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    /* [RequiresSkill(typeof(CampfireCookingSkill), 4)]    
    public partial class CharredFishRecipe : Recipe
    {
        public CharredFishRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CharredFishItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<RawFishItem>(typeof(CampfireCookingEfficiencySkill), 3, CampfireCookingEfficiencySkill.MultiplicativeStrategy), 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CharredFishRecipe), Item.Get<CharredFishItem>().UILink(), 3, typeof(CampfireCookingSpeedSkill), typeof(CampfireCookingSpeedFocusedSpeedTalent)); 
            this.Initialize(Localizer.DoStr("Charred Fish"), typeof(CharredFishRecipe));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    } */
}