# 🎯 Hangman Game Project
 
A classic **Hangman word-guessing game**, built as a C# console application with player profiles, persistent scoring, difficulty levels, and hints. Guess the hidden word letter by letter before you run out of attempts!
 
<p align="center">
  <img alt=".NET" src="https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white">
  <img alt="C%23" src="https://img.shields.io/badge/C%23-console--app-239120?logo=csharp&logoColor=white">
  <img alt="License" src="https://img.shields.io/badge/license-MIT-green">
</p>
---
 
## 📖 Overview
 
**HangmanGameProject** is a text-based implementation of the classic Hangman game. Players create or resume a profile, pick a difficulty level, and try to guess a randomly chosen word one letter at a time. Scores are tracked per player and persisted between sessions, and a leaderboard of the top players is shown at the end of a play session.
 
The project is a nice small example of OOP design in C#: an abstract base game class with difficulty-specific subclasses, event-driven game updates, file-based persistence, and extension methods.
 
---
 
## ✨ Features
 
- 🧑 **Player Profiles** — create a new player or resume an existing one by name or Player ID
- 🎚️ **Difficulty Levels**
  - **Easy** — attempts = word length + 5
  - **Hard** — attempts = `max(3, word length + 2)`
- 💡 **Hints** — reveal a random unguessed letter at the cost of an attempt
- 🔤 **Word Bank** — random words loaded from `Data/words.txt` (with a small built-in fallback list)
- 🏆 **Persistent Scoring** — scores are saved to `Data/scores.txt` and reloaded across sessions
- 📊 **Leaderboard** — top 3 players by score shown at the end of a session
- 🔁 **Replayability** — play multiple rounds in a single session
- ⚙️ **In-game Commands** — `hint`, `quit`, `stats`, `delete_scores`
- 📡 **Event-driven Updates** — game state changes and results are broadcast via C# events (`OnGameUpdate`, `OnGameEnd`)
---
 
## 🧱 Tech Stack
 
| Layer      | Technology            |
|-------------|--------------------------|
| Runtime      | .NET 8 (Console App)      |
| Language     | C#                         |
| Persistence  | Plain text files (`Data/`)  |
 
---
 
## 📁 Project Structure
 
```
HangmanGameProject/
├── Data/
│   ├── words.txt          # Word bank used to pick secret words
│   ├── scores.txt          # Persisted game results (auto-generated/updated)
│   └── player_id.txt        # Tracks the next available Player ID
├── Extensions/
│   └── StringExtensions.cs   # Helpers: HideLetters(), IsLetter()
├── Game/
│   ├── HangmanGame.cs         # Abstract base class with core game loop
│   ├── Easy.cs                 # Easy difficulty (more attempts)
│   ├── Hard.cs                  # Hard difficulty (fewer attempts)
│   ├── GameManager.cs            # Orchestrates player setup, game flow, leaderboard
│   ├── Provider.cs                # Loads words from file
│   ├── Score.cs                    # Reads/writes score file
│   └── Models/
│       ├── BaseEntity.cs           # Base class with Id
│       ├── Player.cs                # Player profile, ID generation, loading
│       └── Result.cs                 # Outcome of a single game round
├── Program.cs
└── HangmanGameProject.csproj
```
 
---
 
## 🚀 Getting Started
 
### Prerequisites
 
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
### Run the game
 
```bash
git clone https://github.com/<your-username>/HangmanGameProject.git
cd HangmanGameProject/HangmanGameProject
dotnet run
```
 
### How to play
 
1. **Enter your name** when prompted.
2. Choose whether you already have a **Player ID** (to resume your score) or start fresh.
3. Pick a **difficulty**: `1` for Easy, `2` for Hard.
4. Guess the word one letter at a time:
   - Type a single letter to guess it.
   - Type `hint` to reveal a random unguessed letter (costs an attempt).
   - Type `quit` to give up on the current word.
   - Type `delete_scores` to wipe the saved score file.
5. After each round, choose whether to **play again**.
6. When you're done, a **leaderboard** of the top 3 scoring players is displayed.
---
 
## 🧩 How It Works
 
- `GameManager` drives the overall flow: player setup, difficulty selection, running rounds, and showing the leaderboard.
- `HangmanGame` (abstract) implements the shared guessing loop; `Easy` and `Hard` only differ in how many attempts they grant via `InitializeAttempts()`.
- `Provider` reads candidate words from `Data/words.txt`, falling back to a small built-in word list if the file is missing or empty.
- `Score` persists each round's `Result` to `Data/scores.txt` in a simple pipe-delimited format, and can reload or delete that history.
- `Player` generates and tracks unique Player IDs (persisted in `Data/player_id.txt`) so players can resume their score across sessions.
---
 
## 🎮 Sample Session
 
```
Enter your name:
Elene
Do you have your Player ID? (y/n)
n
 
Welcome, Elene! Your ID is: 1, Score: 0
Choose difficulty: 1) Easy 2) Hard
1
[Update] Game started for Elene. Word length: 8.
Word: ________
Enter letter or command ( "hint", "quit"): c
[Update] Good! The word contains 'c'.
...
--- Game Over ---
Player: Elene
Word: computer
Won: True
Attempts left: 9
New player score: 19
```
 
---
 
## 🛠️ Possible Improvements
 
Ideas for extending the project:
 
- Add more word categories/difficulty-appropriate word lists
- Replace text-file persistence with a lightweight database
- Add a GUI or web front-end
- Add unit tests around `HangmanGame`, `Provider`, and `Score`
---
 
## 🤝 Contributing
 
Contributions are welcome!
 
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request
---
 
## 📄 License
 
This project is available under the MIT License. Feel free to use it for learning or as a starting point for your own projects.
 
---
 
<p align="center">Made with ❤️ and a lot of wrong guesses</p>
 
