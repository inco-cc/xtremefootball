using System;
using System.Collections.Generic;
using Sandbox;
using Editor;

namespace XtremeFootball;

public enum TeamSide
{
	Home,
	Away,
}

[Category( "Xtreme Football" )]
[Icon( "groups" )]
public abstract partial class Team : Entity
{
	private static readonly List<Team> all = new();
	public static new IReadOnlyList<Team> All => all.AsReadOnly();

	public static Team Home
	{
		get
		{
			foreach ( var team in all )
				if ( team.Side == TeamSide.Home )
					return team;

			return null;
		}
	}
	public static Team Away
	{
		get
		{
			foreach ( var team in all )
				if ( team.Side == TeamSide.Away )
					return team;

			return null;
		}
	}

	public abstract new string Name { get; }
	public abstract ColorHsv Color { get; }

	[Net] public TeamSide Side { get; protected set; }

	[Net] public uint Goals { get; set; }

	public Team()
	{
		Transmit = TransmitType.Always;

		all.Add( this );
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();

		all.Remove( this );
	}
}
