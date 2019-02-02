using System;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Gameplay.Systems.Tooltip;
using System.Linq;
using Eco.Mods.TechTree;
using Eco.Simulation.Time;
using REYmod.Config;

namespace REYmod.Utils
{
	public class AllergyInitItem : Item
	{
		static AllergyInitItem()
		{
			AllergyUtils.Initialize();
		}
	}


	public class AllergyUtils
	{
		public static void OnPlayerEatFoodAllergy(FoodItem food,User user)
		{
			if (REYmodSettings.Obj.Config.Foodallergiesenabled && user.IsAllergicTo(food))
			{
				ChatUtils.SendMessage(user, "You are allergic to " + food.DisplayName + "!");
				//user.Stomach.UseCalories(food.Calories * 2);
				user.Stomach.Contents.Remove(user.Stomach.Contents.First(x => x.Food == food));
				FoodItem rottenfood = new RottenFoodItem();
				int amountofrotten = (int)(food.Calories / rottenfood.Calories);
				for (int i = 0; i < amountofrotten; i++)
				{
					StomachEntry rottenstomachEntry = new StomachEntry { Food = rottenfood, TimeEaten = WorldTime.Seconds };
					user.Stomach.Contents.Add(rottenstomachEntry);
				}
				//if (WorldTime.Seconds > TimeUtil.DaysToSeconds(1)) // todo: find a better solution for the update (actually.... maybe that one works too on the first day, needs testing (and removal of that if statement)
				//{
					StomachEntry dummystomachEntry = new StomachEntry { Food = rottenfood, TimeEaten = double.MinValue }; // This is a workaround to update the nutritionvalue...
					user.Stomach.Contents.Add(dummystomachEntry); // for some reason that Update method is private...
					user.Stomach.CheckForBowelMovementAndExcreteFeces(user.Player); // unfortunately it wont work on the first day                   
				//}
			}
		}

		public static void Initialize()
		{
			GlobalEvents.OnPlayerEatFood.Add(OnPlayerEatFoodAllergy);
		}
	}

	public static partial class CustomClassExtensions
	{
		#region FoodItem
		[Tooltip(200)]
		public static string MytooltipSection(this FoodItem food, Player player)
		{
			//Console.WriteLine("Requested Tootip");
			if (!REYmodSettings.Obj.Config.Foodallergiesenabled) return null;
			if (player.User.IsAllergicTo(food)) return ("You are allergic to " + food.UILink() + "! (Contains " + player.User.GetAllergyItem().UILink() + ")").Color("red");
			return null;
		}

		#endregion

		#region User
		public static bool IsAllergicTo(this User user, FoodItem food)
		{
			if (user.HasState("allergy") && (user.GetState<string>("allergy") != string.Empty))
			{
				var allergenestring = user.GetState<string>("allergy");
				Item allergeneItem = Item.GetItemByString(user, allergenestring);
				Type allergenetype = allergeneItem.Type;
				if (food.HasIngredient(allergenetype)) return true;
			}
			return false;


		}

		public static Item GetAllergyItem(this User user)
		{
			if (user.HasState("allergy") && (user.GetState<string>("allergy") != string.Empty))
			{
				string allergenestring = user.GetState<string>("allergy");
				return Item.GetItemByString(user, allergenestring);
			}
			return null;
		}

		#endregion
	}
}
