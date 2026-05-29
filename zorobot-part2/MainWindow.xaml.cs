using System;
using System.Windows;
using System.Windows.Input;

namespace ZoroCyberSecurityBot
{
	public partial class MainWindow : Window
	{
		private Chatbot? _bot;

		public MainWindow()
		{
			InitializeComponent();
			AudioPlayer.PlayGreeting();
			TxtChatLog.Text = "[!] Initialize Zoro to begin monitoring security vectors.\n";
		}

		private void InitializeSession()
		{
			string name = TxtNameInput.Text.Trim();
			if (string.IsNullOrWhiteSpace(name)) name = "Rookie";

			_bot = new Chatbot(name);

			// Update UI State
			SetupPanel.Visibility = Visibility.Collapsed;
			ChatInputPanel.Visibility = Visibility.Visible;
			TxtMemoryName.Text = $"User: {name}";

			TxtChatLog.Text += $"[!] Welcome, {name}. Zoro stands ready.\n";
			TxtChatLog.Text += "Zoro >> Type 'help' to see what I can do or 'exit' to close.\n";

			TxtUserInput.Focus();
		}

		private void ProcessTurn()
		{
			if (_bot == null) return;

			string input = TxtUserInput.Text.Trim();
			if (string.IsNullOrWhiteSpace(input))
			{
				TxtChatLog.Text += $"{_bot.UserName} >> [Empty input]\nZoro >> [!] Empty strike. Say something.\n\n";
				TxtUserInput.Clear();
				ChatScroll.ScrollToEnd();
				return;
			}

			if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
			{
				Application.Current.Shutdown();
				return;
			}

			// Append user input text to UI
			TxtChatLog.Text += $"{_bot.UserName} >> {input}\n";

			// Process text response via Chatbot logic
			string response = _bot.GenerateResponse(input);
			TxtChatLog.Text += $"Zoro >> {response}\n\n";

			// Sync/Update memory and sentiment dashboard view trackers
			TxtMemoryTopic.Text = string.IsNullOrEmpty(_bot.FavouriteTopic) ? "Interest: None" : $"Interest: {_bot.FavouriteTopic}";
			TxtCurrentSentiment.Text = $"Current Mood: {_bot.CurrentSentiment.ToUpper()}";

			TxtUserInput.Clear();
			ChatScroll.ScrollToEnd();
		}

		private void BtnStart_Click(object sender, RoutedEventArgs e) => InitializeSession();

		private void TxtNameInput_KeyDown(object sender, KeyInputEventArgs e)
		{
			if (e.Key == Key.Enter) InitializeSession();
		}

		private void BtnSend_Click(object sender, RoutedEventArgs e) => ProcessTurn();

		private void TxtUserInput_KeyDown(object sender, KeyInputEventArgs e)
		{
			if (e.Key == Key.Enter) ProcessTurn();
		}
	}
}