using Editor;
using Sandbox;
using XtremeFootball.Teams;

namespace XtremeFootball.Triggers;

[Library("trigger_goal_red")]
[HammerEntity]
public partial class RedGoalTrigger : BaseGoalTrigger
{
	public override BaseTeam Team => RedTeam.Current;
}
