using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardView : MonoBehaviour
{
    [SerializeField] private GameObject front;
    [SerializeField] private GameObject back;

    public int CardId { get; private set; }
    public bool IsMatched { get; private set; }

    private bool isFlipped;
    private GameManager gameManager;

    public void Init(int id, GameManager manager)
    {
        CardId = id;
        gameManager = manager;
        ResetCard();
    }

    public void OnClick()
    {
        if (isFlipped || IsMatched) return;

        isFlipped = true;
        StartCoroutine(Flip(true));
        gameManager.OnCardFlipped(this);
    }

    public IEnumerator Flip(bool showFront)
    {
        yield return new WaitForSeconds(0.15f);
        front.SetActive(showFront);
        back.SetActive(!showFront);
    }

    public void SetMatched()
    {
        IsMatched = true;
    }

    public void Hide()
    {
        isFlipped = false;
        StartCoroutine(Flip(false));
    }

    public void ResetCard()
    {
        IsMatched = false;
        isFlipped = false;
        front.SetActive(false);
        back.SetActive(true);
    }
}
