using UnityEngine;

public static class GameConstants
{
    // ========================= Timers Constants ===========================
    public static readonly int numMaxCleanUsesPerDay = 3;

    public static readonly int numMaxResearchUsesPerDay = 3;

    // ========================= Timers Constants ===========================
    public static readonly float oneHourInSeconds = 2.5f;

    public static readonly float timerToCheckInSeconds = 1.0f;

    public static readonly float maxTimeDaySeconds = 30.0f;

    public static readonly float foodTimeToExpireInSeconds = 5.0f;

    public static readonly int daysForLevelOne = 5;

    public static readonly int daysForLevelTwo = 10;

    public static readonly int daysForLevelThree = 15;

    // ========================= Size Birds =================================
    public static readonly int smallBird = 1;
    public static readonly int mediumBird = 1;
    public static readonly int bigBird = 1;


    // ========================= Health Constants ==========================
    public static readonly int chanceOfSairaSorte = 5;

    public static readonly int chanceLvlTwoBirdOne = 70;

    public static readonly int chanceLvlThreeBirdOne = 40;

    public static readonly int chanceLvlThreeBirdTwo = 70;

    // ========================= Health Constants ==========================
    public static readonly float multiplyHealthPerBirdExistent = 1f;

    public static readonly float multiplyHealthPerBirdThatHasPass = 0.5f;

    public static readonly float maxHealthForComedouro = 100f;


    // ========================= Social Constants ==========================
    public static readonly float maxSocialForComedouro = 100f;

    public static readonly int numBirdsSocialGoodMin = 3;

    public static readonly int numBirdsSocialGoodMax = 5;

    public static readonly int maxNumberBirds = 10;

    public static readonly float decreaseSocialByNumBirds = 2.0f;

    public static readonly float increaseSocialByNumBirds = 1.0f;

    public static readonly float multiplySocialForBadHealth = 0.5f;
}
