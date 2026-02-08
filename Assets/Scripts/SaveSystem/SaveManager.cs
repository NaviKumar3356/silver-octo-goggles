using UnityEngine;

public static class SaveManager
{
    private const string SCORE_KEY = "SCORE";

    public static void Save(int score)
    {
        PlayerPrefs.SetInt(SCORE_KEY, score);
        PlayerPrefs.Save();
    }

    public static int Load()
    {
        return PlayerPrefs.GetInt(SCORE_KEY, 0);
    }
}
