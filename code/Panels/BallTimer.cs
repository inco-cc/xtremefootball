using System;
using Sandbox.UI;
using XtremeFootball.Items;

namespace XtremeFootball.Panels;

public class BallTimer : WorldPanel
{
	private Ball Ball { get; set; }
	private Label Timer { get; set; }

	public BallTimer(Ball ball)
	{
		Ball = ball;
		Timer = new() { Id = "Timer" };

		AddChild(Timer);

		PanelBounds = new(-256, -256, 512, 512);

		StyleSheet.Load("/code/Panels/BallTimer.scss");
	}

	public override void OnHotloaded()
	{
		base.OnHotloaded();
	}

	public override void Tick()
	{
		base.Tick();

		var time = TimeSpan.FromSeconds(Ball.TimeUntilExpire);

		if (Ball.IsExpirable && Ball.TimeUntilExpire < (Ball.ExpireDelay / 2))
			Timer.Text = time.ToString("s'.'f");
		else
			Timer.Text = null;
	}
}
