using Sandbox;

namespace XtremeFootball;

[Category("Xtreme Football")]
public partial class GameManager : Sandbox.GameManager
{
	public override void ClientJoined(IClient client)
	{
		base.ClientJoined(client);

		client.Pawn = new Player();
	}

	public override void ClientDisconnect(IClient client, NetworkDisconnectionReason reason)
	{
		base.ClientDisconnect(client, reason);

		client.Pawn?.Delete();
	}
}
