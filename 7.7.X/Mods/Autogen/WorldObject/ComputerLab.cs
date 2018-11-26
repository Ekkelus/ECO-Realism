namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;


    [Serialized]
    [RequireRoomContainment]
    [RequireRoomVolume(45)]
    [RequireRoomMaterialTier(4f)]
    [Weight(5000)]
    public partial class ComputerLabItem : WorldObjectItem<ComputerLabObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Computer Lab"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A place where you can sit all day and play video games! Or work on expert-level research."); } }

        static ComputerLabItem()
        {
            
        }
        
    }


    [RequiresSkill(typeof(IndustrialEngineeringSkill), 4)]
    public partial class ComputerLabRecipe : Recipe
    {
        public ComputerLabRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ComputerLabItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(IndustrialEngineeringEfficiencySkill), 50, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CircuitItem>(typeof(IndustrialEngineeringEfficiencySkill), 30, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<ConcreteItem>(typeof(IndustrialEngineeringEfficiencySkill), 40, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PaperItem>(typeof(IndustrialEngineeringEfficiencySkill), 20, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<RivetItem>(typeof(IndustrialEngineeringEfficiencySkill), 10, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(260, ElectronicEngineeringSpeedSkill.MultiplicativeStrategy, typeof(ElectronicEngineeringSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(ComputerLabRecipe), Item.Get<ComputerLabItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<ComputerLabItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Computer Lab"), typeof(ComputerLabRecipe));
            CraftingComponent.AddRecipe(typeof(ElectronicsAssemblyObject), this);
        }
    }
}