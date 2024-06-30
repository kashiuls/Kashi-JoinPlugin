using Rocket.API;
using Rocket.API.Serialisation;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using UnityEngine;

public class KashiJoinPlugin : RocketPlugin<KashiJoinPluginConfig>
{
    protected override void Load()
    {
        U.Events.OnPlayerConnected += OnPlayerConnected;
        U.Events.OnPlayerDisconnected += OnPlayerDisconnected;
        Rocket.Core.Logging.Logger.Log("KashiJoinPlugin loaded!");
    }

    protected override void Unload()
    {
        U.Events.OnPlayerConnected -= OnPlayerConnected;
        U.Events.OnPlayerDisconnected -= OnPlayerDisconnected;
        Rocket.Core.Logging.Logger.Log("KashiJoinPlugin unloaded!");
    }

    private void OnPlayerConnected(UnturnedPlayer player)
    {
        string joinMessage = $"{player.CharacterName} {Configuration.Instance.JoinMessage}";
        UnturnedChat.Say(joinMessage, Configuration.Instance.JoinMessageColor);
        player.TriggerEffect(Configuration.Instance.JoinEffectId);
    }

    private void OnPlayerDisconnected(UnturnedPlayer player)
    {
        string leaveMessage = $"{player.CharacterName} {Configuration.Instance.LeaveMessage}";
        UnturnedChat.Say(leaveMessage, Configuration.Instance.LeaveMessageColor);
        player.TriggerEffect(Configuration.Instance.LeaveEffectId);
    }
}

public class KashiJoinPluginConfig : IRocketPluginConfiguration
{
    public string JoinMessage;
    public string LeaveMessage;
    public Color JoinMessageColor = Color.blue;
    public Color LeaveMessageColor = Color.red;
    public ushort JoinEffectId = 119;
    public ushort LeaveEffectId = 119;

    public void LoadDefaults()
    {
        JoinMessage = "kişisi sunucuya giriş yaptı.";
        LeaveMessage = "kişisi sunucudan çıkış yaptı.";
    }
}
