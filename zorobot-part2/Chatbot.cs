using System;
using System.Collections.Generic;

namespace ZoroCyberSecurityBot
{
    // Part 2 Mandate: Implementation of delegate-driven processing logic
    public delegate string BotAction(string input);

    public class Chatbot
    {
        // Properties tracking runtime Memory & Sentiment contexts
        public string UserName { get; private set; }
        public string FavouriteTopic { get; private set; } = string.Empty;
        public string CurrentSentiment { get; private set; } = "Neutral";
        private string _activeContextTopic = string.Empty;

        // Generic Collections used to store response pools and mapping paths
        private readonly Dictionary<string, BotAction> _keywordRoutes;
        private readonly Dictionary<string, List<string>> _randomizedResponsePool;

        public Chatbot(string userName)
        {
            UserName = userName;
            _keywordRoutes = new Dictionary<string, BotAction>();
            _randomizedResponsePool = new Dictionary<string, List<string>>();

            InitializeDataPools();
            RegisterCommandRoutes();
        }

        private void InitializeDataPools()
        {
            // Task 3: Use structures/generic lists to store sets of random variant returns
            _randomizedResponsePool["phishing"] = new List<string>
            {
                "[CUT] Never click suspicious links. Verify first, then trust.",
                "[SLASH] Phishing relies on urgency. Take a breath and check the sender address closely.",
                "[SWORD] Check for domain spoofs! A trusted brand won't email you from an unverified public mailbox."
            };

            _randomizedResponsePool["password"] = new List<string>
            {
                "[SLASH] Use a different password for every account. No exceptions.",
                "[GUARD] Deploy long passphrases! Mix words, characters, numbers, and symbols to frustrate bad actors.",
                "[STRIKE] A password manager is your greatest shield. Never reuse credentials across portals."
            };

            _randomizedResponsePool["privacy"] = new List<string>
            {
                "[SHIELD] Limit what data you post publicly. Social engineering attacks leverage personal footprints.",
                "[GUARD] Inspect third-party app privacy permissions regularly. Keep access restricted.",
                "[SLASH] Data leakage can reveal location data. Shield your metadata carefully."
            };

            _randomizedResponsePool["scam"] = new List<string>
            {
                "[ALERT] Scammers act quickly to block your critical thinking. Always seek a second opinion.",
                "[STRIKE] If an offer demands cryptocurrency payments or immediate gift cards, pull back immediately.",
                "[CUT] Hang up on unsolicited support center callers. Check real contact channels."
            };
        }

        private void RegisterCommandRoutes()
        {
            // Standard static actions
            _keywordRoutes["help"] = (input) => "Topics: password, phishing, privacy, scam, how are you, purpose";
            _keywordRoutes["how are you"] = (input) => "Three-sword style ready. I'm alert and active.";
            _keywordRoutes["purpose"] = (input) => "Ask me about: passwords, phishing, privacy, or scams.";
            _keywordRoutes["what can i ask"] = _keywordRoutes["purpose"];

            // Custom multi-variant random responses
            _keywordRoutes["password"] = (input) => FetchRandomResponse("password");
            _keywordRoutes["phishing"] = (input) => FetchRandomResponse("phishing");
            _keywordRoutes["privacy"] = (input) => FetchRandomResponse("privacy");
            _keywordRoutes["scam"] = (input) => FetchRandomResponse("scam");

            // Context/Follow-up Flow expansion commands
            _keywordRoutes["give me another tip"] = (input) => ProcessFollowUpRequest();
            _keywordRoutes["explain more"] = _keywordRoutes["give me another tip"];
            _keywordRoutes["tell me more"] = _keywordRoutes["give me another tip"];
        }

        public string GenerateResponse(string rawInput)
        {
            // Task 7: Build professional-grade defensive catch structures around external mutations
            try
            {
                string cleanInput = rawInput.Trim().ToLower();

                // Task 6: Track user sentiment parameters prior to routing 
                EvaluateSentiment(cleanInput);

                // Task 5: Memory profiling capture check
                CapturePersonalDetails(cleanInput);

                // Task 4: Continuous conversational context handling
                foreach (var route in _keywordRoutes)
                {
                    if (cleanInput.Contains(route.Key))
                    {
                        // Cache topic category when dealing with core security rules
                        if (IsPrimaryTopic(route.Key))
                        {
                            _activeContextTopic = route.Key;
                        }

                        string output = route.Value.Invoke(cleanInput);
                        return ContextualizeBySentiment(output);
                    }
                }

                // Default Fallback matching task requirements
                return "I'm not sure I understand. Can you try rephrasing?";
            }
            catch (Exception ex)
            {
                // Fallback catch boundary preventing total process faults
                return $"[Zoro internal error encountered]: {ex.Message}. Fought through cleanly without dropping session.";
            }
        }

        private string FetchRandomResponse(string key)
        {
            if (_randomizedResponsePool.ContainsKey(key))
            {
                var targetList = _randomizedResponsePool[key];
                Random rand = new Random();
                return targetList[rand.Next(targetList.Count)];
            }
            return "[Zoro] Error extracting database response elements.";
        }

        private string ProcessFollowUpRequest()
        {
            if (string.IsNullOrEmpty(_activeContextTopic))
            {
                return "You haven't mentioned a cybersecurity topic yet. Tell me what you're working with: passwords, phishing, privacy, or scams.";
            }
            return $"Expanding track options on '{_activeContextTopic}':\n" + FetchRandomResponse(_activeContextTopic);
        }

        private void CapturePersonalDetails(string input)
        {
            // Memory patterns: "i'm interested in..." or "i am interested in..."
            if (input.Contains("interested in"))
            {
                string[] segments = input.Split(new[] { "interested in" }, StringSplitOptions.None);
                if (segments.Length > 1)
                {
                    string topicClean = segments[1].Replace(".", "").Replace("!", "").Trim();
                    if (!string.IsNullOrEmpty(topicClean))
                    {
                        FavouriteTopic = topicClean;
                    }
                }
            }
        }

        private void EvaluateSentiment(string input)
        {
            // Task 6: Flag simple emotive marker states
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("afraid") || input.Contains("anxious"))
            {
                CurrentSentiment = "Worried";
            }
            else if (input.Contains("curious") || input.Contains("wonder") || input.Contains("interested"))
            {
                CurrentSentiment = "Curious";
            }
            else if (input.Contains("frustrated") || input.Contains("annoyed") || input.Contains("angry") || input.Contains("stupid"))
            {
                CurrentSentiment = "Frustrated";
            }
        }

        private string ContextualizeBySentiment(string baseResponse)
        {
            // Enriches the response using empathetic tone adjustment modifiers based on user emotional state
            switch (CurrentSentiment)
            {
                case "Worried":
                    return $"It's completely understandable to feel that way. Scammers can be very convincing. Let me share some tips to help you stay safe:\n{baseResponse}";
                case "Frustrated":
                    return $"Take a deep breath. Cybersecurity infrastructure can feel intensely complex, but we can secure things piece by piece.\n{baseResponse}";
                case "Curious":
                    return $"Excellent question. Digging deeper into defensive patterns sharpens your skill sets. Here is the breakdown:\n{baseResponse}";
                default:
                    return baseResponse;
            }
        }

        private bool IsPrimaryTopic(string key)
        {
            return key == "password" || key == "phishing" || key == "privacy" || key == "scam";
        }
    }
}