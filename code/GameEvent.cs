using System;
using Sandbox;
using Editor;

namespace XtremeFootball;

public static class GameEvent
{
	public static class Ball
	{
		public class Reset : EventAttribute
		{
			public Reset() : base( "ball.reset" )
			{
			}
		}
	}
}
