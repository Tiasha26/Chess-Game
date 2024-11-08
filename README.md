# Chess Game
This is a classic chess game created in Unity, featuring fully interactive gameplay with both white and black pieces, turn-based logic, and win conditions based on standard chess rules. It allows two players to play against each other on the same device, with clear indications for check and checkmate.

## Table of Contents
- [Features](#features)
- [Installation](#installation)
- [How to Play](#how-to-play)
- [Gameplay Mechanics](#gameplay-mechanics)
- [Known Issues](#known-issues)
- [Future Improvements](#future-improvements)
  
## Features
- Two-player Mode: Play against a friend in a hot-seat style game.
- Check and Checkmate Detection: Alerts players when their king is in check and announces the winner upon checkmate.
- Standard Chess Mechanics: Supports piece movement, captures, and basic rules such as turn-taking.
- Visual Indicators: Highlights available moves and shows red markers for potential captures.
  
## Installation
1. Clone this repository:
```bash
git clone https://github.com/Tiasha26/Chess-Game.git
```
2. Open the project in Unity (version 2020.3 or later recommended).
3. Open the Game scene located in the Assets/Scenes folder.
4. Press Play in the Unity editor to start the game.

## How to Play
1. Objective: The goal is to checkmate the opponent's king.
2. Controls:
    - Click on a piece to highlight its available moves.
    - Click on a highlighted square to move the piece to that location.
    - Capture opponent pieces by moving your piece onto their square.
3. End of Turn: After moving a piece, the turn automatically switches to the opponent.

## Gameplay Mechanics
- Pieces: Includes all standard chess pieces: king, queen, rook, bishop, knight, and pawns.
- Moves and Captures: Valid moves are displayed for each piece, with special moves for capturing opponent pieces.
- Check and Checkmate: When a king is in check, it is highlighted, and if no valid moves are left to escape, the game ends in checkmate.
- Turn Management: Turns alternate between white and black players, automatically managed within the game logic.
  
## Known Issues
- Some special rules (like castling, en passant, and pawn promotion) are not yet implemented.
- Currently, only two-player mode on the same device is supported.
  
## Future Improvements
- AI Opponent: Add an option for single-player mode with an AI opponent.
- Special Moves: Implement castling, en passant, and pawn promotion.
- Online Multiplayer: Enable online play against other players.
- UI Improvements: Add better visuals and animations for moves and captures.
