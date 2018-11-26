//    using Eco.Shared.Utils;
//    using Eco.Shared.View;
//    using Eco.Shared.Items;
//    using Eco.Gameplay.Pipes;
//    using Eco.World.Blocks;


//    [Serialized]
//    public partial class SmallWoodSignItem :
//        WorldObjectItem<SmallWoodSignObject> 
//    {
//        public override LocString DisplayName { get { return Localizer.DoStr("Small Wood Sign"); } } 
//        public override string Description  { get { return  "A small wooden sign for all your small text needs."; } }

//        static SmallWoodSignItem()
//        {

//        }


//    }


//    [RequiresSkill(typeof(WoodworkingSkill), 1)]
//    public partial class SmallWoodSignRecipe : Recipe
//    {
//        public SmallWoodSignRecipe()
//        {
//            this.Products = new CraftingElement[]
//            {
//                new CraftingElement<SmallWoodSignItem>(),
//            };

//            this.Ingredients = new CraftingElement[]
//            {
//                new CraftingElement<BoardItem>(typeof(WoodworkingEfficiencySkill), 8, WoodworkingEfficiencySkill.MultiplicativeStrategy),   
//            };
//            SkillModifiedValue value = new SkillModifiedValue(1, WoodworkingSpeedSkill.MultiplicativeStrategy, typeof(WoodworkingSpeedSkill), Localizer.DoStr("craft time"));
//            SkillModifiedValueManager.AddBenefitForObject(typeof(SmallWoodSignRecipe), Item.Get<SmallWoodSignItem>().UILink(), value);
//            SkillModifiedValueManager.AddSkillBenefit(Item.Get<SmallWoodSignItem>().UILink(), value);
//            this.CraftMinutes = value;
//            this.Initialize("Small Wood Sign", typeof(SmallWoodSignRecipe));
//            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
//        }
//    }
//}