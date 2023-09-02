using System;
using Sandbox;
using Editor;
using XtremeFootball.Teams;

namespace XtremeFootball.SpawnPoints;

[Library( "info_player_blue" )]
[HammerEntity]
public partial class BlueSpawnPoint : BaseSpawnPoint
{
	public override BaseTeam Team => BlueTeam.Current;
}
