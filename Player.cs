using Godot;
using System;

public class Player : KinematicBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	Vector2 velocity {get; set;}
	const int MAX_SPEED = 100;
	const int ACCELERATION = 25;
	const int FRICTION = 150;
	[Export]
	public int speed = 10;
	public Vector2 Velocity = Vector2.Zero;
	
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
//	public void GetInput(){
//		Velocity = new Vector2();
//		if (Input.IsActionPressed("ui_right"))
//			Velocity.x +=1;
//
//		if (Input.IsActionPressed("ui_left"))
//
//			Velocity.x -=1;
//
//		if (Input.IsActionPressed("ui_down"))
//
//			Velocity.y +=1;
//
//		if (Input.IsActionPressed("ui_up"))
//			Velocity.y -=1;
//
//		Velocity = Velocity.Normalized() * speed;
//	}

public Vector2 GetInput(){
	var input_vector = Vector2.Zero;
	input_vector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
	input_vector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
	return input_vector;
}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
 public override void _Process(float delta)
 {   
	var currentAnimation = (AnimationPlayer)GetNode("AnimationPlayer");
	var currentAnimationTree = (AnimationTree)GetNode("AnimationTree");
	var animationTreeState = (AnimationNodeStateMachinePlayback)currentAnimationTree.Get("parameters/playback");
	var input_vector = GetInput();
	
	if(input_vector != Vector2.Zero){
//		Velocity += input_vector * ACCELERATION * delta;
//		Velocity = Velocity.Clamped(MAX_SPEED * delta);
		currentAnimationTree.Set("parameters/Idle/blend_position", input_vector);
		currentAnimationTree.Set("parameters/Run/blend_position", input_vector);
		animationTreeState.Travel("Run");
		Velocity = Velocity.MoveToward(input_vector * MAX_SPEED, ACCELERATION);
	}else{
		animationTreeState.Travel("Idle");
		Velocity = Velocity.MoveToward(Vector2.Zero, FRICTION * delta);
	}
	
	Velocity = MoveAndSlide(Velocity);
	
	//GetInput();
	//Velocity = MoveAndSlide(Velocity);
 }

}
