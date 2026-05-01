using UnityEngine;

namespace Minigame
{
    public class Minigame : IPausable
    {
        int id;

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

        Minigame(int id)
        {
            this.id = id;

            this.minigameDuration = 7;
            this.minigameTimer = this.minigameDuration;
        }

        public static Minigame of(int id)
        {
            return new Minigame(id);
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