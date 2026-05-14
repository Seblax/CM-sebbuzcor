using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using Unity.VisualScripting;
using UnityEngine;

namespace Minigame.Game4
{
    public class Card : MonoBehaviour
    {
        private static Sprite[] cardSpirtes;
        private Material cardMaterial;

        private PlayerInputDetector detector;
        private BoxCollider boxCollider;

        private Rigidbody rgbody;

        private void Awake()
        {
            if (cardSpirtes == null)
                cardSpirtes = Resources.LoadAll<Sprite>(Data.Minigame.Game4.CARDS_SPRITES_PATH);

            MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();

            if (meshRenderer != null)
            {
                cardMaterial = meshRenderer.material;
            }
            else
            {
                Debug.LogError("¡No se encontró un MeshRenderer en los hijos de " + gameObject.name + "!");
            }

            rgbody = GetComponent<Rigidbody>();
            rgbody.useGravity = false;
            rgbody.collisionDetectionMode = CollisionDetectionMode.Continuous;

            if (cardMaterial != null && cardSpirtes != null && cardSpirtes.Length > 0)
            {
                Sprite randomSprite = cardSpirtes.GetRandom();
                cardMaterial.SetTexture("_BaseMap", randomSprite.texture);
            }

            boxCollider = this.AddComponent<BoxCollider>();

            boxCollider.size = new Vector3(0.5f, 1f, 0.1f);
            boxCollider.center = new Vector3(0, 0.5f, 0f);
            boxCollider.enabled = false;

            detector = GetComponentInParent<PlayerInputDetector>();

            detector.PlayerInputDetected += ThrowCard;
        }

        void ThrowCard()
        {
            detector.PlayerInputDetected -= ThrowCard;

            rgbody.useGravity = true;

            float linearDamping = Random.Range(Data.Minigame.Game4.Card.LINEAR_DAMPING.Item1, Data.Minigame.Game4.Card.LINEAR_DAMPING.Item2);
            float angularDamping = Random.Range(Data.Minigame.Game4.Card.ANGULAR_DAMPING.Item1, Data.Minigame.Game4.Card.ANGULAR_DAMPING.Item2);

            rgbody.linearDamping = linearDamping;
            rgbody.angularDamping = angularDamping;

            float xDirection = Random.Range(Data.Minigame.Game4.Card.X_DIRECTION_RANGE.Item1, Data.Minigame.Game4.Card.X_DIRECTION_RANGE.Item2);
            float yDirection = Random.Range(Data.Minigame.Game4.Card.Y_DIRECTION_RANGE.Item1, Data.Minigame.Game4.Card.Y_DIRECTION_RANGE.Item2);
            float zDirection = Random.Range(Data.Minigame.Game4.Card.Z_DIRECTION_RANGE.Item1, Data.Minigame.Game4.Card.Z_DIRECTION_RANGE.Item2);

            float xRotation = Random.Range(Data.Minigame.Game4.Card.X_ROTATION_RANGE.Item1, Data.Minigame.Game4.Card.X_ROTATION_RANGE.Item2);
            float yRotation = Random.Range(Data.Minigame.Game4.Card.Y_ROTATION_RANGE.Item1, Data.Minigame.Game4.Card.Y_ROTATION_RANGE.Item2);
            float zRotation = Random.Range(Data.Minigame.Game4.Card.Z_ROTATION_RANGE.Item1, Data.Minigame.Game4.Card.Z_ROTATION_RANGE.Item2);


            Vector3 randomDirection = new Vector3(xDirection, yDirection, zDirection).normalized;

            // 5. Aplicar Impulso
            rgbody.AddForce(randomDirection * Data.Minigame.Game4.Card.IMPULSE_FORCE, ForceMode.Impulse);

            Vector3 randomRotation = new Vector3(xRotation, yRotation, zRotation);

            rgbody.AddTorque(randomRotation * Data.Minigame.Game4.Card.TORQUE_FORCE, ForceMode.Impulse);

            boxCollider.enabled = true;

            AudioManager.instance.PlayEffectDelay(Data.Minigame.Game4.Card.THROW_SOUND, Random.Range(0f, 1f));
        }

        private void Update()
        {
            if (MinigameManager.instance.minigame.TimerPercent <= 0)
            {
                Destroy(gameObject, 1f);
            }
        }
    }
}