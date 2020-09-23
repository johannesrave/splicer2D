public class MovementA : Movement
{
    protected override void Move(float speed)
    {
        var transformPosition = transform.position;
        transformPosition.y -= speed;
        
    }
}