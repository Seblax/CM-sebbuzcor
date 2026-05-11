using UnityEngine;

public class LookAt : MonoBehaviour
{
    public GameObject target;
    public float rotationOffset = 90f;

    private void Update()
    {
        if (target == null) return;

        // 1. Obtenemos la dirección del vector entre el objeto y la luz
        Vector3 direction = target.transform.position - transform.position;

        // 2. Calculamos el ángulo en radianes y lo pasamos a grados
        // Usamos Mathf.Atan2(y, x) porque es más preciso para ángulos de 360°
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 3. Aplicamos la rotación solo en el eje Z
        transform.rotation = Quaternion.Euler(0, 0, angle - rotationOffset);
    }
}
