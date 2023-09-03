using Editor;
using Sandbox;
using XtremeFootball.Teams;

namespace XtremeFootball.Triggers;

[Library("trigger_goal_blue")]
[HammerEntity]
public partial class BlueGoalTrigger : BaseGoalTrigger
{
	public override BaseTeam Team => BlueTeam.Current;
}
