using System;
using Sandbox;
using Editor;

namespace XtremeFootball.Teams;

[Library( "info_team_red" )]
[HammerEntity]
public partial class RedTeam : BaseTeam
{
	public static RedTeam Current { get; private set; }

	public override string Name { get; } = "Red Rhinos";
	public override ColorHsv Color { get; } = new( 0, .8f, 1 );

	public RedTeam()
	{
		Current?.Delete();

		Current = this;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();

		if ( Current == this )
			Current = null;
	}

	[Sandbox.GameEvent.Entity.PostSpawn]
	protected static void AutoSpawn()
	{
		_ = Current ?? new RedTeam();
	}
}
