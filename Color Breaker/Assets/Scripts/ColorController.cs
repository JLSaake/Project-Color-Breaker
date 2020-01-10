using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{
    /*
        Controller responsible for handling Color purchasing,
        and selection (and its UI) in the main menu scene.
    */

    #region Public variables

    [Header("Color Buttons")]
    [Tooltip("Array of colors for gameplay, must be greater than or equal to number of buttons")]
    public Color[] ColorSelection; // Array of color buttons
    [Tooltip("Array of button gameobjects for selecting primary color")]
    public GameObject[] Buttons1;
    [Tooltip("Array of button gameobjects for selecting secondary color")]
    public GameObject[] Buttons2;

    [Header("Purchase Windows")]
    [Tooltip("Popup gameobject for confirming color purchase")]
    public GameObject purchaseWindow; // Popup that asks user if they want to purchase color
    [Tooltip("Text object for displaying purchase text and price")]
    public Text purchaseText; // Text to edit to tell user how many coins they are spending
    [Tooltip("Popup gameobject for notifying player they do not have enough coins to purchase color")]
    public GameObject notEnoughCoinsWindow; // Popup that notifies user they do not have enough coins for color
    [Tooltip("Text object for displaying how many coins the player needs to purchase color")]
    public Text notEnoughCoinsText; // Text to edit to tell user how many coins short they are

    #endregion

    #region Helper variables
    
    private int Color1Index = 0; // Local variable for primary color seletion index
    private int Color2Index = 1; // Local variable for secondary color selection index
    private int purchaseIndex = 0; // Local variable for index of color being purchased
    
    #endregion

    // Initialize colors and prices, and start all popups closed
    void Start()
    {
        SetButtonColorsAndPrices();
        SetPrimarySecondaryFromData();
        PopupWindowsClosed();
    }

    #region Set UI Elements

    // Set the color and price for each button in the UI from save data
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

    // Set the text for the primary and secondary colors to display in the UI
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

    #endregion

    #region Purchase and window management

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

    // Purchasing the selected color
    public void PurchaseColor()
    {
        PlayerData.AddCoins(-(PlayerData.GetColorCost(purchaseIndex)));
        PlayerData.PurchaseColor(purchaseIndex);
        _SetBothButtons(purchaseIndex, "");
        ClosePurchaseWindow();
    }

    // Close the purchase window
    public void ClosePurchaseWindow()
    {
        purchaseWindow.SetActive(false);
        purchaseIndex = 0;

    }

    // Close the cannot purchase window
    public void CloseNotEnoughCoinsWindow()
    {
        notEnoughCoinsWindow.SetActive(false);
    }

    // Close all purchase windows
    public void PopupWindowsClosed()
    {
        notEnoughCoinsWindow.SetActive(false);
        purchaseWindow.SetActive(false);
    }

    // Helper function, sets text value for button in both Primary and Secondary UI elements
    private void _SetBothButtons(int index, string message)
    {
        Buttons1[index].GetComponentInChildren<Text>().text = message;
        Buttons2[index].GetComponentInChildren<Text>().text = message;
    }

    #endregion

}
