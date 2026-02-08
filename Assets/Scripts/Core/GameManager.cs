using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public int rows = 2;
    public int columns = 3;
    public Transform gridRoot;
    public CardView cardPrefab;

    [Header("References")]
    public UIManager uiManager;
    public AudioSource audioSource;
    public AudioClip flipSound, matchSound, mismatchSound, gameOverSound;

    private List<CardView> flippedCards = new List<CardView>();
    private List<CardView> allCards = new List<CardView>();

    private int score;
    private int matchedPairs;

    void Start()
    {
        LoadGame();
        GenerateGrid();
        uiManager.UpdateScore(score);
    }

    void GenerateGrid()
    {
        int totalCards = rows * columns;
        List<int> ids = new List<int>();

        for (int i = 0; i < totalCards / 2; i++)
        {
            ids.Add(i);
            ids.Add(i);
        }

        Shuffle(ids);

        for (int i = 0; i < ids.Count; i++)
        {
            CardView card = Instantiate(cardPrefab, gridRoot);
            card.Init(ids[i], this);
            allCards.Add(card);
        }
    }

    public void OnCardFlipped(CardView card)
    {
        audioSource.PlayOneShot(flipSound);
        flippedCards.Add(card);

        if (flippedCards.Count >= 2)
            StartCoroutine(CheckMatch());
    }

    IEnumerator CheckMatch()
    {
        CardView a = flippedCards[0];
        CardView b = flippedCards[1];

        yield return new WaitForSeconds(0.4f);

        if (a.CardId == b.CardId)
        {
            a.SetMatched();
            b.SetMatched();

            matchedPairs++;
            score += 100;
            audioSource.PlayOneShot(matchSound);
        }
        else
        {
            a.Hide();
            b.Hide();
            score -= 10;
            audioSource.PlayOneShot(mismatchSound);
        }

        flippedCards.Clear();
        uiManager.UpdateScore(score);
        SaveGame();

        if (matchedPairs == (rows * columns) / 2)
            GameOver();
    }

    void GameOver()
    {
        audioSource.PlayOneShot(gameOverSound);
        uiManager.ShowGameOver();
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = Random.Range(i, list.Count);
            (list[i], list[rnd]) = (list[rnd], list[i]);
        }
    }

    void SaveGame()
    {
        SaveManager.Save(score);
    }

    void LoadGame()
    {
        score = SaveManager.Load();
    }
}
