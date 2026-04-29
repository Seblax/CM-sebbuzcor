using UnityEngine;

namespace Minigame
{
    public class Minigame : IPausable
    {
        int id;

        public float minigameDuration;
        public float minigameTimer;

        public int rounds;

        //Completado con exito;
        public bool succes = false;
        
        //Pausar;
        public bool IsPaused { get => paused; set => paused = value; }
        public bool paused;

        public float TimerPercent { get => this.minigameTimer / this.minigameDuration; }
        public bool IsTimerOver { get => this.minigameTimer < 0; }
        public int GetCurrentRound{ get => rounds;}
        public int IncrementRound { set => rounds += 1; }
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

        public void Timer() {
            if(this.IsPaused) { return; }

            if (minigameTimer > 0) this.minigameTimer -= Time.deltaTime;
        }
    }
}