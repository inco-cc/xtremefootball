using System;
using System.Collections.Generic;
using Sandbox;
using Editor;

namespace XtremeFootball.Teams;

public enum TeamSide
{
	Home,
	Away,
}

[Category( "Xtreme Football" )]
[Icon( "groups" )]
public abstract partial class BaseTeam : Entity
{
	private static readonly List<BaseTeam> all = new();
	public static new IReadOnlyList<BaseTeam> All => all.AsReadOnly();

	public static BaseTeam Home
	{
		get
		{
			foreach ( var team in all )
				if ( team.Side == TeamSide.Home )
					return team;

			return null;
		}
	}
	public static BaseTeam Away
	{
		get
		{
			foreach ( var team in all )
				if ( team.Side == TeamSide.Away )
					return team;

			return null;
		}
	}

	public IReadOnlyList<Player> Players
	{
		get
		{
			List<Player> players = new(Player.All);

			foreach ( var player in players )
				if ( player.Team != this )
					players.Remove( player );

			return players.AsReadOnly();
		}
	}

	public abstract new string Name { get; }
	public abstract ColorHsv Color { get; }

	[Net] public TeamSide Side { get; protected set; }

	[Net] public uint Goals { get; set; }

	public BaseTeam()
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
