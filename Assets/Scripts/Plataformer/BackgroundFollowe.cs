using UnityEngine;

public class BackgroundFollowe : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;          // Jugador a seguir
    public Camera mainCamera;         // Cámara principal (perspectiva)
    public Vector3 offset;            // Offset adicional respecto a la posición de la cámara

    [Header("Configuración")]
    public bool followX = true;
    public bool followY = true;
    public bool followZ = false;      // Normalmente no seguimos Z si es UI o overlay

    private void Update()
    {
        if (mainCamera == null || player == null)
        {
            Debug.LogWarning("La cámara o el jugador no están asignados.");
            return;
        }

        // Obtener posición de la cámara en mundo
        Vector3 cameraPosition = mainCamera.transform.position;

        // Posición base para el sprite
        Vector3 targetPosition = Vector3.zero;

        // Aplicar los ejes a seguir
        float posX = followX ? cameraPosition.x + offset.x : transform.position.x;
        float posY = followY ? cameraPosition.y + offset.y : transform.position.y;
        float posZ = followZ ? cameraPosition.z + offset.z : transform.position.z;

        targetPosition = new Vector3(posX, posY, posZ);

        // Actualizar posición del sprite
        transform.position = targetPosition;
    }
}
