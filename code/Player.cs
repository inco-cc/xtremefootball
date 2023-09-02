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

	[Net]
	public BaseTeam Team { get; set; }

	[ClientInput]
	public Vector3 MoveDirection { get; protected set; }
	[ClientInput]
	public Rotation ViewRotation { get; set; }

	[Net, Predicted]
	public Vector3 LocalEyePosition { get; set; } = new( 0, 0, 72 );
	public Vector3 EyePosition => Transform.PointToWorld( LocalEyePosition );

	public Player()
	{
		EnableLagCompensation = true;

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

	public override void FrameSimulate( IClient client )
	{
		base.FrameSimulate( client );

		Camera.Rotation = ViewRotation;
		Camera.Position = EyePosition;

		Camera.FieldOfView = Screen.CreateVerticalFieldOfView( Game.Preferences.FieldOfView );
	}
}
