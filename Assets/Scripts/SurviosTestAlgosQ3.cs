using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SurviosTestAlgosQ3 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector2[] coinPositions = new Vector2[] {
			new Vector2 (0, 4),
			new Vector2 (0, 0),
			new Vector2 (4, 4)
		};

		Vector2[] trapPositions = new Vector2[] {
			new Vector2 (3, 3),
			new Vector2 (3, 4),
			new Vector2 (4, 4),
			new Vector2 (4, 3)
		};

		// Indexes of positions on board start @ 0
		GameBoard gameBoard = new GameBoard (5, 5, new Vector2 (0, 0), trapPositions, coinPositions);
		if (gameBoard.CanCollectAllCoins ()) {
			Debug.Log ("Gameboard can collect all coins");
		} else {
			Debug.Log ("Gameboard can NOT collect all coins");
		}
	}
		
}

public class Coin {
	public Vector2 position;
	public List<Vector2> examinedLocations = new List<Vector2>();
	public bool isReachable = false;

	public Coin(Vector2 position) {
		this.position = position;
	}

	public bool MovesCross(List<Move> moves) {
		// Need to deal with intersections, its not just if the player can land on coin its if he can cross the coin
		foreach (Move move in moves) {
			if (move.cells.Contains(position)) {
				isReachable = true;
				Debug.Log ("Coin Reached! " + position.ToString());
				return isReachable;
			}
		}
		return false;
	}

}

public class Move {
	public List<Vector2> cells = new List<Vector2>();
	public Vector2 endLocation;

	public Move(Vector2 endLocation, Vector2[] crossingCells) {
		this.endLocation = endLocation;
		foreach (Vector2 cell in crossingCells) {
			cells.Add (cell);
		}
		cells.Add (endLocation);
	}
}




public class GameBoard {
	int xLength;
	int yLength;
	Vector2 startingPosition;
	Vector2[] trapPositions;
	List<Coin> coins = new List<Coin>();


	public GameBoard(int x, int y, Vector2 startingPosition, Vector2[] trapPositions, Vector2[] coinPositions) {
		this.xLength = x;
		this.yLength = y;
		this.startingPosition = startingPosition;
		this.trapPositions = trapPositions;
		foreach (Vector2 position in coinPositions) {
			coins.Add (new Coin (position));
		}
	}

	public bool CanCollectAllCoins() {

		// Calculate reachability of each coin
		foreach (Coin coin in coins) {
			FindWayToCoin(startingPosition, coin);
		}

		// Check if each coin ended up reaching start
		bool allCoinsReached = true;
		foreach (Coin coin in coins) {
			if (!coin.isReachable) {
				allCoinsReached = false;
			}
		}

		return allCoinsReached;
	}

	// THIS IS THE FUNCTION THAT ANSWERS THE QUESTION
	void FindWayToCoin(Vector2 position, Coin coin) {
		if (!coin.isReachable) {
			List<Move> moves = GetMovesFromPosition (position);
			// was the coin found
			if (coin.MovesCross(moves)) {
				return;
			}

			foreach (Move move in moves) {
				if (!coin.examinedLocations.Contains (move.endLocation)) {
					coin.examinedLocations.Add (move.endLocation);
					FindWayToCoin (move.endLocation, coin);
				}
			}
		}
	}

	List<Move> GetMovesFromPosition(Vector2 position) {

		List<Move> possibleMoves = GeneratePossibleMoves (position);
		List<Move> legitMoves = new List<Move> ();

		foreach (Move move in possibleMoves) {
			bool legit = true;

			// has to be within the bounds of the grid
			if ((move.endLocation.x > xLength - 1) || (move.endLocation.x < 0)) {
				legit = false;
			}
			if ((move.endLocation.y > yLength - 1) || (move.endLocation.y < 0)) {
				legit = false;
			}

			// can't have a trap
			foreach (Vector2 trap in trapPositions) {
				if (trap == move.endLocation) {
					legit = false;
				}
			}

			if (legit) {
//				Debug.Log ("Move is legit to: " + move.endLocation.ToString() + " from: " + position.ToString());
				legitMoves.Add (move);
			} else {
//				Debug.Log ("Move not Legit to: " + move.endLocation.ToString() + " from: " + position.ToString());
			}
		}
		return legitMoves;
	}


	List<Move> GeneratePossibleMoves(Vector2 position) {
		List<Move> possibleMoves = new List<Move>();
		possibleMoves.Add (
			new Move(
				new Vector2 (position.x - 1, position.y + 2),
				new Vector2[] {
					new Vector2 (position.x - 1, position.y + 1),
					new Vector2 (position.x - 1, position.y),
					new Vector2 (position.x, position.y + 1),
					new Vector2 (position.x, position.y + 2),
				}
			)
		);
		possibleMoves.Add (
			new Move(
				new Vector2 (position.x - 1, position.y - 2),
				new Vector2[] {
					new Vector2 (position.x - 1, position.y - 1),
					new Vector2 (position.x - 1, position.y),
					new Vector2 (position.x, position.y - 1),
					new Vector2 (position.x, position.y - 2)
				}
			)
		);
		possibleMoves.Add (
			new Move(
				new Vector2 (position.x + 1, position.y + 2),
				new Vector2[] {
					new Vector2 (position.x + 1, position.y + 1),
					new Vector2 (position.x + 1, position.y),
					new Vector2 (position.x, position.y + 1),
					new Vector2 (position.x, position.y + 2)
				}
			)
		);
		possibleMoves.Add (
			new Move(
				new Vector2 (position.x + 1, position.y - 2),
				new Vector2[] {
					new Vector2 (position.x + 1, position.y - 1),
					new Vector2 (position.x + 1, position.y),
					new Vector2 (position.x, position.y - 1),
					new Vector2 (position.x, position.y - 2)
				}
			)
		);

		possibleMoves.Add (
			new Move(
				new Vector2 (position.x - 2, position.y + 1),
				new Vector2[] {
					new Vector2 (position.x - 1, position.y + 1),
					new Vector2 (position.x, position.y + 1),
					new Vector2 (position.x - 1, position.y),
					new Vector2 (position.x - 2, position.y)
				}
			)
		);

		possibleMoves.Add (
			new Move(
				new Vector2 (position.x - 2, position.y - 1),
				new Vector2[] {
					new Vector2 (position.x - 2, position.y),
					new Vector2 (position.x - 1, position.y),
					new Vector2 (position.x - 1, position.y - 1),
					new Vector2 (position.x, position.y - 1)
				}
			)
		);

		possibleMoves.Add (
			new Move(
				new Vector2 (position.x + 2, position.y + 1),
				new Vector2[] {
					new Vector2 (position.x + 1, position.y + 1),
					new Vector2 (position.x, position.y + 1),
					new Vector2 (position.x + 1, position.y),
					new Vector2 (position.x + 2, position.y)
				}
			)
		);

		possibleMoves.Add (
			new Move(
				new Vector2 (position.x + 2, position.y - 1),
				new Vector2[] {
					new Vector2 (position.x + 1, position.y),
					new Vector2 (position.x + 2, position.y),
					new Vector2 (position.x, position.y - 1),
					new Vector2 (position.x + 1, position.y - 1)
				}
			)
		);
		return possibleMoves;
	}

}
