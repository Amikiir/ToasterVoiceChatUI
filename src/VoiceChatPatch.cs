using System.Collections.Generic;
using HarmonyLib;

namespace ToasterVoiceChatUI;

public static class VoiceChatPatch
{
    public static List<Player> talkingPlayers = new List<Player>();
    // TODO remove null players from this list when they leave
    
    [HarmonyPatch(typeof(PlayerBodyV2Controller), "Event_OnPlayerVoiceStarted")]
    public static class PlayerBodyV2ControllerEventOnPlayerVoiceStarted
    {
        [HarmonyPostfix]
        public static void Postfix(PlayerBodyV2Controller __instance, Dictionary<string, object> message)
        {
            
            // Plugin.Log.LogInfo($"Event_OnPlayerVoiceStarted");
            // PlayerBodyV2 controllerPlayerBody = __instance.playerBody;
            // if (controllerPlayerBody == null)
            // {
            //     Plugin.Log.LogInfo($"playerBody is null for talking");
            // }
            //
            // Player controllerPlayer = controllerPlayerBody.Player;
            // if (controllerPlayer == null)
            // {
            //     Plugin.Log.LogInfo($"player is null for talking");
            // }
            // Plugin.Log.LogInfo($"Talking player: {controllerPlayer.Username.Value.ToString()}");
            
            // [Info   :Toaster Voice Chat UI] Talking player: Ladouceur
            // [Info   :Toaster Voice Chat UI] message, key player System.String, value Player Player
            // [Info   :Toaster Voice Chat UI] message, key eventName System.String, value Il2CppSystem.Object Il2CppSystem.Object
            // foreach (KeyValuePair<string, Il2CppSystem.Object> kvp in message)
            // {
            //     Plugin.Log.LogInfo($"message, key {kvp.Key}, value {kvp.Value}");
            // }

            Player player = (Player) message["player"];
            // PlayerManager pm = NetworkBehaviourSingleton<PlayerManager>.instance;
            // if (!controllerPlayer.IsLocalPlayer) return; // Only listen to the events firing on our local player
            // if (pm.GetLocalPlayer() != controllerPlayer) return;
            if (!talkingPlayers.Contains(player))
            {
                talkingPlayers.Add(player);
                // Plugin.Log($"Talking player: {player.Username.Value.ToString()}");
                VoiceChatUI.UpdateTalkingUI();
            }
            
            
            // StartedTalking(player.OwnerClientId);
        }
    }
    
    [HarmonyPatch(typeof(PlayerBodyV2Controller), "Event_OnPlayerVoiceStopped")]
    public static class PlayerBodyV2ControllerEventOnPlayerVoiceStopped
    {
        [HarmonyPostfix]
        public static void Postfix(PlayerBodyV2Controller __instance, Dictionary<string, object> message)
        {
            // Plugin.Log.LogInfo($"Event_OnPlayerVoiceStopped");
            // PlayerBodyV2 controllerPlayerBody = __instance.playerBody;
            // if (controllerPlayerBody == null)
            // {
            //     Plugin.Log.LogInfo($"playerBody is null for talking");
            // }
            //
            // Player controllerPlayer = controllerPlayerBody.Player;
            // if (controllerPlayer == null)
            // {
            //     Plugin.Log.LogInfo($"player is null for talking");
            // }
            // Plugin.Log.LogInfo($"Talking player: {controllerPlayer.Username.Value.ToString()}");
            
            // [Info   :Toaster Voice Chat UI] Talking player: Ladouceur
            // [Info   :Toaster Voice Chat UI] message, key player System.String, value Player Player
            // [Info   :Toaster Voice Chat UI] message, key eventName System.String, value Il2CppSystem.Object Il2CppSystem.Object
            // foreach (KeyValuePair<string, Il2CppSystem.Object> kvp in message)
            // {
            //     Plugin.Log.LogInfo($"message, key {kvp.Key}, value {kvp.Value}");
            // }

            Player player = (Player) message["player"];
            // PlayerManager pm = NetworkBehaviourSingleton<PlayerManager>.instance;
            // if (!controllerPlayer.IsLocalPlayer) return; // Only listen to the events firing on our local player
            // if (pm.GetLocalPlayer() != controllerPlayer) return;


            if (talkingPlayers.Contains(player))
            {
                talkingPlayers.Remove(player);
                // Plugin.Log($"Talking no more player: {player.Username.Value.ToString()}");
                VoiceChatUI.UpdateTalkingUI();
            }
        }
    }
}