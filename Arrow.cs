using Godot;
using System;

public class Arrow : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private int speed = 150;
    private float lifespan = 20;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
 public override void _Process(float delta)
 {
   Position += Transform.x * delta * speed;  
   lifespan -= delta;
   if  (lifespan < 0){
    QueueFree();
   }
 }
 private void _on_Area2D_body_entered(object body){
    QueueFree();
    if(body is KinematicBody2D){
        if(body is Player){
            Player pc = body as Player;
            pc.TakeDamage();
        }

    }

 }
}
