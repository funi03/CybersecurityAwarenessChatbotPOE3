using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Cybersecurity_Awareness_Chatbot_Part2
    {
        
        public class Quiz_Question_Load
        {
            // ============ FIELDS ============
            private List<Question_in_quiz> questions;
            private string saveFilePath;
            private Random random;

            // ============ CONSTRUCTOR ============
            public Quiz_Question_Load()
            {
                questions = new List<Question_in_quiz>();
                saveFilePath = "quiz_questions.json";
                random = new Random();
            }

            // ============ PUBLIC METHODS ============

            
            public List<Question_in_quiz> LoadAllQuestions()
            {
                if (File.Exists(saveFilePath))
                {
                    try
                    {
                        // If file exists, load from there
                        string json = File.ReadAllText(saveFilePath);
                        // You can use Newtonsoft.Json or System.Text.Json here
                        // For simplicity, we'll use the default set
                        return GetDefaultQuestions();
                    }
                    catch
                    {
                        return GetDefaultQuestions();
                    }
                }
                else
                {
                    // Create default questions and save to file
                    var defaultQuestions = GetDefaultQuestions();
                    SaveQuestions(defaultQuestions);
                    return defaultQuestions;
                }
            }

            
            public void autoLoadQuiz(ref List<Question_in_quiz> questions)
            {
                questions = GetDefaultQuestions();
            }

          
            public List<Question_in_quiz> GetRandomQuestions(int count = 10)
            {
                var allQuestions = LoadAllQuestions();
                if (allQuestions.Count <= count)
                    return allQuestions;

                var shuffled = allQuestions.OrderBy(x => random.Next()).ToList();
                return shuffled.Take(count).ToList();
            }

            
            public List<Question_in_quiz> GetQuestionsByCategory(string category)
            {
                return LoadAllQuestions()
                    .Where(q => q.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

           
            public List<Question_in_quiz> GetQuestionsByDifficulty(int difficulty)
            {
                return LoadAllQuestions()
                    .Where(q => q.Difficulty == difficulty)
                    .ToList();
            }

           
            public List<Question_in_quiz> GetQuestionsByCategoryAndDifficulty(string category, int difficulty)
            {
                return LoadAllQuestions()
                    .Where(q => q.Category.Equals(category, StringComparison.OrdinalIgnoreCase)
                           && q.Difficulty == difficulty)
                    .ToList();
            }

           
            public void SaveQuestions(List<Question_in_quiz> questions)
            {
                try
                {
                    // For simplicity, we'll save as CSV
                    var lines = new List<string>
                {
                    "Text,CorrectAnswer,WrongAnswers,Explanation,Category,Difficulty"
                };

                    foreach (var q in questions)
                    {
                        string wrongAnswers = string.Join(";", q.WrongAnswers);
                        string line = $"{EscapeCsv(q.Text)},{EscapeCsv(q.CorrectAnswer)},{EscapeCsv(wrongAnswers)},{EscapeCsv(q.Explanation ?? "")},{EscapeCsv(q.Category)},{q.Difficulty}";
                        lines.Add(line);
                    }

                    File.WriteAllLines(saveFilePath.Replace(".json", ".csv"), lines);
                }
                catch
                {
                    // If saving fails, continue with in-memory questions
                }
            }

          
            public int GetQuestionCount()
            {
                return LoadAllQuestions().Count;
            }

            
            public void AddQuestion(Question_in_quiz question)
            {
                var allQuestions = LoadAllQuestions();
                allQuestions.Add(question);
                SaveQuestions(allQuestions);
            }

            
            
            public List<Question_in_quiz> SearchQuestions(string searchTerm)
            {
                return LoadAllQuestions()
                    .Where(q => q.Text.ToLower().Contains(searchTerm.ToLower()) ||
                                q.Explanation.ToLower().Contains(searchTerm.ToLower()) ||
                                q.Category.ToLower().Contains(searchTerm.ToLower()))
                    .ToList();
            }

          
  
            public Dictionary<string, int> GetCategoryStatistics()
            {
                var stats = new Dictionary<string, int>();
                var allQuestions = LoadAllQuestions();

                foreach (var q in allQuestions)
                {
                    if (stats.ContainsKey(q.Category))
                        stats[q.Category]++;
                    else
                        stats[q.Category] = 1;
                }

                return stats;
            }

         
            public Dictionary<int, int> GetDifficultyStatistics()
            {
                var stats = new Dictionary<int, int>();
                var allQuestions = LoadAllQuestions();

                foreach (var q in allQuestions)
                {
                    if (stats.ContainsKey(q.Difficulty))
                        stats[q.Difficulty]++;
                    else
                        stats[q.Difficulty] = 1;
                }

                return stats;
            }

            // ============ PRIVATE METHODS ============

            private List<Question_in_quiz> GetDefaultQuestions()
            {
                return new List<Question_in_quiz>
            {
           
                // PASSWORD SAFETY QUESTIONS (5 questions)
              
                new Question_in_quiz
                {
                    Text = "What is password safety?",
                    CorrectAnswer = "Using unique and strong passwords",
                    WrongAnswers = new List<string> { "Sharing passwords with friends", "Using short passwords", "Using common words" },
                    Explanation = "Password safety involves using unique, complex passwords for each account to prevent unauthorized access.",
                    Category = "Password",
                    Difficulty = 1
                },
                new Question_in_quiz
                {
                    Text = "Which of these is a strong password?",
                    CorrectAnswer = "P@55w0rD!#987",
                    WrongAnswers = new List<string> { "Password123", "qwerty2024", "123456789" },
                    Explanation = "A strong password includes uppercase, lowercase, numbers, and special characters. P@55w0rD!#987 meets all these criteria.",
                    Category = "Password",
                    Difficulty = 1
                },
                new Question_in_quiz
                {
                    Text = "When should you update your password?",
                    CorrectAnswer = "Every 3-6 months",
                    WrongAnswers = new List<string> { "Yearly", "Never", "Only if hacked" },
                    Explanation = "Regular password updates every 3-6 months help prevent unauthorized access and protect your accounts.",
                    Category = "Password",
                    Difficulty = 2
                },
                new Question_in_quiz
                {
                    Text = "What is the risk of reusing passwords?",
                    CorrectAnswer = "One hack compromises all accounts",
                    WrongAnswers = new List<string> { "Typing delay", "Site error", "No effect" },
                    Explanation = "If one account is compromised, attackers can access all accounts using the same password. Always use unique passwords.",
                    Category = "Password",
                    Difficulty = 2
                },
                new Question_in_quiz
                {
                    Text = "What is two-factor authentication (2FA)?",
                    CorrectAnswer = "A second verification step",
                    WrongAnswers = new List<string> { "A single password", "An antivirus", "A firewall" },
                    Explanation = "2FA adds an extra layer of security by requiring two forms of verification, like a password and a code sent to your phone.",
                    Category = "Password",
                    Difficulty = 2
                },

                
                // PHISHING QUESTIONS (4 questions)
                
                new Question_in_quiz
                {
                    Text = "What is phishing?",
                    CorrectAnswer = "Tricking users to steal data",
                    WrongAnswers = new List<string> { "Data backup", "Safe login", "Password tips" },
                    Explanation = "Phishing is a cyberattack where attackers trick users into revealing sensitive information like passwords and credit card details.",
                    Category = "Phishing",
                    Difficulty = 1
                },
                new Question_in_quiz
                {
                    Text = "What is a sign of a phishing email?",
                    CorrectAnswer = "Urgent or strange links",
                    WrongAnswers = new List<string> { "Good grammar", "Known sender", "Unsubscribe button" },
                    Explanation = "Phishing emails often create urgency and contain suspicious links. Always check the sender's email address carefully.",
                    Category = "Phishing",
                    Difficulty = 2
                },
                new Question_in_quiz
                {
                    Text = "What should you do if you receive a suspicious email?",
                    CorrectAnswer = "Report and delete immediately",
                    WrongAnswers = new List<string> { "Reply and ask questions", "Forward to friends", "Click all links" },
                    Explanation = "Suspicious emails should be reported to your IT department or email provider and deleted without opening any links.",
                    Category = "Phishing",
                    Difficulty = 1
                },
                new Question_in_quiz
                {
                    Text = "How can you identify a phishing attempt?",
                    CorrectAnswer = "Check the sender's email address",
                    WrongAnswers = new List<string> { "Read the subject line only", "Check the font style", "Look at the signature" },
                    Explanation = "Always check the sender's email address. Phishing emails often use addresses that look similar but are slightly different.",
                    Category = "Phishing",
                    Difficulty = 2
                },

                
                // PRIVACY QUESTIONS (3 questions)
                
                new Question_in_quiz
                {
                    Text = "What is identity theft?",
                    CorrectAnswer = "Stealing personal information for fraud",
                    WrongAnswers = new List<string> { "Changing passwords", "Creating new accounts", "Backing up data" },
                    Explanation = "Identity theft involves stealing personal information like your ID number or bank details to commit fraud.",
                    Category = "Privacy",
                    Difficulty = 3
                },
                new Question_in_quiz
                {
                    Text = "How can you protect your privacy online?",
                    CorrectAnswer = "Review privacy settings regularly",
                    WrongAnswers = new List<string> { "Share everything online", "Use the same password", "Never log out" },
                    Explanation = "Regularly reviewing your privacy settings on social media and other platforms helps protect your personal information.",
                    Category = "Privacy",
                    Difficulty = 2
                },
                new Question_in_quiz
                {
                    Text = "What should you do if you're a victim of identity theft?",
                    CorrectAnswer = "Report immediately and change passwords",
                    WrongAnswers = new List<string> { "Ignore it", "Wait and see", "Tell no one" },
                    Explanation = "Immediate reporting to your bank and changing passwords minimizes damage from identity theft.",
                    Category = "Privacy",
                    Difficulty = 3
                },

                
                // BROWSING SAFETY QUESTIONS (3 questions)
               
                new Question_in_quiz
                {
                    Text = "What is safe browsing?",
                    CorrectAnswer = "Using trusted and secure websites",
                    WrongAnswers = new List<string> { "Click all links", "Visit unknown pages", "Enable pop-ups" },
                    Explanation = "Safe browsing means only visiting trusted websites and avoiding suspicious links that could contain malware.",
                    Category = "Browsing",
                    Difficulty = 1
                },
                new Question_in_quiz
                {
                    Text = "What is a sign of an unsafe website?",
                    CorrectAnswer = "Typos and excessive pop-ups",
                    WrongAnswers = new List<string> { "HTTPS shown", "Fast load", "No ads" },
                    Explanation = "Unsafe websites often have typos, excessive pop-ups, and lack security certificates. Look for HTTPS and padlock icons.",
                    Category = "Browsing",
                    Difficulty = 2
                },
                new Question_in_quiz
                {
                    Text = "What should you do on public Wi-Fi?",
                    CorrectAnswer = "Use VPN and avoid private info",
                    WrongAnswers = new List<string> { "Bank online", "File share", "Shop online" },
                    Explanation = "Public Wi-Fi is unsafe for sensitive transactions. Use a VPN for protection and avoid accessing financial accounts.",
                    Category = "Browsing",
                    Difficulty = 2
                },

              
                // MALWARE QUESTIONS (3 questions)
            
                new Question_in_quiz
                {
                    Text = "What is ransomware?",
                    CorrectAnswer = "Malware that encrypts files and demands payment",
                    WrongAnswers = new List<string> { "A type of phishing", "A free tool", "A network firewall" },
                    Explanation = "Ransomware locks your files and demands payment to release them. Never pay the ransom and restore from backups.",
                    Category = "Malware",
                    Difficulty = 3
                },
                new Question_in_quiz
                {
                    Text = "How can you protect against malware?",
                    CorrectAnswer = "Install trusted antivirus software",
                    WrongAnswers = new List<string> { "Click on all ads", "Download from unknown sites", "Disable updates" },
                    Explanation = "Installing and regularly updating trusted antivirus software helps protect your computer from malware infections.",
                    Category = "Malware",
                    Difficulty = 2
                },
                new Question_in_quiz
                {
                    Text = "What is social engineering?",
                    CorrectAnswer = "Psychological manipulation to trick users",
                    WrongAnswers = new List<string> { "A type of malware", "A software update", "A network tool" },
                    Explanation = "Social engineering exploits human psychology to gain unauthorized access. Be careful of people asking for sensitive information.",
                    Category = "Malware",
                    Difficulty = 3
                },

              
                // NETWORK SECURITY QUESTIONS (3 questions)
                
                new Question_in_quiz
                {
                    Text = "What is a firewall?",
                    CorrectAnswer = "Monitors and controls network traffic",
                    WrongAnswers = new List<string> { "Speeds up computer", "Stores files", "Encrypts emails" },
                    Explanation = "A firewall protects your network by monitoring and controlling incoming and outgoing traffic based on security rules.",
                    Category = "Network",
                    Difficulty = 2
                },
                new Question_in_quiz
                {
                    Text = "What is HTTPS?",
                    CorrectAnswer = "Secure version of HTTP for encrypted connections",
                    WrongAnswers = new List<string> { "A type of phishing", "A programming language", "A firewall" },
                    Explanation = "HTTPS encrypts data between your browser and the website, protecting your information from interception.",
                    Category = "Network",
                    Difficulty = 3
                },
                new Question_in_quiz
                {
                    Text = "Why should you keep software updated?",
                    CorrectAnswer = "To fix security vulnerabilities",
                    WrongAnswers = new List<string> { "To get new features", "To make it slower", "It's not important" },
                    Explanation = "Software updates often include patches for security vulnerabilities. Always keep your software updated.",
                    Category = "Network",
                    Difficulty = 2
                },

                
                // GENERAL SECURITY QUESTIONS (4 questions)
               
                new Question_in_quiz
                {
                    Text = "What is data backup?",
                    CorrectAnswer = "Creating copies of data for recovery",
                    WrongAnswers = new List<string> { "Deleting files", "Installing software", "Changing settings" },
                    Explanation = "Data backup ensures you can recover files in case of data loss from hardware failure, malware, or accidental deletion.",
                    Category = "Security",
                    Difficulty = 2
                },
                new Question_in_quiz
                {
                    Text = "What should you do if you're a victim of a cyberattack?",
                    CorrectAnswer = "Report immediately and change passwords",
                    WrongAnswers = new List<string> { "Ignore it", "Wait and see", "Tell no one" },
                    Explanation = "Immediate reporting and password changes minimize damage from cyberattacks.",
                    Category = "Security",
                    Difficulty = 3
                },
                new Question_in_quiz
                {
                    Text = "What is the main purpose of antivirus software?",
                    CorrectAnswer = "Detect and remove malicious software",
                    WrongAnswers = new List<string> { "Speed up computer", "Store passwords", "Backup files" },
                    Explanation = "Antivirus software protects your computer from malware and viruses by detecting and removing threats.",
                    Category = "Security",
                    Difficulty = 2
                },
                new Question_in_quiz
                {
                    Text = "What is encryption?",
                    CorrectAnswer = "Scrambling data to protect it",
                    WrongAnswers = new List<string> { "Deleting data", "Copying files", "Speeding up processes" },
                    Explanation = "Encryption scrambles data so that only authorized parties can read it. It's essential for protecting sensitive information.",
                    Category = "Security",
                    Difficulty = 3
                }
            };
            }

           
            private string EscapeCsv(string value)
            {
                if (string.IsNullOrEmpty(value))
                    return "";

                if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
                {
                    value = value.Replace("\"", "\"\"");
                    return $"\"{value}\"";
                }
                return value;
            }
        }
    }

    
