namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Players;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;
    using Shared.Serialization;
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

    [RequiresSkill(typeof(MillingSkill), 2)]    
    public partial class OilRecipe : Recipe
    {
        public OilRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<OilItem>(),
               
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<CerealGermItem>(typeof(MillingSkill), 30, MillingSkill.MultiplicativeStrategy), 
            };
            CraftMinutes = CreateCraftTimeValue(typeof(OilRecipe), Item.Get<OilItem>().UILink(), 5, typeof(MillingSkill)); 
            Initialize(Localizer.DoStr("Oil"), typeof(OilRecipe));
            CraftingComponent.AddRecipe(typeof(MillObject), this);
        }
    }
}