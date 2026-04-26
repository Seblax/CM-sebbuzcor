using UnityEngine;

namespace Minigame
{
    public class Minigame : IPausable
    {


        public float minigameDuration = 7;
        public float minigameTimer;

        //Completado con exito;
        public bool succes = false;
        
        //Pausar;
        public bool IsPaused { get => paused; set => paused = value; }
        public bool paused;

        public float TimerPercent { get => this.minigameTimer / this.minigameDuration; }
        public bool IsTimerOver { get => this.minigameTimer < 0; }

        Minigame(float duration)
        {
            minigameDuration = duration;
        }

        public static Minigame of(float duration)
        {
            return new Minigame(duration);
        }

        public void Reset()
        {
            minigameTimer = minigameDuration;
        }

        public void SetPaused(bool isPaused)
        {
            this.IsPaused = isPaused;
        }

        public void Timer() {
            if(this.IsPaused) { return; }

            if (minigameTimer > 0) this.minigameTimer -= Time.deltaTime;
        }
    }
}