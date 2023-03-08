using Godot;
using System;

public class ArcherEnemy : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
    public void _on_Detection_Radius_body_entered(object body){
        GD.Print("Body has entered" +body);
    }
    private void _on_Detection_Radius_body_exited(object body){
        GD.Print("Body has exited" +body);
    }
}
