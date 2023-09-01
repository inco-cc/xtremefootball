using System;
using System.Collections.Generic;
using Sandbox;
using Editor;
using XtremeFootball.Teams;

namespace XtremeFootball;

[Category( "Xtreme Football" )]
[Icon( "person" )]
public partial class Player : AnimatedEntity
{
	private static readonly List<Player> all = new();
	public static new IReadOnlyList<Player> All => all.AsReadOnly();

	[Net] public BaseTeam Team { get; set; }

	public Player()
	{
		all.Add( this );
	}

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "models/citizen/citizen.vmdl" );
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();

		all.Remove( this );
	}
}
