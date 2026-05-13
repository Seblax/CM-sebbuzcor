using System;
using UnityEngine;



public class Data
{
    public static class Minigame
    {
        public static string PLAYER_TAG = "Player";
        public static class Game0
        {
            public static class Bunny
            {

                public static string JUMP_SOUND = "BunnyJump";
                public static string HIT_SOUND = "BunnyHit";

                public static class Hop
                {
                    public static readonly float AMPLITUDE = 4f;
                    public static readonly float SPEED = 0.75f;
                    public static readonly Vector3 DIRECTION = Vector3.up;
                }
            }

            public static class Car
            {

                public static readonly string CAR_LOOP_SOUND = "CarMoveLoop";
                public static readonly string CAR_START_SOUND = "CarStart";

                public static class Mover
                {
                    public static readonly float DISTANCE = 100f;
                    public static readonly float INTERVAL = 5f;
                }

                public static class Hop
                {
                    public static readonly float AMPLITUDE = UnityEngine.Random.Range(0.15f, 0.25f);
                    public static readonly float SPEED = 0.25f;
                    public static readonly Vector3 DIRECTION = Vector3.up;
                }
            }
        }

        public static class Game1
        {
            public static string WIN_SOUND = "BlockWin";
            public static float HEALTH = 100f;
            public static float MIN_DMG = 5f;
            public static float MAX_DMG = 10f;

            public static float BASE_SCORE_POINTS = 100f;
            public static float RATIO_SCORE_POINTS_TIMER = 100f;

            public static class Blcok
            {

                public static readonly float DESTROY_DELAY = 2f;

                public static class Shake
                {

                    public static readonly Tuple<float, float> INTERVAL = Tuple.Create(0.1f, 0.15f);
                    public static readonly Tuple<float, float> MAGNITUDE = Tuple.Create(0.2f, 0.3f);

                }

                public static class Impulse
                {
                    public static readonly Tuple<float,float> X_RANGE = Tuple.Create(5f, 10f);
                    public static readonly Tuple<float, float> Y_RANGE = Tuple.Create(1f, 2f);
                    public static readonly Tuple<float, float> FORCE = Tuple.Create(7.5f, 10f);

                }
            }
        }

        public static class Game2
        {
            public static string WIN_SOUND = "BlockWin";
            public static float HEALTH = 100f;
            public static float MIN_DMG = 5f;
            public static float MAX_DMG = 10f;

            public static float BASE_SCORE_POINTS = 100f;
            public static float RATIO_SCORE_POINTS_TIMER = 100f;

            public static class Alan
            {
                public static readonly string ALAN_SCRIPTABLEOBJECTS_PATH = "ScriptableObjects/Minigames/Game 2 ScriptableObjects";

                public static readonly float BASE_SPEED = 1f;
                public static readonly float MAX_SPEED = 5f;
                public static readonly float ACCELERATION = 0.25f;

                public static readonly float TRAIL_DELAY = 1.25f;

                public static readonly float BASE_SCORE = 200f;

                public static readonly float MIN_X_SPAWN = 0.1f;
                public static readonly float MAX_X_SPAWN = 0.9f;
                public static readonly float MIN_Y_SPAWN = 0.15f;
                public static readonly float MAX_Y_SPAWN = 0.85f;

                public static readonly string BALLOON_POP_SOUND = "BalloonPop";
                public static readonly string BALLOON_EXPLODE_SOUND = "BalloonExplode";

                public static class Shake
                {
                    public static readonly float INTERVAL = 0.05f;
                    public static readonly float DURATION = 5f;
                }

                public static class Happy
                {
                    public static readonly float PERCENT = 1f;
                    public static readonly float SHAKE_SPEED = 0;
                }

                public static class Normal
                {
                    public static readonly float PERCENT = 0.75f;
                    public static readonly float SHAKE_SPEED = 2f;
                }


                public static class Scared
                {
                    public static readonly float PERCENT = 0.5f;
                    public static readonly float SHAKE_SPEED = 5f;
                }

                public static class Terrified
                {
                    public static readonly float PERCENT = 0.25f;
                    public static readonly float SHAKE_SPEED = 10f;
                }

                public static class Dead
                {
                    public static readonly float PERCENT = 0f;
                }
            }

            public static class Door
            {
                public static readonly string DOOR_SOUND = "Door";

                public static readonly float MIN_ALAN_DISTANCE = 4f;
                public static readonly float MAX_ALAN_DISTANCE = 5f;
                public static readonly float MIN_X_SPAWN = 0.15f;
                public static readonly float MAX_X_SPAWN = 0.85f;
                public static readonly float MIN_Y_SPAWN = 0.15f;
                public static readonly float MAX_Y_SPAWN = 0.85f;

                public static class Shake
                {
                    public static readonly float SPEED = 25f;
                    public static readonly float INTERVAL = 0.05f;
                    public static readonly float DURATION = 0.5f;

                }
            }
        }

        public static class Game3
        {
            public static readonly string GRAVEYARD_MUSIC_SOUND = "GraveyardMusic";

            public static readonly float RANDOM_SOUND_TIMER = 2.5f;

            public static class Cat
            {
                public static readonly string CAT_SPRITES_PATH = "Textures/Minigame/Game 3/gato";

                public static readonly float CATCH_TIME = 1.5f;
                public static readonly float SCALE_RATIO = 0.15f;

                public static readonly Vector3 LIGHT_START_POSITION = Vector3.zero;
                public static readonly float MIN_LIGHT_DISTANCE = 4f;
                public static readonly float MAX_LIGHT_DISTANCE = 50f;
                public static readonly float MIN_X_SPAWN = 0.1f;
                public static readonly float MAX_X_SPAWN = 0.9f;
                public static readonly float MIN_Y_SPAWN = 0.25f;
                public static readonly float MAX_Y_SPAWN = 0.85f;

                public static readonly string CAT_CATCH_SOUND = "CatCatch";
                public static readonly string CAT_VISIBLE_SOUND = "CatVisible";

                public static class Shake
                {
                    public static readonly float SPEED = 5f;
                    public static readonly float INTERVAL = 0.10f;
                    public static readonly float DURATION = 3.5f;
                }

                public static class Hop
                {
                    public static readonly float SPEED = 0.25f;
                    public static readonly float AMPLITUDE = 1.5f;
                }
            }

            public static class Lantern
            {
                public static readonly float BASE_SPEED = 5f;       // Velocidad inicial
                public static readonly float MAX_SPEED = 15f;      // Velocidad tope
                public static readonly float ACCELERATION = 10f;  // Qué tan rápido aumenta la velocidad

                public static class Beam
                {
                    public static readonly float BASE_SCALE = -2.5f;
                    public static readonly float BASE_POSITION = -0.75f;

                    public static readonly float SCALE_MODIFIER = -0.5f;
                    public static readonly float POSITION_MODIFIER = -0.15f;
                }
            }
        }

        public static class Game4
        {
            public static readonly string CARDS_SPRITES_PATH = "Textures/Minigame/Game 4/Card";

            public static class Card {
                public static readonly float IMPULSE_FORCE = 10;
                public static readonly float TORQUE_FORCE = 5;
                
                public static readonly Tuple<float, float> LINEAR_DAMPING = Tuple.Create(0.5f,0.75f);
                public static readonly Tuple<float, float> ANGULAR_DAMPING = Tuple.Create(0.5f, 0.75f);

                public static readonly Tuple<float, float> X_DIRECTION_RANGE = Tuple.Create(-0.5f,0.5f);
                public static readonly Tuple<float, float> Y_DIRECTION_RANGE = Tuple.Create(0.2f, 1f);
                public static readonly Tuple<float,float> Z_DIRECTION_RANGE = Tuple.Create(2.5f, 3f);


                public static readonly Tuple<float, float> X_ROTATION_RANGE = Tuple.Create(-2f, 2f);
                public static readonly Tuple<float, float> Y_ROTATION_RANGE = Tuple.Create(-2f, 2f);
                public static readonly Tuple<float, float> Z_ROTATION_RANGE = Tuple.Create(-2f, 2f);
            }

            public static class Table
            {
                public static readonly float SPEED = 0.25f;
                public static readonly float AMPLITUDE = 1.5f;
            }
        }
    }


}
