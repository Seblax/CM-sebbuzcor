using UnityEngine;

namespace Minigame
{
    public class Minigame : IPausable
    {
        int id;
        public GameObject minigameObject;

        public GameObject tittle;
        public GameObject game;
        public GameObject score;


        public float minigameDuration;
        public float minigameTimer;

        //Completado con exito;
        public bool succes = false;

        //Pausar;
        public bool IsPaused { get => paused; set => paused = value; }
        public bool paused;

        public float GetMinigameDuration { get => minigameDuration; }
        public float TimerPercent { get => this.minigameTimer / this.minigameDuration; }
        public bool IsTimerOver { get => this.minigameTimer < 0; }

        public bool Win { get => succes; }
        public bool Lose { get => !succes; }

        public int ID { get => id; }


        Minigame(int id, GameObject minigameObject)
        {
            this.id = id;
            this.minigameObject = minigameObject;

            this.minigameDuration = 7;
            this.minigameTimer = this.minigameDuration;

            this.tittle = minigameObject.transform.GetChild(0).gameObject;
            this.game = minigameObject.transform.GetChild(1).gameObject;
            this.score = minigameObject.transform.GetChild(2).gameObject;
        }

        public static Minigame of(int id, GameObject minigameObject)
        {
            return new Minigame(id, minigameObject);
        }

        public void Reset()
        {
            minigameTimer = minigameDuration;
        }

        public void SetPaused(bool isPaused)
        {
            this.IsPaused = isPaused;
        }

        public void Timer()
        {
            if (this.IsPaused) { return; }

            if (minigameTimer > 0) this.minigameTimer -= Time.deltaTime;
        }

        public void Victory() { succes = true; }
        public void Defeat() { succes = false; }
    }
}