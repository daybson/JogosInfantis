using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CardSpanwer : MonoBehaviour
{
    public List<GameObject> cardsPrefabs;

    public int currentLevel;

    public List<GameObject> grids;


    public void ShowCardsGrid(int level)
    {
        DeactivateChildren();

        grids[currentLevel].SetActive(false);

        currentLevel = level;

        grids[level].SetActive(true);


        switch (level)
        {
            case 0: SelectCards(2); break;
            case 1: SelectCards(3); break;
        }


        for (int i = 0; i < grids[level].transform.childCount; i++)
        {
            var x = Random.Range(0, grids[level].transform.childCount);
            var y = Random.Range(0, grids[level].transform.childCount);

            while (x == y)
            {
                y = Random.Range(0, grids[level].transform.childCount);
            }

            grids[level].transform.GetChild(i).SetSiblingIndex(y);
            grids[level].transform.GetChild(y).SetSiblingIndex(x);
        }


    }


    private void DeactivateChildren()
    {
        foreach (var c in FindObjectsOfType<Card>().ToList())
        {
            c.transform.SetParent(null);
            Destroy(c.gameObject);
        }
    }

    private void SelectCards(int uniqueCardsCount)
    {


        var selected = new List<int>();


        for (int i = 0; i < uniqueCardsCount; i++)
        {
            var cardIndex = Random.Range(0, cardsPrefabs.Count);

            if (selected.Contains(cardIndex))
            {
                i--;
                continue;
            }

            selected.Add(cardIndex);
        }

        foreach (var c in selected)
        {
            var c1 = Instantiate(cardsPrefabs[c]);
            var c2 = Instantiate(cardsPrefabs[c]);

            c1.transform.SetParent(grids[currentLevel].transform);
            c2.transform.SetParent(grids[currentLevel].transform);

            c1.transform.localScale = Vector3.one;
            c2.transform.localScale = Vector3.one;
        }
    }

    /*
    public List<GameObject> cards;

    public GridLayoutGroup gridLayout;
    public CanvasScaler canvasScaler;

    public int totalCards;


    public void Spawn()
    {
        foreach (var c in cards)
        {
            c.transform.SetParent(null);
            c.gameObject.SetActive(false);
            DestroyImmediate(c);
        }

        cards.Clear();

        for (var i = 0; i < totalCards; i++)
        {
            var c1 = Instantiate(prefabs[i]);
            var c2 = Instantiate(prefabs[i]);

            cards.Add(c1);
            cards.Add(c2);

            c1.transform.SetParent(gridLayout.transform);
            c2.transform.SetParent(gridLayout.transform);

            c1.transform.localScale = Vector3.one;
            c2.transform.localScale = Vector3.one;
        }
    }

    public void AdjustGrid(int level)
    {
        totalCards = level * 2;
        print("Total cards  " + totalCards);

        if (level == 1)
            gridLayout.constraintCount = 2;
        else
            gridLayout.constraintCount = level + 1;

        var res = canvasScaler.referenceResolution;
        var columns = gridLayout.constraintCount;
        var cellSize = gridLayout.cellSize;

        var spacing = gridLayout.spacing;
        var paddingLeft = (res.x - columns * cellSize.x - (columns - 1) * spacing.x) / 2;

        gridLayout.padding.left = (int)paddingLeft;

        var rows = (totalCards / columns) + 1;

        var paddingTop = (res.y - rows * cellSize.y - (rows - 1) * spacing.y) / 2;
        gridLayout.padding.top = (int)paddingTop;

        Spawn();
    }

    void GetColumnAndRow(GridLayoutGroup glg, out int column, out int row)
    {
        column = 0;
        row = 0;

        if (glg.transform.childCount == 0)
            return;

        //Column and row are now 1
        column = 1;
        row = 1;

        //Get the first child GameObject of the GridLayoutGroup
        RectTransform firstChildObj = glg.transform.GetChild(0).GetComponent<RectTransform>();

        Vector2 firstChildPos = firstChildObj.localPosition;
        bool stopCountingRow = false;

        //Loop through the rest of the child object
        for (int i = 1; i < glg.transform.childCount; i++)
        {
            //Get the next child
            RectTransform currentChildObj = glg.transform.GetChild(i).GetComponent<RectTransform>();

            Vector2 currentChildPos = currentChildObj.localPosition;

            //if first child.x == otherchild.x, it is a column, ele it's a row
            if (firstChildPos.x == currentChildPos.x)
            {
                column++;
                //Stop couting row once we find column
                stopCountingRow = true;
            }
            else
            {
                if (!stopCountingRow)
                    row++;
            }
        }
    }
    */
}
