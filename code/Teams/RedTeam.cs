using System;
using Sandbox;
using Editor;

namespace XtremeFootball.Teams;

[ClassName( "team_red" ), HammerEntity]
public partial class RedTeam : BaseTeam
{
	public override string Name { get; } = "Red Rhinos";
	public override ColorHsv Color { get; } = new( 0, .8f, 1 );

	[Sandbox.GameEvent.Entity.PostSpawn]
	protected static void AutoCreate()
	{
		_ = Home ?? new RedTeam() { Side = TeamSide.Home };
	}
}
