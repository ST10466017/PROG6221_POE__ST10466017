using System;
using System.Media;

public static class AudioPlayer
{
	public static void PlayGreeting()
	{
		try
		{
			SoundPlayer player = new SoundPlayer("greeting.wav");
			player.PlaySync();
		}
		catch
		{
			Console.WriteLine("[!] Audio file missing. Zoro fights on silently.");
		}
	}
}