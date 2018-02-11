using UnityEngine;

public class DeckEditorLayout : MonoBehaviour
{
    public const float WidthCheck = 1199f;

    public Vector2 SortButtonPortraitPosition => new Vector2(15f, -(deckEditorLayout.rect.height + 87.5f));
    public static readonly Vector2 SortButtonLandscapePosition = new Vector2(187.5f, 0f);

    public Vector2 DeckSelectorButtonsPortraitPosition => new Vector2(250f, -(deckEditorLayout.rect.height + 75f));
    public static readonly Vector2 DeckSelectorButtonLandscapePosition = new Vector2(365f, 0f);

    public Vector2 DeckButtonsPortraitPosition => new Vector2(0f, -(deckEditorLayout.rect.height + 87.5f));
    public static readonly Vector2 DeckButtonsLandscapePosition = new Vector2(-650f, 0f);

    public static readonly Vector2 SearchNamePortraitPosition = new Vector2(15f, 450f);
    public static readonly Vector2 SearchNameLandscapePosition = new Vector2(15f, 367.5f);

    public Vector2 ResultsPagePortraitPosition => new Vector2(GetComponent<RectTransform>().rect.width - resultsPage.rect.width, 450f);
    public static readonly Vector2 ResultsPageLandscapePosition = new Vector2(675, 367.5f);

    public RectTransform sortButton;
    public RectTransform deckSelectorButtons;
    public RectTransform deckButtons;
    public RectTransform searchName;
    public RectTransform resultsPage;

    public RectTransform deckEditorLayout;
    public SearchResults searchResults;

    void Start()
    {
        OnRectTransformDimensionsChange();
    }

    void OnRectTransformDimensionsChange()
    {
        if (!gameObject.activeInHierarchy)
            return;

        RectTransform rt = GetComponent<RectTransform>();
        float aspectRatio = rt.rect.width / rt.rect.height;
        deckSelectorButtons.gameObject.SetActive(aspectRatio < 1 || aspectRatio >= 1.5f);
        resultsPage.gameObject.SetActive(aspectRatio < 1 || aspectRatio >= 1.5f);

        sortButton.anchoredPosition = GetComponent<RectTransform>().rect.width < WidthCheck ? SortButtonPortraitPosition : SortButtonLandscapePosition;
        deckSelectorButtons.anchoredPosition = GetComponent<RectTransform>().rect.width < WidthCheck ? DeckSelectorButtonsPortraitPosition : DeckSelectorButtonLandscapePosition;
        deckButtons.anchoredPosition = GetComponent<RectTransform>().rect.width < WidthCheck ? DeckButtonsPortraitPosition : DeckButtonsLandscapePosition;

        searchName.anchoredPosition = GetComponent<RectTransform>().rect.width < WidthCheck ? SearchNamePortraitPosition : SearchNameLandscapePosition;
        resultsPage.anchoredPosition = GetComponent<RectTransform>().rect.width < WidthCheck ? ResultsPagePortraitPosition : ResultsPageLandscapePosition;

        searchResults.CurrentPageIndex = 0;
        searchResults.UpdateSearchResultsPanel();
        CardInfoViewer.Instance.IsVisible = false;
    }
}
