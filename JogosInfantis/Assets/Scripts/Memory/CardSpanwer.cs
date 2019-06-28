using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CardSpanwer : MonoBehaviour
{
    public List<GameObject> cards;

    public GridLayoutGroup gridLayout;
    public CanvasScaler canvasScaler;
    public int totalCards;
    public int rows;

    private void Awake()
    {
        /*List<GameObject> clone = new List<GameObject>();

        for (var i = 0; i < totalCards; i++)
        {
            var indexCard = Random.Range(0, cards.Count);

            clone.Add(Instantiate(cards[indexCard]));
            clone.Add(Instantiate(cards[indexCard]));
        }*/

        AdjustGrid();
    }


    public void AdjustGrid()
    {
        var res = canvasScaler.referenceResolution;
        var columns = gridLayout.constraintCount;
        var cellSize = gridLayout.cellSize;
        var spacing = gridLayout.spacing;
        var paddingLeft = (res.x - columns * cellSize.x - (columns - 1) * spacing.x) / 2;
        gridLayout.padding.left = (int)paddingLeft;
        var paddingTop = (res.y - rows * cellSize.y - (rows - 1) * spacing.y) / 2;
        gridLayout.padding.top = (int)paddingTop;
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


}
