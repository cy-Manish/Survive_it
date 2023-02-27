using Godot;
using System;

public class Player : KinematicBody2D
{
    private int speed = 200;
    private int gravity = 4000;
    private float friction = .1f;
    private float acceleration = .5f;
    private int jumpHeight = 300;
    private int dashSpeed = 500;
    private bool isDashing = false;
    private float dashTimer = .2f;
    private float dashTimerReset = .2f;
    private bool isDashAvailable = true;
    private Vector2 velocity = new Vector2();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
 public override void _Process(float delta)
 {
    if(!isDashing){

     int direction = 0;
     if(Input.IsActionPressed("ui_left")){
         direction -= 1;
     }
     if(Input.IsActionPressed("ui_right")){
         direction += 1;
     }
     if(direction != 0){
         velocity.x=Mathf.Lerp(velocity.x, direction * speed, acceleration);
     }
     else
     {
         velocity.x = Mathf.Lerp(velocity.x, 0, friction);
     }
    }
        if(IsOnFloor()){
        if(Input.IsActionJustPressed("jump")){
                velocity.y -= jumpHeight;  
            }
            isDashAvailable = true;
        }  

    if(isDashAvailable){
        if(Input.IsActionJustPressed("dash")){

            if (Input.IsActionPressed("ui_left")){
                velocity.x = -dashSpeed;
                isDashing = true;

                }
                if (Input.IsActionPressed("ui_right")){
                    velocity.x = dashSpeed;
                    isDashing = true;

                }

                if (Input.IsActionPressed("ui_up")){
                    velocity.y = -dashSpeed;
                    isDashing = true;
                }

                if (Input.IsActionPressed("ui_right") && Input.IsActionPressed("ui_up")){
                    velocity.x = dashSpeed;
                    velocity.y = -dashSpeed;
                    isDashing = true;
                }
                if (Input.IsActionPressed("ui_left") && Input.IsActionPressed("ui_up")){
                    velocity.x = -dashSpeed;
                    velocity.y = -dashSpeed;
                    isDashing = true;
                }

            dashTimer = dashTimerReset;
            isDashAvailable = false;
        }
    }
    if(isDashing){
        dashTimer -= delta;
        if(dashTimer <= 0){
            isDashing = false;
            velocity = new Vector2(0,0);
        }
        else{
            velocity.y += gravity * delta;

        }

    }
    MoveAndSlide(velocity, Vector2.Up);  
 }
}
