using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;

namespace REYmod.Mods.REYmod.Jail
{
    class JailChatCommands : IChatCommandHandler
    {
        [ChatCommand("setjailpos", "", level: ChatAuthorizationLevel.Admin)]
        public static void SetJailPosCommand(User user, int radius = 5)
        { }

        [ChatCommand("jail", "Send a Player to jail", level: ChatAuthorizationLevel.Admin)]
        public static void Jail(User user, string username, int termInMinutes)
        { }

        [ChatCommand("unjail", "Free a player from Jail and teleport him", level: ChatAuthorizationLevel.Admin)]
        public static void UnJail(User user, string username)
        { }
    }
}
