 namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    
    [Serialized]
    [Weight(50)]                                          
    public partial class RawFishItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Raw Fish"; } }
        public override string Description                      { get { return "A fatty cut of raw fish."; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 7, Protein = 3, Vitamins = 0};
        public override float Calories                          { get { return 200; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }


    public partial class RawSalmonRecipe : Recipe
    {
        public RawSalmonRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RawFishItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SalmonFilletItem>(1),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(RawSalmonRecipe), Item.Get<RawFishItem>().UILink(), 0.01f, typeof(FishCleaningSpeedSkill));
            this.Initialize("Raw Salmon", typeof(RawSalmonRecipe));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }

    public partial class RawTroutRecipe : Recipe
    {
        public RawTroutRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RawFishItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TroutFilletItem>(1),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(RawTroutRecipe), Item.Get<RawFishItem>().UILink(), 0.01f, typeof(FishCleaningSpeedSkill));
            this.Initialize("Raw Trout", typeof(RawTroutRecipe));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }


    public partial class RawTunaRecipe : Recipe
    {
        public RawTunaRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RawFishItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TunaFilletItem>(1),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(RawTunaRecipe), Item.Get<RawFishItem>().UILink(), 0.01f, typeof(FishCleaningSpeedSkill));
            this.Initialize("Raw Tuna", typeof(RawTunaRecipe));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }

} 