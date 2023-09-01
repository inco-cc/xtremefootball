using System;
using Sandbox;
using Editor;

namespace XtremeFootball;

[Category( "Xtreme Football" )]
public partial class GameManager : Sandbox.GameManager
{
	public override void ClientJoined( IClient client )
	{
		base.ClientJoined( client );

		var player = new Player();

		client.Pawn = player;
	}

	public override void ClientDisconnect( IClient client, NetworkDisconnectionReason reason )
	{
		base.ClientDisconnect( client, reason );

		client.Pawn?.Delete();
	}
}
