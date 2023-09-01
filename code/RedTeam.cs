using System;
using Sandbox;
using Editor;

namespace XtremeFootball;

[ClassName( "team_red" )]
[HammerEntity]
public partial class RedTeam : Team
{
	public override string Name { get; } = "Red Rhinos";
	public override ColorHsv Color { get; } = new( 0, .8f, 1 );

	[GameEvent.Entity.PostSpawn]
	protected static void AutoCreate()
	{
		if ( Home == null )
			_ = new RedTeam() { Side = TeamSide.Home };
	}
}
