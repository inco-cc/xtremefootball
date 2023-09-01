using System;
using Sandbox;
using Editor;

namespace XtremeFootball;

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

		if ( client.Pawn != null && client.Pawn.IsValid )
			client.Pawn.Delete();
	}
}
