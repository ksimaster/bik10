using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextScript : MonoBehaviour
{

    private int countMasha = 0;
    private string [] mashaPositive = { "Привет!", "Вау! Это классно! Давай!", "Это было супер! Ты супер!" };
    private string [] mashaNeitral = { "Хай!", "Прикольно, но это не моё...", "Вообще никаких эмоций..." };
    private string [] mashaNegative = { "Ну привет!", "Кринж! Это какая-то дичь! ", "Спасибо! Я поняла, что мне в жизни точно не надо..." };



    private string[] playerMashaPositive = { "Привет!", "Давай погуляем в парке!", "Как тебе понравилось, солнце?" };
    private string[] playerMashaNeitral = { "Здорово!", "Давай запустим ракету!", "Потенциальная энергия полностью перешла в кинетическую..." };
    private string[] playerMashaNegative = { "Привет, Машка-Таракашка!", "Пойдёшь ночью на кладбище? ", "Зря не пошла, мы классно оторвались с ребятами!" };


    public TMP_Text textMasha;
    public TMP_Text textPlayerMashaPositive;
    public TMP_Text textPlayerMashaNeitral;
    public TMP_Text textPlayerMashaNegative;

    void Start()
    {
        textPlayerMashaPositive.text = playerMashaPositive[countMasha];
        textPlayerMashaNeitral.text = playerMashaNeitral[countMasha];
        textPlayerMashaNegative.text = playerMashaNegative[countMasha];
    }

    void Update()
    {
        // ловушка на переполнение count
    }

   public void ReceiveMessagePlayer(int typeMessage)
   {
        switch (typeMessage)
        {
            case 0:
                textMasha.text = mashaPositive[countMasha];
                countMasha += 1;
                textPlayerMashaPositive.text = playerMashaPositive[countMasha];
                textPlayerMashaNeitral.text = playerMashaNeitral[countMasha];
                textPlayerMashaNegative.text = playerMashaNegative[countMasha];
                break;
            case 1:
                textMasha.text = mashaNeitral[countMasha];
                countMasha += 1;
                textPlayerMashaPositive.text = playerMashaPositive[countMasha];
                textPlayerMashaNeitral.text = playerMashaNeitral[countMasha];
                textPlayerMashaNegative.text = playerMashaNegative[countMasha];
                break;
            case 2:
                textMasha.text = mashaNegative[countMasha];
                countMasha += 1;
                textPlayerMashaPositive.text = playerMashaPositive[countMasha];
                textPlayerMashaNeitral.text = playerMashaNeitral[countMasha];
                textPlayerMashaNegative.text = playerMashaNegative[countMasha];
                break;
        }

   }

    public void SendMessagePlayer(int typeMessage)
    {
        switch (typeMessage)
        {
            case 0:
                textPlayerMashaPositive.text = playerMashaPositive[countMasha];
                ReceiveMessagePlayer(0);
                break;
            case 1:
                textPlayerMashaNeitral.text = playerMashaNeitral[countMasha];
                ReceiveMessagePlayer(1);
                break;
            case 2:
                textPlayerMashaNegative.text = playerMashaNegative[countMasha];
                ReceiveMessagePlayer(2);
                break;
        }

    }

}
