using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{

    // Rewrite

    // Start
        // Match selected color for color 1 & 2 from player data
        // Update "selected" text

    // Have array with all color "buttons"
    // Update color function (takes color# and which color button)
        // gives where in array to get color from
        // get color from button image in array
        // set color in playerData to set color

    // TODO: use colorblind friendly colors


    public Color[] ColorSelection; // Array of color buttons
    public GameObject[] Buttons1;
    public GameObject[] Buttons2;
    public GameObject purchaseWindow; // Popup that asks user if they want to purchase color
    public Text purchaseText; // Text to edit to tell user how many coins they are spending
    public GameObject notEnoughCoinsWindow; // Popup that notifies user they do not have enough coins for color
    public Text notEnoughCoinsText; // Text to edit to tell user how many coins short they are
    int Color1Index = 0;
    int Color2Index = 1;

    private int purchaseIndex = 0;


    void Start()
    {
        SetButtonColorsAndPrices();
        SetPrimarySecondaryFromData();
        PopupWindowsClosed();
    }

    // TODO: merge into a single loop
    void SetButtonColorsAndPrices()
    {
        int cost = 0;
        if (ColorSelection.Length < Buttons1.Length || ColorSelection.Length < Buttons2.Length)
        {
            Debug.LogError("Insufficent Colors Specified in Color Selection Menu");
        }
        for(int i = 0; i < Buttons1.Length; ++i)
        {
            Buttons1[i].GetComponent<Image>().color = ColorSelection[i];
            cost = PlayerData.GetColorCost(i);
            if (cost != 0)
            {
                Buttons1[i].GetComponentInChildren<Text>().text = cost.ToString() + " ¢";

            }
        }
        for(int j = 0; j < Buttons2.Length; ++j)
        {
            Buttons2[j].GetComponent<Image>().color = ColorSelection[j];
            cost = PlayerData.GetColorCost(j);
            if (cost != 0)
            {
                Buttons2[j].GetComponentInChildren<Text>().text = cost.ToString() + " ¢";

            }
        }
    }

    void SetPrimarySecondaryFromData()
    {
        for (int c = 0; c < ColorSelection.Length; ++c)
        {
            if (ColorSelection[c] == PlayerData.GetColor1())
            {
            _SetBothButtons(c, "Primary");
            Color1Index = c;
            } else
            if (ColorSelection[c] == PlayerData.GetColor2())
            {
            _SetBothButtons(c, "Secondary");
            Color2Index = c;
            }
        }
    }


    // TODO: combine to one function via second event trigger
    // Handles setting the primary color, and displaying popup windows for unpurchased colors
    public void SetColor1(int buttonIndex)
    {
        int price = CheckColorPrice(buttonIndex);
        if (price == 0) // Color already purchased
        {
            if (buttonIndex != Color2Index)
            {
                _SetBothButtons(Color1Index, "");
                Color1Index = buttonIndex;
                PlayerData.SetColor1(ColorSelection[buttonIndex]);
                _SetBothButtons(buttonIndex, "Primary");
            }
        } else
        if (price < 0) // Not enough coins to purchase
        {
            notEnoughCoinsWindow.SetActive(true);
            notEnoughCoinsText.text = "You need " + Mathf.Abs(price) + " ¢ to purchase color";
        } else // enough coins to purchase
        {
            purchaseWindow.SetActive(true);
            purchaseText.text = "Would you like to buy color for " + price + " ¢?";
            purchaseIndex = buttonIndex;
        }

    }

    // Handles setting the secondary color, and displaying popup windows for unpurchased colors
    public void SetColor2(int buttonIndex)
    {
        int price = CheckColorPrice(buttonIndex);
        if (price == 0)
        {
            if (buttonIndex != Color1Index)
            {
                _SetBothButtons(Color2Index, "");
                Color2Index = buttonIndex;
                PlayerData.SetColor2(ColorSelection[buttonIndex]);
                _SetBothButtons(buttonIndex, "Secondary");
            }
        } else
        if (price < 0)
        {
            notEnoughCoinsWindow.SetActive(true);
            notEnoughCoinsText.text = "You need " + Mathf.Abs(price) + " ¢ to purchase color";
        } else
        {
            purchaseWindow.SetActive(true);
            purchaseText.text = "Would you like to buy color for " + price + " ¢?";
            purchaseIndex = buttonIndex;
        }

    }


    // Returns one of three ints (positive means able to buy, negative means not enough coins, zero means already purchased)
    public static int CheckColorPrice(int colorIndex)
    {
        int costForColor = PlayerData.GetColorCost(colorIndex);
        int currCoins = PlayerData.GetCoins();
        if (costForColor == 0)
        {
            return 0;
        } else
        if (costForColor <= currCoins)
        {
            return costForColor;
        } else
        {
            return currCoins - costForColor;
        }
    }

    public void PurchaseColor()
    {
        PlayerData.AddCoins(-(PlayerData.GetColorCost(purchaseIndex)));
        PlayerData.PurchaseColor(purchaseIndex);
        _SetBothButtons(purchaseIndex, "");
        ClosePurchaseWindow();
    }

    public void ClosePurchaseWindow()
    {
        purchaseWindow.SetActive(false);
        purchaseIndex = 0;

    }

    public void CloseNotEnoughCoinsWindow()
    {
        notEnoughCoinsWindow.SetActive(false);
    }

    public void PopupWindowsClosed()
    {
        notEnoughCoinsWindow.SetActive(false);
        purchaseWindow.SetActive(false);
    }

    private void _SetBothButtons(int index, string message)
    {
        Buttons1[index].GetComponentInChildren<Text>().text = message;
        Buttons2[index].GetComponentInChildren<Text>().text = message;
    }

}
