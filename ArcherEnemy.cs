using Godot;
using System;

public class ArcherEnemy : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Player player;
    private bool active;
    private bool ableToShoot = true;
    private float shootTimer = 1f;
    private float shootTimerReset = 1f;
    [Export]
    public PackedScene Arrow;

    public bool isShooting = false;
    private AnimatedSprite animatedSprite;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (active)
        {
            var angle = GlobalPosition.AngleToPoint(player.GlobalPosition);
            if (Mathf.Abs(angle) > Mathf.Pi / 2)
            {
                animatedSprite.FlipH = false;
            }
            else
            {
                animatedSprite.FlipH = true;
            }
            if (ableToShoot)
            {
                var spaceState = GetWorld2d().DirectSpaceState;
                Godot.Collections.Dictionary result = spaceState.IntersectRay(this.Position, player.Position, new Godot.Collections.Array { this });
                if (result != null)
                {
                    if (result.Contains("collider"))
                    {
                        this.GetNode<Position2D>("ProjectileSpwan").LookAt(player.Position);
                        if (result["collider"] == player)
                        {
                            animatedSprite.Play("Shooting");
                            isShooting = true;

                        }
                    }
                }

            }
            else
            {
                if (!isShooting)
                {
                    animatedSprite.Play("Idle");
                }
            }
        }
        if (shootTimer <= 0)
        {
            ableToShoot = true;
        }
        else
        {
            shootTimer -= delta;
        }

    }

    public void _on_Detection_Radius_body_entered(object body)
    {
        GD.Print("Body has entered" + body);
        if (body is Player)
        {
            player = body as Player;
            active = true;
        }
    }
    private void _on_Detection_Radius_body_exited(object body)
    {
        GD.Print("Body has exited" + body);
        if (body is Player)
        {
            active = false;
        }
    }
    private void _on_AnimatedSprite_animation_finished()
    {
        if (animatedSprite.GetAnimation() == "Shooting")
        {
            shootAtPlayer();
            isShooting = false;
        }
    }
    private void shootAtPlayer()
    {
        GD.Print("Shooting");
        Arrow arrow = Arrow.Instance() as Arrow;
        Owner.AddChild(arrow);
        arrow.GlobalTransform = this.GetNode<Position2D>("ProjectileSpwan").GlobalTransform;
        ableToShoot = false;
        shootTimer = shootTimerReset;


    }
}
