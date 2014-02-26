using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NegroniGame //GameSettings.
{
    public struct GameSettings
    {
        public const float SECONDS_TO_DISAPPEARING = 20; //from class Drop
        public const int PERCENT_TO_DROP_POTION = 10; //from class Drop
        public const int REUSE_TIME = 10; //from class ElixirsHandler
        public const int MAX_SPAWNED_MOBS = 4; //from class MonstersHandler
        public const float SHOT_REUSE_TIME = 1000f; //from class ShotsHandler
        public const int RECOVERY_AMOUNT = 30; //from struct ElixirsHP
        public const float TIME_TO_CHANGE_DIRECTION = 5; //from class Monster -> sec 5
        public const int MOVE_MAX_LENGTH = 80; //from class Monster
        public const float ANIM_DELAY = 200f; //from class Monster
        public const int MOB_SPEED = 1; //from class Monster
        public const int AGGRO_RANGE = 80; //from class Monster
        public const int ATTACK_INTERVAL = 2; //from class Monster
        public const double WIDTH_OF_FULL_HEALTHBAR = 32; //from class Monster
        public const double MINUS_WIDTH_OF_HEALTH_BAR = 3.2; //from class Monster
        public const int HP_POINTS_INITIAL = 200; //from class Player
        public const float PLAYER_SPEED = 2f; //from class Player
        public const float PLAYER_ANIM_SPEED = 200f; //from class Player
        public const float SHOT_SPEED = 4f; //from class Shot
        public const float SHOT_ANIM_SPEED = 100f; //from class Shot
        public const int RANGE = 150; //from class Shot
        public const float WELL_REUSE_TIME = 15; //from class Well

    }
}
