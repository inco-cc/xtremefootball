using System;
using System.Collections.Generic;
using Sandbox;
using Editor;
using XtremeFootball.Teams;

namespace XtremeFootball.SpawnPoints;

[Category( "Xtreme Football" )]
[Icon( "place" )]
public abstract partial class BaseSpawnPoint : SpawnPoint
{
	private static readonly List<BaseSpawnPoint> all = new();
	public static new IReadOnlyList<BaseSpawnPoint> All => all.AsReadOnly();

	public abstract BaseTeam Team { get; }

	public BaseSpawnPoint()
	{
		all.Add( this );
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();

		all.Remove( this );
	}
}
