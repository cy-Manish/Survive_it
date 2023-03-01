using Godot;
using System;

public class Player : KinematicBody2D
{
    private int speed = 100;
    private int gravity = 40;
    private float friction = .1f;
    private float acceleration = .5f;
    private int jumpHeight = 100;
    private int dashSpeed = 500;
    private bool isDashing = false;
    private float dashTimer = .2f;
    private float dashTimerReset = .2f;
    private bool isDashAvailable = true;
    private bool isWallJumping = false;
    private float wallJumpTimer = .45f;
    private float wallJumpTimerReset = .45f;
    private Vector2 velocity = new Vector2();
    private bool canClimb = true;
    private int climbSpeed = 100;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
 public override void _Process(float delta)
 {
    if(!isDashing && !isWallJumping){
        processMovement(delta);

        }
        if(IsOnFloor()){
            if(Input.IsActionJustPressed("jump"))
            {
                velocity.y = -jumpHeight;  
            }
            canClimb=true;
            isDashAvailable = true;
        }
        if(canClimb){
            processClimb(delta);
        }


        processWallJump(delta);  

            

        if(isDashAvailable)
        {
           processDash(delta);
        }

        if(isDashing){
            dashTimer -= delta;
            if(dashTimer <= 0)
            {
                isDashing = false;
                velocity = new Vector2(0,0);
            }
        }
            else
            {
                velocity.y += gravity * delta;

            }

        MoveAndSlide(velocity, Vector2.Up);  

    }

    private void processClimb(float delta){
        if(Input.IsActionPressed("climb")){
            if(canClimb){
                if(Input.IsActionPressed("ui_up")){
                    velocity.y = -climbSpeed;
                }
                else if(Input.IsActionJustPressed("ui_down")){
                    velocity.y = climbSpeed;

                }
                else{
                    velocity = new Vector2(0,0);
                }

            }
         }
    }
    private void processMovement(float delta){
        int direction = 0;
        if(Input.IsActionPressed("ui_left")){
            direction -= 1;
        }
        if(Input.IsActionPressed("ui_right")){
            direction += 1;
        }
        if(direction != 0){
            velocity.x = Mathf.Lerp(velocity.x, direction * speed, acceleration);
        }
        else{
            velocity.x = Mathf.Lerp(velocity.x, 0, friction);
        }
    }
    private void processDash(float delta){
         if(Input.IsActionJustPressed("dash"))
            {
                if (Input.IsActionPressed("ui_left"))
                {
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
    private void processWallJump(float delta){
        if (Input.IsActionJustPressed("jump") && GetNode<RayCast2D>("RayCastLeft").IsColliding()){
                velocity.y = -jumpHeight;
                velocity.x = jumpHeight;
                isWallJumping = true;
            }
            else if (Input.IsActionJustPressed("jump") && GetNode<RayCast2D>("RayCastLeft").IsColliding()){
                velocity.y = -jumpHeight;
                velocity.x = -jumpHeight;
                isWallJumping = true;
            }
            if (isWallJumping){
                wallJumpTimer -= delta;
                if(wallJumpTimer <= 0){
                    isWallJumping =false;
                    wallJumpTimer = wallJumpTimerReset;
                }
            }
    }

}
