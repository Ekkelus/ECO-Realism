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
    public partial class CamasPasteItem :
        FoodItem            
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Camas Paste"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Camas Paste"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Pulverized camas works as an excellent thickener or flavour enhancer."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 3, Fat = 10, Protein = 2, Vitamins = 0};
        public override float Calories                          { get { return 60; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(MillingSkill), 2)]    
    public partial class CamasPasteRecipe : Recipe
    {
        public CamasPasteRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<CamasPasteItem>(),
               
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<CamasBulbItem>(typeof(MillingSkill), 10, MillingSkill.MultiplicativeStrategy), 
            };
            CraftMinutes = CreateCraftTimeValue(typeof(CamasPasteRecipe), Item.Get<CamasPasteItem>().UILink(), 5, typeof(MillingSkill)); 
            Initialize(Localizer.DoStr("Camas Paste"), typeof(CamasPasteRecipe));
            CraftingComponent.AddRecipe(typeof(MillObject), this);
        }
    }
}