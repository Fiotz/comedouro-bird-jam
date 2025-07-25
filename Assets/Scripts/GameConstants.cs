using UnityEngine;

public static class GameConstants
{
    // ========================= Timers Constants ===========================
    public static readonly float timerToCheckInSeconds = 3.0f;

    public static readonly float maxTimeDaySeconds = 50f;

    public static readonly float foodTimeToExpireInSeconds = 10f;

    // ========================= Size Birds =================================
    public static readonly int smallBird = 1;
    public static readonly int mediumBird = 1;
    public static readonly int bigBird = 1;


    // ========================= Health Constants ==========================
    public static readonly float multiplyHealthPerBirdExistent = 1f;

    public static readonly float multiplyHealthPerBirdThatHasPass = 0.5f;

    public static readonly float maxHealthForComedouro = 100f;


    // ========================= Social Constants ==========================
    public static readonly float maxSocialForComedouro = 100f;

    public static readonly int numBirdsSocialGoodMin = 4;

    public static readonly int numBirdsSocialGoodMax = 6;

    public static readonly int maxNumberBirds = 10;

    public static readonly float decreaseSocialByNumBirds = 2.0f;

    public static readonly float increaseSocialByNumBirds = 1.0f;

    public static readonly float multiplySocialForBadHealth = 0.5f;
}
