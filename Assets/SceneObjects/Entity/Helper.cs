using UnityEngine;

public static class Helper
{
    // HelperMethods
    public static GameObject DebugSphere(Vector2 position)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = position;
        sphere.transform.localScale = new Vector2(0.1f, 0.1f);
        sphere.GetComponent<Renderer>().material.color = Color.red;
        return sphere;
    }
}
