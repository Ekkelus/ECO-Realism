//    using Eco.Shared.Utils;
//    using Eco.Shared.View;
//    using Eco.Shared.Items;
//    using Eco.Gameplay.Pipes;
//    using Eco.World.Blocks;

//    [Serialized]    
//    [RequireComponent(typeof(PropertyAuthComponent))]
//    [RequireComponent(typeof(MinimapComponent))]                
//    [RequireComponent(typeof(LinkComponent))]                   
//    [RequireComponent(typeof(CraftingComponent))]               
//    [RequireComponent(typeof(HousingComponent))]                  
//    [RequireComponent(typeof(SolidGroundComponent))]            
//    [RequireComponent(typeof(RoomRequirementsComponent))]
//    [RequireRoomContainment]
//    [RequireRoomVolume(25)]                              
//    [RequireRoomMaterialTier(2, 18)]        
//    public partial class MachineShopObject : 
//        WorldObject    
//    {
//        public override LocString DisplayName { get { return Localizer.DoStr("Machine Shop"); } } 


//        protected override void Initialize()
//        {
//            this.GetComponent<MinimapComponent>().Initialize("Crafting");                                 
//            this.GetComponent<HousingComponent>().Set(MachineShopItem.HousingVal);                                



//        }

//        public override void Destroy()
//        {
//            base.Destroy();
//        }

//    }

//    [Serialized]
//    [Weight(5000)]
//    public partial class MachineShopItem : WorldObjectItem<MachineShopObject>
//    {
//        public override LocString DisplayName { get { return Localizer.DoStr("Machine Shop"); } } 
//        public override LocString DisplayDescription { get { return Localizer.DoStr("A fancy toolbench that creates equally fancy toys."); } }

//        static MachineShopItem()
//        {

//        }

//        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
//        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
//                                                {
//                                                    Category = "Industrial",
//                                                    TypeForRoomLimit = "", 
//        };}}
//    }


//    [RequiresSkill(typeof(MechanicsSkill), 0)]
//    public partial class MachineShopRecipe : Recipe
//    {
//        public MachineShopRecipe()
//        {
//            this.Products = new CraftingElement[]
//            {
//                new CraftingElement<MachineShopItem>(),
//            };

//            this.Ingredients = new CraftingElement[]
//            {
//                new CraftingElement<LumberItem>(typeof(MechanicsSkill), 20, MechanicsSkill.MultiplicativeStrategy),
//                new CraftingElement<BoardItem>(typeof(MechanicsSkill), 30, MechanicsSkill.MultiplicativeStrategy),
//                new CraftingElement<IronIngotItem>(typeof(MechanicsSkill), 40, MechanicsSkill.MultiplicativeStrategy),   
//            };
//            SkillModifiedValue value = new SkillModifiedValue(120, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsSkill), Localizer.DoStr("craft time"));
//            SkillModifiedValueManager.AddBenefitForObject(typeof(MachineShopRecipe), Item.Get<MachineShopItem>().UILink(), value);
//            SkillModifiedValueManager.AddSkillBenefit(Item.Get<MachineShopItem>().UILink(), value);
//            this.CraftMinutes = value;
//            this.Initialize(Localizer.DoStr("Machine Shop"), typeof(MachineShopRecipe));
//            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
//        }
//    }
//}