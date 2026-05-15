using UnityEngine;

public class Utils
{
    public static Vector3 RandomPosition(float minOffsetX, float maxOffsetX, float minOffsetY, float maxOffsetY)
    {
        Camera cam = Camera.main;

        float spawnX = Random.Range(minOffsetX, maxOffsetX);
        float spawnY = Random.Range(minOffsetY, maxOffsetY);
        ;

        Vector3 spawnViewportPos = new Vector3(spawnX, spawnY, Mathf.Abs(cam.transform.position.z));
        return cam.ViewportToWorldPoint(spawnViewportPos);
    }

    public static Vector3 RandomPosition(Vector3 position, float minDistance, float maxDistance, float minOffsetX, float maxOffsetX, float minOffsetY, float maxOffsetY)
    {
        Camera cam = Camera.main;
        Vector3 finalPosition;
        float currentDistance;

        int maxAttempts = 50;
        int currentAttempt = 0;


        do
        {
            float spawnX = Random.Range(minOffsetX, maxOffsetX);
            float spawnY = Random.Range(minOffsetY, maxOffsetY);

            float distanceZ = Mathf.Abs(cam.transform.position.z - position.z);
            Vector3 spawnViewportPos = new Vector3(spawnX, spawnY, distanceZ);
            finalPosition = cam.ViewportToWorldPoint(spawnViewportPos);

            currentDistance = Vector3.Distance(finalPosition, position);

            if (currentAttempt > maxAttempts)
            {
                currentAttempt = 0;
                minDistance -= 0.1f;
            }

            currentAttempt++;
        } while (currentDistance < minDistance || currentDistance > maxDistance);

        Debug.Log($"Position {position} \nFinal Position {finalPosition}");
        return finalPosition;
    }
}
