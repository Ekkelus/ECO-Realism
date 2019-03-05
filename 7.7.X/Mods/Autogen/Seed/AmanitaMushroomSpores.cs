namespace Eco.Mods.TechTree
{
	using Gameplay.Components;
	using Gameplay.DynamicValues;
	using Gameplay.Items;
	using Gameplay.Skills;
	using Gameplay.Systems.TextLinks;
	using Shared.Localization;
	using Shared.Serialization;
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


	[RequiresSkill(typeof(FarmingSkill), 1)]
	public class AmanitaMushroomSporesRecipe : Recipe
	{
		public AmanitaMushroomSporesRecipe()
		{
			Products = new CraftingElement[]
			{
				new CraftingElement<AmanitaMushroomSporesItem>(),
			};
			Ingredients = new CraftingElement[]
			{
				new CraftingElement<AmanitaMushroomsItem>(typeof(FarmingSkill), 2, FarmingSkill.MultiplicativeStrategy),
			};
			SkillModifiedValue value = new SkillModifiedValue(0.5f, FarmingSkill.MultiplicativeStrategy, typeof(FarmingSkill), Localizer.DoStr("craft time"));
			SkillModifiedValueManager.AddBenefitForObject(typeof(AmanitaMushroomSporesRecipe), Item.Get<AmanitaMushroomSporesItem>().UILink(), value);
			SkillModifiedValueManager.AddSkillBenefit(Item.Get<AmanitaMushroomSporesItem>().UILink(), value);
			CraftMinutes = value;

			Initialize(Localizer.DoStr("Amanita Mushroom Spores"), typeof(AmanitaMushroomSporesRecipe));
			CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
		}
	}


	[Serialized]
	[Category("Hidden")]
	[Weight(10)]  
	public partial class AmanitaMushroomSporesPackItem : SeedPackItem
	{
	    public override LocString DisplayName { get { return Localizer.DoStr("Amanita Mushroom Spores Pack"); } }
		public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow amanita mushrooms. Not sure why you'd want these poisonous mushrooms, though."); } }
		public override LocString SpeciesName { get { return Localizer.DoStr("AmanitaMushroom"); } }
	}

}