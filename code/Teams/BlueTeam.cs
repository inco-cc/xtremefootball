using Editor;
using Sandbox;

namespace XtremeFootball.Teams;

[Library("info_team_blue")]
[HammerEntity]
public partial class BlueTeam : BaseTeam
{
	public static BlueTeam Current { get; private set; }

	public override string Name { get; } = "Blue Bulls";
	public override ColorHsv Color { get; } = new(240, .8f, 1);

	public BlueTeam()
	{
		Current?.Delete();

		Current = this;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();

		if (Current == this)
			Current = null;
	}

	[Sandbox.GameEvent.Entity.PostSpawn]
	protected static void AutoSpawn()
	{
		if (All.Count < 2)
			_ = new BlueTeam();
	}
}
