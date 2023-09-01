using System;
using Sandbox;
using Editor;

namespace XtremeFootball.Teams;

[ClassName( "team_blue" ), HammerEntity]
public partial class BlueTeam : BaseTeam
{
	public override string Name { get; } = "Blue Bulls";
	public override ColorHsv Color { get; } = new( 240, .8f, 1 );

	[Sandbox.GameEvent.Entity.PostSpawn]
	protected static void AutoCreate()
	{
		_ = Away ?? new BlueTeam() { Side = TeamSide.Away };
	}
}
