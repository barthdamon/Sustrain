using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SurviosTestAlgosQ3 : MonoBehaviour {

	// Use this for initialization
	void Start () {

		// coin and trap positions mimic example board
		Vector2[] coinPositions = new Vector2[] {
			new Vector2 (0, 3),
			new Vector2 (1, 4),
			new Vector2 (0, 1),
			new Vector2 (1, 0),
			new Vector2 (3, 0),
			new Vector2 (4, 1),
// uncomment to test unable to collect all coins
// 			new Vector2 (4, 4)
		};

		Vector2[] trapPositions = new Vector2[] {
			new Vector2 (3, 3),
			new Vector2 (3, 4),
			new Vector2 (4, 4),
			new Vector2 (4, 3)
		};
			
		GameBoard gameBoard = new GameBoard (5, 5, new Vector2 (2, 2), trapPositions, coinPositions);
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

public struct Move {
	public List<Vector2> cells;
	public Vector2 endLocation;

	public Move(Vector2 endLocation, Vector2[] crossingCells) {
		this.endLocation = endLocation;
		cells = new List<Vector2> ();
		cells.AddRange (crossingCells);
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

	// THIS IS THE FUNCTION THAT ANSWERS THE QUESTION :)
	public bool CanCollectAllCoins() {

		// Calculate reachability of each coin
		bool allCoinsReached = true;
		foreach (Coin coin in coins) {
			FindWayToCoin(startingPosition, coin);
			if (!coin.isReachable) {
				allCoinsReached = false;
			}
		}
		return allCoinsReached;
	}
		
	void FindWayToCoin(Vector2 position, Coin coin) {
		if (!coin.isReachable) {
			List<Move> moves = GetMovesFromPosition (position);
			// was the coin found?
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
		List<Move> legitMoves = possibleMoves.Where( move => 
			// needs to land on the board
			!((move.endLocation.x > xLength - 1) || (move.endLocation.x < 0))
			&&
			!((move.endLocation.y > yLength - 1) || (move.endLocation.y < 0))
			&&
			// can't land on a trap
			!MoveHitsTrap(move)
		).ToList();
		return legitMoves;
	}
		
	bool MoveHitsTrap(Move move) {
		foreach (Vector2 trap in trapPositions) {
			if (trap == move.endLocation) {
				return true;
			}
		}
		return false;
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
