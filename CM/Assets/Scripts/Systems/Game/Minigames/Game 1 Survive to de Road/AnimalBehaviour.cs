using UnityEngine;

namespace Game0
{

    public class AnimalBehaviour : PlayerControllerTap
    {
        public Hop _hop;

        void Start()
        {

        }

        void Update()
        {

        }

        public override void TapEvent()
        {
            _hop.Play();
        }
    }

}