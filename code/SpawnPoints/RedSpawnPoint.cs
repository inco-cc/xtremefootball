using System;
using Sandbox;
using Editor;
using XtremeFootball.Teams;

namespace XtremeFootball.SpawnPoints;

[Library( "info_player_red" )]
[HammerEntity]
public partial class RedSpawnPoint : BaseSpawnPoint
{
	public override BaseTeam Team => RedTeam.Current;
}
