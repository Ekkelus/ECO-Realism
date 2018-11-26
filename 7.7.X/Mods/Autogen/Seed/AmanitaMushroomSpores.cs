namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Gameplay.Players;
    using System.ComponentModel;

    [Serialized]
	[Weight(10)]  
	public partial class AmanitaMushroomSporesItem : SeedItem
	{
		static AmanitaMushroomSporesItem() { }
		
		private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

		public override LocString DisplayName { get { return Localizer.DoStr("Amanita Mushroom Spores"); } }
		public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow amanita mushrooms. Not sure why you'd want these poisonous mushrooms, though."); } }
		public override LocString SpeciesName { get { return Localizer.DoStr("AmanitaMushroom"); } }

		public override float Calories { get { return 0; } }
		public override Nutrients Nutrition { get { return nutrition; } }
	}


	[RequiresSkill(typeof(SeedProductionSkill), 1)]
	public class AmanitaMushroomSporesRecipe : Recipe
	{
		public AmanitaMushroomSporesRecipe()
		{
			this.Products = new CraftingElement[]
			{
				new CraftingElement<AmanitaMushroomSporesItem>(),
			};
			this.Ingredients = new CraftingElement[]
			{
				new CraftingElement<AmanitaMushroomsItem>(typeof(SeedProductionEfficiencySkill), 2, SeedProductionEfficiencySkill.MultiplicativeStrategy),
			};
			SkillModifiedValue value = new SkillModifiedValue(0.5f, SeedProductionSpeedSkill.MultiplicativeStrategy, typeof(SeedProductionSpeedSkill), Localizer.DoStr("craft time"));
			SkillModifiedValueManager.AddBenefitForObject(typeof(AmanitaMushroomSporesRecipe), Item.Get<AmanitaMushroomSporesItem>().UILink(), value);
			SkillModifiedValueManager.AddSkillBenefit(Item.Get<AmanitaMushroomSporesItem>().UILink(), value);
			this.CraftMinutes = value;

			this.Initialize("Amanita Mushroom Spores", typeof(AmanitaMushroomSporesRecipe));
			CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
		}
	}


	[Serialized]
	[Category("Hidden")]
	[Weight(10)]  
	public partial class AmanitaMushroomSporesPackItem : SeedPackItem
	{
		static AmanitaMushroomSporesPackItem() { }

		public override LocString DisplayName { get { return Localizer.DoStr("Amanita Mushroom Spores Pack"); } }
		public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow amanita mushrooms. Not sure why you'd want these poisonous mushrooms, though."); } }
		public override LocString SpeciesName { get { return Localizer.DoStr("AmanitaMushroom"); } }
	}

}