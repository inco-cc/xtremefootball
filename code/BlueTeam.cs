using System;
using Sandbox;
using Editor;

namespace XtremeFootball;

[ClassName( "team_blue" )]
[HammerEntity]
public partial class BlueTeam : Team
{
	public override string Name { get; } = "Blue Bulls";
	public override ColorHsv Color { get; } = new( 240, .8f, 1 );

	[GameEvent.Entity.PostSpawn]
	protected static void AutoCreate()
	{
		if ( Away == null )
			_ = new BlueTeam() { Side = TeamSide.Away };
	}
}
