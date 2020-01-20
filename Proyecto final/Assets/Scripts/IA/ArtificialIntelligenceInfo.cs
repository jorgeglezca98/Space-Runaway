using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialIntelligenceInfo{

	public enum Direction
	{
			Left,
			Right
	}

	public Vector3 PreviousAsteroidPosition;
	public Vector3 CurrentAsteroidPosition;
	private Direction CurrentDirection;
	private Quaternion SpaceshipRotation;

	public ArtificialIntelligenceInfo(){
		PreviousAsteroidPosition = new Vector3(0,0,0);
		CurrentAsteroidPosition = new Vector3(0,0,0);
		CurrentDirection = Direction.Left;
	}

	public Quaternion GetSpaceshipRotation(){
		return SpaceshipRotation;
	}

	public void SetSpaceshipRotation(Quaternion rotation){
		SpaceshipRotation = rotation;
	}

	public Direction GetCurrentDirection(){
		return CurrentDirection;
	}

	public void SetCurrentDirection(Direction direction){
		CurrentDirection = direction;
	}

	public Vector3 GetPreviousAsteroidPosition(){
		return PreviousAsteroidPosition;
	}

	public void SetPreviousAsteroidPosition(Vector3 position){
		PreviousAsteroidPosition = position;
	}

	public Vector3 GetCurrentAsteroidPosition(){
		return CurrentAsteroidPosition;
	}

	public void SetCurrentAsteroidPosition(Vector3 position){
		CurrentAsteroidPosition = position;
	}




}
