using System;
using System.Collections;
using System.Collections.Generic;

namespace Cybersecurity_Awareness_Chatbot_Part2
{
    
        public class sentiment_detect
        {
            // RANDOM OBJECT
            Random random = new Random();

            // SENTIMENT DETECTION
            public string detect_sentiment(string message)
            {
                // LOWERCASE MESSAGE
                message = message.ToLower();

                // WORRIED
                if (message.Contains("worried") ||
                    message.Contains("scared") ||
                    message.Contains("afraid"))
                {
                    return "worried";
                }

                // FRUSTRATED
                else if (message.Contains("frustrated") ||
                         message.Contains("angry") ||
                         message.Contains("annoyed"))
                {
                    return "frustrated";
                }

                // CURIOUS
                else if (message.Contains("curious") ||
                         message.Contains("interested") ||
                         message.Contains("learn"))
                {
                    return "curious";
                }

                // HAPPY
                else if (message.Contains("happy") ||
                         message.Contains("good") ||
                         message.Contains("great"))
                {
                    return "happy";
                }

                // SAD
                else if (message.Contains("sad") ||
                         message.Contains("upset") ||
                         message.Contains("depressed"))
                {
                    return "sad";
                }

                // NO SENTIMENT
                return "neutral";
            }

            // DYNAMIC SENTIMENT RESPONSES
            public string sentiment_response(string feeling)
            {
                List<string> responses = new List<string>();

                // WORRIED
                if (feeling == "worried")
                {
                    responses.Add("It is understandable to feel worried. Cyber threats are common, but I can help you stay safe online.");
                    responses.Add("Do not panic. I can give you cybersecurity tips to stay protected.");
                    responses.Add("Many people worry about online safety. Learning cybersecurity helps reduce risks.");
                }

                // FRUSTRATED
                else if (feeling == "frustrated")
                {
                    responses.Add("I understand your frustration. Cybersecurity can feel overwhelming sometimes.");
                    responses.Add("It is okay to feel frustrated. I will help you step by step.");
                    responses.Add("Do not worry, cybersecurity becomes easier with practice.");
                }

                // CURIOUS
                else if (feeling == "curious")
                {
                    responses.Add("That is great! Learning about cybersecurity helps you stay protected online.");
                    responses.Add("Curiosity is important in cybersecurity awareness.");
                    responses.Add("The more you learn about cybersecurity, the safer you become online.");
                }

                // HAPPY
                else if (feeling == "happy")
                {
                    responses.Add("I am glad you are feeling positive about cybersecurity awareness!");
                    responses.Add("That is wonderful to hear!");
                    responses.Add("Positivity makes learning easier and more enjoyable.");
                }

                // SAD
                else if (feeling == "sad")
                {
                    responses.Add("I am sorry you are feeling sad. I am here to help.");
                    responses.Add("Take things one step at a time. You are doing well.");
                    responses.Add("Cybersecurity can be stressful, but learning helps build confidence.");
                }

                // RETURN RANDOM RESPONSE
                if (responses.Count > 0)
                {
                    int index = random.Next(responses.Count);

                    return responses[index];
                }

                // DEFAULT
                return "";
            }

        }//end class
    }//end namespace

