using UnityEngine;

public static class GameConstants
{
    // ========================= Timers Constants ===========================
    public static readonly float oneHourInSeconds = 1.0f;

    public static readonly float timerToCheckInSeconds = 3.0f;

    public static readonly float maxTimeDaySeconds = 15.0f;

    public static readonly float foodTimeToExpireInSeconds = 10f;

    public static readonly int daysForLevelOne = 5;

    public static readonly int daysForLevelTwo = 10;

    public static readonly int daysForLevelThree = 15;

    // ========================= Size Birds =================================
    public static readonly int smallBird = 1;
    public static readonly int mediumBird = 1;
    public static readonly int bigBird = 1;


    // ========================= Health Constants ==========================
    public static readonly int chanceOfSanhaco = 5;

    public static readonly int chanceLvlTwoBirdOne = 70;

    public static readonly int chanceLvlThreeBirdOne = 40;

    public static readonly int chanceLvlThreeBirdTwo = 70;

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
