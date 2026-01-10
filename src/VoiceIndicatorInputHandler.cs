using UnityEngine;
using UnityEngine.InputSystem;

namespace ToasterVoiceChatUI;

// Handles keyboard input for toggling voice indicator modes
public class VoiceIndicatorInputHandler : MonoBehaviour
{
    private float _lastToggleTime = 0f;
    private const float TOGGLE_COOLDOWN = 0.5f; // Prevent accidental double toggles
    
    void Update()
    {
        try
        {
            var keyboard = Keyboard.current;
            if (keyboard == null) return;
            
            // Toggle between text and image mode with F8 key
            if (keyboard.f8Key.wasPressedThisFrame)
            {
                if (Time.time - _lastToggleTime > TOGGLE_COOLDOWN)
                {
                    _lastToggleTime = Time.time;
                    ToggleIndicatorMode();
                }
            }
            
            // Adjust height with Ctrl + Plus/Minus
            bool ctrlPressed = keyboard.leftCtrlKey.isPressed || keyboard.rightCtrlKey.isPressed;
            
            if (ctrlPressed)
            {
                if (keyboard.equalsKey.wasPressedThisFrame || keyboard.numpadPlusKey.wasPressedThisFrame)
                {
                    AdjustHeight(0.1f);
                }
                else if (keyboard.minusKey.wasPressedThisFrame || keyboard.numpadMinusKey.wasPressedThisFrame)
                {
                    AdjustHeight(-0.1f);
                }
            }
        }
        catch (System.Exception e)
        {
            Plugin.LogError($"Error in VoiceIndicatorInputHandler: {e.Message}");
        }
    }
    
    private void ToggleIndicatorMode()
    {
        VoiceChatSettings.ToggleIndicatorMode();
        
        string mode = VoiceChatSettings.IndicatorMode switch
        {
            VoiceIndicatorMode.None => "Off",
            VoiceIndicatorMode.Text => "Text",
            VoiceIndicatorMode.Image => "Image",
            _ => "Unknown"
        };
        ShowNotification($"Voice indicator: {mode}");
    }
    
    private void AdjustHeight(float delta)
    {
        VoiceChatSettings.IndicatorHeight += delta;
        VoiceChatSettings.IndicatorHeight = Mathf.Clamp(VoiceChatSettings.IndicatorHeight, 1.0f, 10.0f);
        
        ShowNotification($"Indicator height: {VoiceChatSettings.IndicatorHeight:F1}");
        
        // Refresh indicators to apply new height
        if (PlayerActivityIndicator.Instance != null)
        {
            PlayerActivityIndicator.Instance.RefreshVoiceIndicatorState();
        }
    }
    
    private void ShowNotification(string message)
    {
        try
        {
            // Try to show in chat
            var chat = UIChat.Instance;
            if (chat != null)
            {
                chat.AddChatMessage($"<color=orange>[Voice Indicator]</color> {message}");
            }
        }
        catch
        {
            // Chat might not be available
        }
    }
}
