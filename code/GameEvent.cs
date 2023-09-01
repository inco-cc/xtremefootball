using System;
using Sandbox;
using Editor;

namespace XtremeFootball;

public static class GameEvent
{
	public static class Item
	{
		public class Expire : EventAttribute
		{
			public Expire() : base( "item.expire" )
			{
			}
		}
	}

	public static class Ball
	{
		public class Reset : EventAttribute
		{
			public Reset() : base( "ball.reset" )
			{
			}
		}
	}

	public static class Announcer
	{
		public class Say : EventAttribute
		{
			public Say() : base( "announcer.say" )
			{
			}
		}
	}
}
