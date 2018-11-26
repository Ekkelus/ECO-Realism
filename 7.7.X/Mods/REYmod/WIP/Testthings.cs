using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Minimap;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Gameplay.Pipes.LiquidComponents;
using Eco.Gameplay.Pipes.Gases;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using Eco.Shared.View;
using Eco.Shared.Items;
using Eco.Gameplay.Pipes;
using Eco.World.Blocks;
using REYmod.Utils;
using Eco.Gameplay.Systems.Chat;
using System.Linq;
using Eco.Shared.Networking;
using Eco.Simulation.WorldLayers;
using Eco.World;
using Eco.WorldGenerator;
using Eco.Mods.TechTree;
using System.Timers;
using Eco.Gameplay;
using Eco.Simulation.Time;
using Eco.Shared.Localization;
using REYmod.Blocks;


// everything here is for still in development and is mostly used when i need some ways of testing things im working on and trying some new things
// will send a Warningmessage to all every minute if enabled!



namespace REYmod.TestStuff
{

	[Category("Hidden")]
	public class AnotherInitItem : Item
	{
		static AnotherInitItem()
		{
			GlobalEvents.OneMinuteEvent.Add(WarnDebug);
		}

		private static void WarnDebug()
		{
			ChatManager.ServerMessageToAllAlreadyLocalized("WARNING! Debugfunctions enabled! Please contact an admin about that!", false);
		}
	}


   
	public class ChatCommands : IChatCommandHandler
	{

		[ChatCommand("diamond", "Spawns a diamond above you", level: ChatAuthorizationLevel.Admin)]
		public static void DiamondSpawn(User user)
		{
			Vector3i x = user.Position.Round + new Vector3i(0, 2, 0);
			World.SetBlock(typeof(DiamondBlock), x);
		}


		[ChatCommand("linkhidden", "links an hidden item", level: ChatAuthorizationLevel.Admin)]
		public static void LinkHidden(User user, string item)
		{
			Item x = Item.GetItemByString(user, item);
			if(x!=null) ChatUtils.SendMessage(user, x.UILink());
		}

		[ChatCommand("lawinfo", "gives info about a law", level: ChatAuthorizationLevel.Admin)]
		public static void LawInfo(User user)
		{
			Law law = Legislation.Laws.AllNonFailedLaws.Where(x => x.State == LawState.Voting).First();
			Console.WriteLine(law.Title);
			Console.WriteLine("Zoned? " + law.IsZonedLaw);
			Console.WriteLine("Clauses? " + law.HasClauses);
		 //   Console.WriteLine("Logic? " + law.HasLogic);
			Console.WriteLine("Zonename: " + law.Zone);
			Console.WriteLine("ManagedActionsCount: " + law.ManagedActions.Count());


		}




		[ChatCommand("testcontains", "", level: ChatAuthorizationLevel.Admin)]
		public static void Testcontains(User user,string itemstr,string ingredientstr)
		{
			Item item = Item.Get(itemstr);
			Item ingredient = Item.Get(ingredientstr);
			if(item != null && ingredient != null)
			{
				if (item.HasIngredient(ingredient.Type))
				{
					ChatUtils.SendMessage(user, item.DisplayName + " contains " + ingredient.DisplayName);
				}
				else ChatUtils.SendMessage(user, item.DisplayName + " does not contain " + ingredient.DisplayName);

			}
			else ChatUtils.SendMessage(user, "ERROR");

		}

		[ChatCommand("forcetime", "force advances time", level: ChatAuthorizationLevel.Admin)]
		public static void ForceTime(User user,string seconds)
		{
			WorldTime.ForceAdvanceTime(Convert.ToInt32(seconds));
		}


		[ChatCommand("worldgentest", "test som worldgen functions")]
		public static void worldgentest(User user,float initialspread = 1f,float spreaddecay = 0.95f ,int coresize = 0)
		{
			Vector3 pos = user.Position + new Vector3(0,5,0);
			WorldGenUtils.CreateVein(pos.WorldPosition3i, new DirtBlock(),initialspread: initialspread, spreaddecay: spreaddecay ,coreSize: coresize);


		}

		[ChatCommand("testdebug", "test some stuff")]
		public static void testdebug(User user)
		{
			ChatUtils.SendMessage(user, World.ChunksCount.ToString());
			ChatUtils.SendMessage(user, World.GetDepth(user.Position.Round).ToString());
			ChatUtils.SendMessage(user, ("asdf").Color("green"));
			ChatUtils.SendMessage(user, Text.Color("00FF00", "bla"));
			Legislation.Laws.AllNonFailedLaws.ForEach(x => { ChatUtils.SendMessage(user, x.Title); });

			string teststring = "Hello World!";
			ChatUtils.SendMessage(user, Text.Bold(teststring));
			ChatUtils.SendMessage(user, teststring.Bold());

			Timer timer = new Timer(10000);
			timer.Disposed += (sender, e) => ChatUtils.SendMessage(user, "Timer disposed");
			timer.AutoReset = false;
			timer.Elapsed += (sender, e) => Timer_Elapsed(sender, e, user);
			timer.Start();

		}

		private static void Timer_Elapsed(object sender, ElapsedEventArgs e, User user)
		{
			ChatUtils.SendMessage(user, "Timer elapsed!");
			(sender as Timer).Dispose();
		}

		[ChatCommand("testlink", "test some stuff")]
		public static void testlink(User user)
		{
			user.Player.OpenInfoPanel("agree on something", new Button(x => x.OpenInfoPanel("Test","It worked"),"TITLE","Click to agree","Agree".Color("green"),true).UILink());
		}



	}

}
