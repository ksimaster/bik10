using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextScript : MonoBehaviour
{

    private int countMasha = 0;
    private string [] mashaPositive = { "������!", "���! ��� �������! �����!", "��� ���� �����! �� �����!" };
    private string [] mashaNeitral = { "���!", "���������, �� ��� �� ��...", "������ ������� ������..." };
    private string [] mashaNegative = { "�� ������!", "�����! ��� �����-�� ����! ", "�������! � ������, ��� ��� � ����� ����� �� ����..." };



    private string[] playerMashaPositive = { "������!", "����� �������� � �����!", "��� ���� �����������, ������?" };
    private string[] playerMashaNeitral = { "�������!", "����� �������� ������!", "������������� ������� ��������� ������� � ������������..." };
    private string[] playerMashaNegative = { "������, �����-���������!", "������ ����� �� ��������? ", "��� �� �����, �� ������� ���������� � ��������!" };


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
        // ������� �� ������������ count
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
