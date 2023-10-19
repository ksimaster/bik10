using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Reflection;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Discharge")]
    public float timeFromMe;
    public float timeToMe;
    public float timeFoto;
   
    
    public GameObject contentConversation;
    public GameObject rightMessagePrefab;
    public GameObject leftMessagePrefab;
    public GameObject imageLeftMessagePrefab;
    public GameObject noticiaPrefab;
    public float messagePreDelay = 3.0f; // Tempo de atraso entre as mensagens
    public float messagePosDelay = 3.0f; // Tempo de atraso entre as mensagens
    public float messageMaxPreDelay = 1.0f; // max var, for reset after fast load
    public float messageMaxPosDelay = 1.0f; // max var, for reset after fast load

    public TMP_Text statusMaria;
    public Image cabecalho;
    public Image botoesFundo;
    public Image fotoMaria;
    public Sprite [] imageGirl;
   // public Sprite bigImage;

    public Button botao1;
    public Button botao2;
    public Button botao3;

    public GameObject ButtonPanel;

    private TMP_Text textoBotao1;
    private TMP_Text textoBotao2;
    private TMP_Text textoBotao3;


    public Image background;
    public TextMeshProUGUI textMessage;

    public bool animationEnabled = false;
    public float blinkDuration = 3f;
    public float textDuration = 3f;
    public float minAlpha = 0f;
    public float maxAlpha = 1f;

    private float elapsedTime;
    private float elapsedTextTime;
    public GameObject fadeImageObject;
    public Image fadeImage;
    public TMP_Text textoFadeImage;
    public TMP_Text diaFadeImage;
    private bool reverse = false;
    private string posTransicao;

    public TMP_Text dia;
    private string novoDia;

    public GameObject telasTransicao;
    public Image notificacaoDia1;
    public Image notificacaoDia2;
    public Image notificacaoDia22;
    public Image notificacaoDia3;
    public Image notificacaoDia32;
    public Image notificacaoDia4;
    public Image notificacaoDia5;
    public Image notificacaoDia6;
    public Image notificacaoDia7;
    public Button botaoTransicao;
    public Button botaoTransicao2;
    public TMP_Text textoBotaoTransicao2;

    public AudioSource musicAudioSource;
    public Image SomOn;
    public Image SomOff;

    //for save
    
    private int numberPoint;
    private string saveKey = "Maria";

    void Start()
    {
        textoBotao1 = botao1.GetComponentInChildren<TMP_Text>();
        textoBotao2 = botao2.GetComponentInChildren<TMP_Text>();
        textoBotao3 = botao3.GetComponentInChildren<TMP_Text>();

        SpriteRenderer cabecalhoSpriteRenderer = cabecalho.GetComponent<SpriteRenderer>();
        SpriteRenderer botoesFundoSpriteRenderer = botoesFundo.GetComponent<SpriteRenderer>();

        //Quarta Feira 22:40
        novoDia = "Qua 22:40";
        dia.text = novoDia;

        //StartCoroutine(Chat1());
        Load();
        StartCoroutine(CheckPointCorutine(numberPoint));
    }

    void Update()
    {
        UpdateTransition();
    }

    private void Load()
    {
        var data = SaveManager.Load<SaveData.PlayerProfile>(saveKey);
        numberPoint = data.checkPoint;
    }
    private void Save()
    {
        SaveManager.Save(saveKey, GetSaveData());
    }

    private SaveData.PlayerProfile GetSaveData()
    {
        var data = new SaveData.PlayerProfile()
        {
            
            checkPoint = numberPoint
        };

        return data;
    }

       public IEnumerator CheckPointCorutine(int numberCheckPoint)
       {

           switch (numberCheckPoint)
           {

             case 0:
                   messagePreDelay = 0.0001f;
                   messagePosDelay = 0.0001f;
                   yield return Chat1();
                   messagePreDelay = messageMaxPreDelay;
                   messagePosDelay = messageMaxPosDelay;

                   break;
               case 1:
                   messagePreDelay = 0.0001f;
                   messagePosDelay = 0.0001f;
                   yield return Chat1();
                   yield return chat2();
                   messagePreDelay = messageMaxPreDelay;
                   messagePosDelay = messageMaxPosDelay;
                   break;
               case 2:
                   messagePreDelay = 0.0001f;
                   messagePosDelay = 0.0001f;
                   yield return Chat1();
                   yield return chat2();
                   yield return chat5();
                   messagePreDelay = messageMaxPreDelay;
                   messagePosDelay = messageMaxPosDelay;
                   break;
                   
}
    }
    

    public IEnumerator Chat1()
    {
        Debug.Log("comecou");
        dia.text = "22:40";

        setButtonOptionsAndShow(
            "Привет! Миша сказал, что ты хочешь стать моделью. Это правда?", () => StartCoroutine(chat2()),
            "CLOSE2", () => StartCoroutine(chat2()),
            "CLOSE3", () => StartCoroutine(chat2()));
        yield return createNewMessageFromMe("");     
    }

    public IEnumerator chat2()
    {
        Debug.Log("Chat2");
        ButtonPanel.SetActive(false);
        yield return createNewMessageFromMe("Привет! Миша сказал, что ты хочешь стать моделью. Это правда?");
        yield return createNewMessageFromYou("Привет! Да, правда");
       
        setButtonOptionsAndShow(
    "Тогда я тот, кто может тебе помочь!...", () => StartCoroutine(chat3()),
    "Это классно! Ты занималась где-то?", () => StartCoroutine(chat34()),
    "CLOSE3", () => StartCoroutine(chat4()));
        numberPoint = 1;
        Save();
    
    }

    public IEnumerator chat3()
    {
        Debug.Log("Chat3");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Тогда я тот, кто может тебе помочь!");
        yield return createNewMessageFromMe("Я фотограф и работаю с различными агентствами :)");
        yield return createNewMessageFromYou("Это, конечно, прикольно. Но как? У меня нет денег на фотосессию.");
        yield return createNewMessageFromYou("Или это такой подкат?");

        setButtonOptionsAndShow(
            "Денег не потребуется...", () => StartCoroutine(chat4()),
            "Красивая девушка может расплатиться и иначе!...", () => StartCoroutine(chat5()),
            "CLOSE3", () => StartCoroutine(chat7())
        );
    }

    public IEnumerator chat4()
    {
        Debug.Log("Chat4");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Денег не потребуется.");
        yield return createNewMessageFromMe("Мне нужна модель для конкурса фотографов");
        yield return createNewMessageFromYou("А что я получу?");
       

        setButtonOptionsAndShow(
            "Ты проведёшь время с шикарным мужчиной!...", () => StartCoroutine(chat6()),
            "Ты получишь фотосессию и я отправлю её в агентства...", () => StartCoroutine(chat7()),
            "CLOSE3", () => StartCoroutine(chat7())
        );
    }

    public IEnumerator chat5()
    {
        Debug.Log("Chat5");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Красивая девушка может расплатиться и иначе!");
        yield return createNewMessageFromMe("К примеру, общением");
        yield return createNewMessageFromYou("В смысле ''общением''? ");
        yield return createNewMessageFromYou("Ты что имеешь ввиду?");
        numberPoint = 2;
        Save();

        setButtonOptionsAndShow(
           "Мы можем потанцевать!", () => StartCoroutine(chat24()),
           "Можем сходить куда-нибудь вместе!", () => StartCoroutine(chat30()),
           "CLOSE3", () => StartCoroutine(chat10())
         );
    }

    public IEnumerator chat6()
    {
        Debug.Log("Chat6");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Ты проведёшь время с шикарным мужчиной!");
        yield return createNewMessageFromMe("А я тебя буду фотографировать!");
        yield return createNewMessageFromMe("И не только..");
        yield return createNewMessageFromYou("Значит всё таки подкат...");
        yield return createNewMessageFromYou("Как же я устала от вас...");


        setButtonOptionsAndShow(
          "CLOSE1", () => StartCoroutine(chat8()),
          "CLOSE2", () => StartCoroutine(chat9()),
          "CLOSE3", () => StartCoroutine(chat10())
        );
    }

    public IEnumerator chat7()
    {
        Debug.Log("Chat7");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Ты получишь фотосессию и я отправлю её в агентства. Тебе это интересно?");
        yield return createNewMessageFromMe("Тебе это интересно?");
        yield return createNewMessageFromYou("Да, но я тебя не знаю и это всё так неожиданно...");
       

        setButtonOptionsAndShow(
          "Я понимаю. Однако ты ничего не теряешь?", () => StartCoroutine(chat8()),
          "CLOSE2", () => StartCoroutine(chat9()),
          "CLOSE3", () => StartCoroutine(chat10())
        );
    }

    public IEnumerator chat8()
    {
        Debug.Log("Chat8");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Я понимаю. Однако ты ничего не теряешь?");
        yield return createNewMessageFromMe("И более того. Приобретаешь фотосессию даром!");
        yield return createNewMessageFromMe("Круто? Что скажешь?");
        yield return createNewMessageFromYou("А могу я подумать?");

        setButtonOptionsAndShow(
          "А зачем думать? Говори быстрей!", () => StartCoroutine(chat9()),
          "Конечно, ты можешь думать!...", () => StartCoroutine(chat10()),
          "CLOSE3", () => StartCoroutine(chat16())
        );
    }

    public IEnumerator chat9()
    {
        Debug.Log("Chat9");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("А зачем думать? Говори быстрей");
        yield return createNewMessageFromYou("Не торопи меня");
        yield return createNewMessageFromYou("Что-то мне это не нравится!");

       setButtonOptionsAndShow(
          "Что случилось-то? Нормально же общались!", () => StartCoroutine(chat14()),
          "CLOSE2", () => StartCoroutine(chat15()),
          "CLOSE3", () => StartCoroutine(chat16())
        );
    }

    public IEnumerator chat10()
    {
        Debug.Log("Chat10");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Конечно, ты можешь думать!");
        yield return createNewMessageFromMe("Вот только о чём? Идти к мечте или остаться на месте?");
        yield return createNewMessageFromMe("Выбор всегда за тобой!");
        yield return createNewMessageFromYou("Хорошо. Я согласна!");
        yield return createNewMessageFromYou("Что от меня требуется?");

        setButtonOptionsAndShow(
          "Отлично! Снимать мы будем на улице.", () => StartCoroutine(chat16()),
          "CLOSE2", () => StartCoroutine(chat15()),
          "CLOSE3", () => StartCoroutine(chat16())
        );
    }

    public IEnumerator chat14()
    {
        Debug.Log("Chat14");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Что случилось-то? Нормально же общались!");
        yield return createNewMessageFromYou("Я не люблю, когда меня торопят!");

        setButtonOptionsAndShow(
        "Я не тороплю, я хочу сделать дело!", () => StartCoroutine(chat15()),
        "CLOSE2", () => StartCoroutine(chat18()),
        "CLOSE3", () => StartCoroutine(chat18())
        );
    }

    public IEnumerator chat15()
    {
        Debug.Log("Chat15");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Я не тороплю, я хочу сделать дело!");
        yield return createNewMessageFromYou("Не");
        yield return createNewMessageFromYou("Наверно без меня");
    

        setButtonOptionsAndShow(
        "CLOSE1", () => StartCoroutine(chat19()),
        "CLOSE2", () => StartCoroutine(chat20()),
        "CLOSE3", () => StartCoroutine(chat20())
        );
    }

    public IEnumerator chat16()
    {
        Debug.Log("Chat16");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Отлично! Снимать мы будем на улице.");
        yield return createNewMessageFromMe("Мне потребуется взять разные фильтры.");
        yield return createNewMessageFromMe("Есть возможность скинуть какие-то фото?");
        yield return createNewMessageFromMe("Желательно летом с пляжа.");
        yield return createNewMessageFromYou("Есть вот такое!");
        yield return createImageFromYou(1, "");
        yield return createNewMessageFromYou("Подойдёт?");


        setButtonOptionsAndShow(
        "Супер! А есть чисто в купальнике?", () => StartCoroutine(chat17()),
        "CLOSE2", () => StartCoroutine(chat31()),
        "CLOSE3", () => StartCoroutine(chat32())
        );
    }

    public IEnumerator chat17()
    {
        Debug.Log("Chat17");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Супер! А есть чисто в купальнике?");
        yield return createNewMessageFromMe("Там как раз конкурс такой.");
        yield return createNewMessageFromYou("Да, вот!");
        yield return createImageFromYou(2, "");


        setButtonOptionsAndShow(
        "Вау! Ты просто обворожительна!", () => StartCoroutine(chat18()),
        "CLOSE2", () => StartCoroutine(chat31()),
        "CLOSE3", () => StartCoroutine(chat32())
        );
    }

    public IEnumerator chat18()
    {
        Debug.Log("Chat18");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Вау! Ты просто обворожительна!");
        yield return createNewMessageFromMe("И ты почти полностью повторила позу!");
        yield return createNewMessageFromMe("Это говорит, что у тебя огромный потенциал!");
        yield return createNewMessageFromYou("Спасибо!");
        yield return createNewMessageFromYou("Приятно слышать такое от профи!");


        setButtonOptionsAndShow(
        "Я уверен, что ты даже с различными позами справишься!", () => StartCoroutine(chat19()),
        "CLOSE2", () => StartCoroutine(chat31()),
        "CLOSE3", () => StartCoroutine(chat32())
        );


    }

    public IEnumerator chat19()
    {
        Debug.Log("Chat19");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Я уверен, что ты даже с различными позами справишься!");
        yield return createNewMessageFromMe("Ты буквально держишь того, кто смотрит на тебя!");
        yield return createNewMessageFromYou("Да, вот смотри!");
        yield return createImageFromYou(3, "");
        yield return createNewMessageFromYou("Это подруга меня фотографировала.");


        setButtonOptionsAndShow(
         "Просто шикарно!", () => StartCoroutine(chat20()),
         "CLOSE2", () => StartCoroutine(chat24()),
         "CLOSE3", () => StartCoroutine(chat24())
         );
    }

    public IEnumerator chat20()
    {
        Debug.Log("Chat20");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Просто шикарно!");
        yield return createNewMessageFromMe("У тебя гигантский потенциал и я уверен у нас получится классно поработать вместе!");
        yield return createNewMessageFromYou("Да, я тоже!");
        yield return createNewMessageFromYou("Когда съемка?");


        setButtonOptionsAndShow(
         "Сейчас отправлю заявку на конкурс! Как только её одобрят, начнем! Я тебе напишу!", () => StartCoroutine(chat23()),
         "CLOSE2", () => StartCoroutine(chat24()),
         "CLOSE3", () => StartCoroutine(chat24())
         );
    }

    public IEnumerator chat23()
    {
        Debug.Log("Chat23");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Сейчас отправлю заявку на конкурс!");
        yield return createNewMessageFromMe("Как только её одобрят, начнем!");
        yield return createNewMessageFromMe("Я тебе напишу!");
        yield return createNewMessageFromYou("Хорошо!");
        yield return createNewMessageFromYou("Буду ждать!");
       

        setButtonOptionsAndShow(
        "CLOSE1", () => StartCoroutine(chat30()),
        "CLOSE2", () => StartCoroutine(chat31()),
        "CLOSE3", () => StartCoroutine(chat32())
        );
    }

    public IEnumerator chat24()
    {
        Debug.Log("Chat24");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Мы можем потанцевать!");
        yield return createNewMessageFromMe("Можем посмотреть фильм!");
        yield return createNewMessageFromYou("Без меня!");
        yield return createNewMessageFromYou("Давай самостоятельно! ");
        yield return createNewMessageFromYou("Пока-пока!");
        yield return createNewMessageFromYou(":ь");


        setButtonOptionsAndShow(
        "CLOSE1", () => StartCoroutine(chat30()),
        "CLOSE2", () => StartCoroutine(chat31()),
        "CLOSE3", () => StartCoroutine(chat32())
        );
    }

    public IEnumerator chat30()
    {
        Debug.Log("Chat30");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Можем сходить куда-нибудь вместе!");
        yield return createNewMessageFromMe("В кино!");
        yield return createNewMessageFromMe("В парк!");
        yield return createNewMessageFromMe("Тебе может быть это интересно?");
        yield return createNewMessageFromYou("Возможно.");
        yield return createNewMessageFromYou("Давай сначала сходим, а потом я подумаю по поводу фоток");


        setButtonOptionsAndShow(
       "А может фотку для затравки?", () => StartCoroutine(chat31()),
       "CLOSE2", () => StartCoroutine(chat34()),
       "CLOSE3", () => StartCoroutine(chat35())
       );
    }

    public IEnumerator chat31()
    {
        Debug.Log("Chat31");
        ButtonPanel.SetActive(false);
        yield return createNewMessageFromMe("А может фотку для затравки?");
        yield return createNewMessageFromYou("Хорошо, держи!");
        yield return createImageFromYou(1, "");

        setButtonOptionsAndShow(
       "Вау! А ты реально супер!", () => StartCoroutine(chat32()),
       "CLOSE2", () => StartCoroutine(chat34()),
       "CLOSE3", () => StartCoroutine(chat35())
       );
    }

    public IEnumerator chat32()
    {
        Debug.Log("Chat32");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Вау! А ты реально супер!");
        yield return createNewMessageFromYou("Когда встретимся?");
        setButtonOptionsAndShow(
       "Давай вечером завтра в Центральном парке!", () => StartCoroutine(chat33()),
       "CLOSE2", () => StartCoroutine(chat34()),
       "CLOSE3", () => StartCoroutine(chat35())
       );
    }

    public IEnumerator chat33()
    {
        Debug.Log("Chat33");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Давай вечером завтра в Центральном парке!");
        yield return createNewMessageFromMe("Я тебе точное место позже вышлю!");
        yield return createNewMessageFromYou("Хорошо, жду :)");
        setButtonOptionsAndShow(
       "CLOSE1", () => StartCoroutine(chat33()),
       "CLOSE2", () => StartCoroutine(chat34()),
       "CLOSE3", () => StartCoroutine(chat35())
       );
    }

    public IEnumerator chat34()
    {
        Debug.Log("Chat34");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Это классно! Ты занималась где-то?");
        yield return createNewMessageFromYou("Нет.");
        yield return createNewMessageFromYou("Я собираюсь только идти учиться.");
        setButtonOptionsAndShow(
       "Ты поставила себе цель и стремишься к ней.", () => StartCoroutine(chat35()),
       "Ааа... То есть ты не грамотная и не училась...", () => StartCoroutine(chat45()),
       "CLOSE3", () => StartCoroutine(chat35())
       );
    }

    public IEnumerator chat35()
    {
        Debug.Log("Chat35");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Ты поставила себе цель и стремишься к ней.");
        yield return createNewMessageFromMe("Наверно, уже и фотосессий много прошла?");
        yield return createNewMessageFromMe("Да?");
        yield return createNewMessageFromYou("Нет.");
        yield return createNewMessageFromYou("А почему ты спрашиваешь?");
        setButtonOptionsAndShow(
       "Я подбираю материал для сайта.", () => StartCoroutine(chat35()),
       "Я начинающий фотограф и мне нужна модель. Ты можешь мне в этом помочь?", () => StartCoroutine(chat34()),
       "CLOSE3", () => StartCoroutine(chat35())
       );
    }

    public IEnumerator chat36()
    {
        //sexta feira 13:30
        Debug.Log("Chat36");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Я подбираю материал для сайта.");
        yield return createNewMessageFromMe("Твои фотки мне бы очень пригодились!");
        yield return createNewMessageFromYou("В смысле?");
        yield return createNewMessageFromYou("А что за сайт?");
        setButtonOptionsAndShow(
       "Самые красивые девушки мира!", () => StartCoroutine(chat40()),
       "Да, обычный! Горячие доступные сочные девушки!", () => StartCoroutine(chat44()),
       "CLOSE3", () => StartCoroutine(chat35())
       );
    }

    public IEnumerator chat40()
    {
        Debug.Log("Chat40");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Самые красивые девушки мира!");
        yield return createNewMessageFromMe("Думаю, ты займёшь самый топ!");
        yield return createNewMessageFromYou("Спасибки за комплимент!");
        yield return createNewMessageFromYou("Но фотки я тебе не скину.");
        yield return createNewMessageFromYou(":Ь");

        setButtonOptionsAndShow(
      "Это не только комплимент, это правда!", () => StartCoroutine(chat41()),
      "CLOSE2", () => StartCoroutine(chat44()),
      "CLOSE3", () => StartCoroutine(chat45())
      );
    }

    public IEnumerator chat41()
    {
        Debug.Log("Chat41");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Это не только комплимент, это правда!");
        yield return createNewMessageFromMe("У меня заказ есть от крупного издательства журнала для женщин!");
        yield return createNewMessageFromYou("Ага, а ещё и спортивная машина и ты меня на ней покатаешь! Да?");

        setButtonOptionsAndShow(
      "Нет! Но у меня в планах купить!", () => StartCoroutine(chat42()),
      "CLOSE2", () => StartCoroutine(chat44()),
      "CLOSE3", () => StartCoroutine(chat45())
      );
    }

    public IEnumerator chat42()
    {
        Debug.Log("Chat42");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Нет! Но у меня в планах купить!");
        yield return createNewMessageFromMe("Я бы тебя покатал! Если хочешь, то встретимся и обсудим!");
        yield return createNewMessageFromMe("Если хочешь, то встретимся и обсудим!");
        yield return createNewMessageFromYou("Давай!");
        yield return createNewMessageFromYou("И захвати договор на фотки для сайта, если он, конечно, есть!");
        yield return createNewMessageFromYou("Вот тебе фотка для затравки!");
        yield return createImageFromYou(2, "");

        setButtonOptionsAndShow(
      "Вау! Супер! Я позже скину время и место!", () => StartCoroutine(chat43()),
      "CLOSE2", () => StartCoroutine(chat44()),
      "CLOSE3", () => StartCoroutine(chat45())
      );
    }

    public IEnumerator chat43()
    {
        Debug.Log("Chat43");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Вау! Супер! Я позже скину время и место!");
        yield return createNewMessageFromYou("Хорошо, жду!");


        setButtonOptionsAndShow(
    "CLOSE1", () => StartCoroutine(chat43()),
    "CLOSE2", () => StartCoroutine(chat44()),
    "CLOSE3", () => StartCoroutine(chat45())
);
    }

    public IEnumerator chat44()
    {
        Debug.Log("Chat44");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Да, обычный! Горячие доступные сочные девушки!");
        yield return createNewMessageFromYou("Фу... Мерзость! Не пиши мне!");
        
        setButtonOptionsAndShow(
    "CLOSE1", () => StartCoroutine(chat43()),
    "CLOSE2", () => StartCoroutine(chat44()),
    "CLOSE3", () => StartCoroutine(chat45())
);
    }

    public IEnumerator chat45()
    {
        Debug.Log("Chat45");
        ButtonPanel.SetActive(false);
        
        messagePreDelay = 2.0f;
        yield return createNewMessageFromMe("Ааа... То есть ты не грамотная и не училась!");
        yield return createNewMessageFromYou("В смысле?");
        yield return createNewMessageFromYou("Ты о чём?");
        yield return createNewMessageFromYou("У меня есть образование!");


        setButtonOptionsAndShow(
         "Да ладно, я шучу!", () => StartCoroutine(chat46()),
         "CLOSE2", () => StartCoroutine(chat44()),
         "CLOSE3", () => StartCoroutine(chat45())
         );
    }



    public IEnumerator chat46()
    {
        Debug.Log("Chat46");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Да ладно, я шучу!");
        yield return createNewMessageFromMe("Чего ты какая серьезная?");
        yield return createNewMessageFromYou("Какая есть!");



        setButtonOptionsAndShow(
      "Зачем какая есть? Запах плохой и совмещать с едой как-то... Фууу", () => StartCoroutine(chat47()),
        "CLOSE2", () => StartCoroutine(chat44()),
        "CLOSE3", () => StartCoroutine(chat45())
      );

    }

    public IEnumerator chat47()
    {
        Debug.Log("Chat47");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Зачем какая есть?");
        yield return createNewMessageFromMe("Запах плохой и совмещать с едой как-то... Фууу");
        yield return createNewMessageFromYou("Ты о чём?");


        setButtonOptionsAndShow(
      "Да я всё ещё шучу!", () => StartCoroutine(chat48()),
      "О какашках и еде!", () => StartCoroutine(chat54()),
      "CLOSE3", () => StartCoroutine(chat54())
      );

    }

    public IEnumerator chat48()
    {
        Debug.Log("Chat48");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Да я всё ещё шучу!");
        yield return createNewMessageFromYou("Ааа");
        yield return createNewMessageFromYou("Сам пошутил, сам посмеялся.");
       
        setButtonOptionsAndShow(
      "Ну типо того!", () => StartCoroutine(chat52()),
      "CLOSE2", () => StartCoroutine(chat53()),
      "CLOSE3", () => StartCoroutine(chat54())
      );
    }

    public IEnumerator chat52()
    {
        Debug.Log("Chat52");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Ну типо того!");
        yield return createNewMessageFromMe("Я тут в Смешных анекдотах на Яндекс Играх много прикольного нашёл!");
        yield return createNewMessageFromMe("Но эту шутку сам придумал!");
        yield return createNewMessageFromYou("Оно и видно!");
        yield return createNewMessageFromYou("Ладно, мне некогда!");

        setButtonOptionsAndShow(
      "А мы ещё пообщаемся?", () => StartCoroutine(chat53()),
      "CLOSE2", () => StartCoroutine(chat53()),
      "CLOSE3", () => StartCoroutine(chat54())

      );
    }

    public IEnumerator chat53()
    {
        Debug.Log("Chat53");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("А мы ещё пообщаемся?");
        yield return createNewMessageFromMe("Ты вроде прикольная девчонка!");
        yield return createNewMessageFromYou("Я подумаю... Пока");


        setButtonOptionsAndShow(
      "CLOSE1", () => StartCoroutine(chat55()),
      "CLOSE2", () => StartCoroutine(chat53()),
      "CLOSE3", () => StartCoroutine(chat54())

      );
    }

    public IEnumerator chat54()
    {
        Debug.Log("Chat54");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("О какашках и еде!");
        yield return createNewMessageFromMe("Ты сама начала этот разговор!");
        yield return createNewMessageFromMe("Я только поддерживаю беседу");
        yield return createNewMessageFromYou("Я не начинала! ");
        yield return createNewMessageFromYou("Это ты какую-то чушь несёшь!");

        setButtonOptionsAndShow(
      "А с юмором я вижу проблемы у тебя, да?", () => StartCoroutine(chat55()),
      "CLOSE2", () => StartCoroutine(chat56()),
      "CLOSE3", () => StartCoroutine(chat57())

      );
    }

    public IEnumerator chat55()
    {
        Debug.Log("Chat55");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("А с юмором я вижу проблемы у тебя, да?");
        yield return createNewMessageFromYou("У меня всё хорошо!");
        yield return createNewMessageFromYou("А свои шутки ты на помойке находишь?");

        setButtonOptionsAndShow(
     "Нет!", () => StartCoroutine(chat56()),
     "CLOSE2", () => StartCoroutine(chat62()),
     "CLOSE3", () => StartCoroutine(chat63())

     );
    }

    public IEnumerator chat56()
    {
        Debug.Log("Chat56");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Нет!");
        yield return createNewMessageFromMe("Я на Яндекс Играх смотрю Смешные Демотиваторы!");
        yield return createNewMessageFromMe("Но это я сам придумал!");
        yield return createNewMessageFromYou("Оно и чувствуется!");
        yield return createNewMessageFromYou("На Яндекс Играх такую ерунду не найти!");

        setButtonOptionsAndShow(
     "Ты играешь?", () => StartCoroutine(chat57()),
     "CLOSE2", () => StartCoroutine(chat62()),
     "CLOSE3", () => StartCoroutine(chat63())

     );
    }

    public IEnumerator chat57()
    {
        Debug.Log("Chat57");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Ты играешь?");
        yield return createNewMessageFromYou("Видела несколько игр!");
        yield return createNewMessageFromYou("Но я не фанат игрушек!");


        setButtonOptionsAndShow(
     "Я тоже, но там есть игры на двоих!", () => StartCoroutine(chat61()),
     "CLOSE2", () => StartCoroutine(chat62()),
     "CLOSE3", () => StartCoroutine(chat63())

     );
    }

    public IEnumerator chat61()
    {
        Debug.Log("Cha61");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Я тоже, но там есть игры на двоих!");
        yield return createNewMessageFromMe("Хочешь сыграть?");
        yield return createNewMessageFromYou("Ну давай.");
        yield return createNewMessageFromYou("Всё равно сейчас думала чем время убить");
        setButtonOptionsAndShow(
     "Хорошо, сейчас сброшу ссылку!", () => StartCoroutine(chat62()),
     "CLOSE2", () => StartCoroutine(chat62()),
     "CLOSE3", () => StartCoroutine(chat63())

     );
    }

    public IEnumerator chat62()
    {
        Debug.Log("Cha62");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Хорошо, сейчас сброшу ссылку!");
        yield return createNewMessageFromYou("Хорошо, жду.");
        setButtonOptionsAndShow(
     "CLOSE1", () => StartCoroutine(chat62()),
     "CLOSE2", () => StartCoroutine(chat62()),
     "CLOSE3", () => StartCoroutine(chat63())
     );
    }

    public IEnumerator chat63()
    {
        Debug.Log("Cha63");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Se falemo");
        yield return createNewMessageFromMe("Boa noite");

        textoFadeImage.text = "Hoje foi um dia bom";
        diaFadeImage.text = "Dom 12:40";
        posTransicao = "chat64";
        novoDia = "Dom 12:40";
        animationEnabled = true;
    }

    public IEnumerator chat64()
    {
        //Domingo 12:40
        Debug.Log("Cha64");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("oii");
        yield return createNewMessageFromMe("sobre o teres hj");
        yield return createNewMessageFromMe("pode as 17:00?");
        yield return createNewMessageFromYou("Oii");
        yield return createNewMessageFromYou("Pode ser mais cedo?");
        yield return createNewMessageFromYou("Não posso chegar muito tarde em casa");

        setButtonOptionsAndShow(
     "Ué pq não pode??", () => StartCoroutine(chat65()),
     "Mais cedo que 17:00?? kkkkkk", () => StartCoroutine(chat66()),
     "Pode sim", () => StartCoroutine(chat67())
     );
    }

    public IEnumerator chat65()
    {
        Debug.Log("Cha65");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Ué pq não pode?",true); //apaga
        yield return createNewMessageFromMe("16:00 então?"); // reescreve com esse texto
        yield return createNewMessageFromYou("Acho que esse horário fica massa");
        yield return createNewMessageFromYou("Lá pelas 16:00 chego ai então");
        yield return createNewMessageFromYou("Fica esperto ein😠");
        yield return createNewMessageFromMe("Feshoww");

        textoFadeImage.text = "(TERES)";
        diaFadeImage.text = "Mais tarde no mesmo dia";
        posTransicao = "chat685";
        novoDia = "Dom 22:16";
        animationEnabled = true;
    }

    public IEnumerator chat66()
    {
        Debug.Log("Cha66");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Mais cedo que 17:00? kk",true); //apaga
        yield return createNewMessageFromMe("16:00 então?"); // reescreve com esse texto
        yield return createNewMessageFromYou("Acho que esse horário fica massa");
        yield return createNewMessageFromYou("Lá pelas 16:00 chego ai então");
        yield return createNewMessageFromYou("Fica esperto ein😠");
        yield return createNewMessageFromMe("Feshoww");
        
        textoFadeImage.text = "(TERES)";
        diaFadeImage.text = "Mais tarde no mesmo dia";
        posTransicao = "chat685";
        novoDia = "Dom 22:16";
        animationEnabled = true;
    }

    public IEnumerator chat67()
    {
        Debug.Log("Cha67");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("pode sim"); // NÃO APAGA
        yield return createNewMessageFromMe("16:00 então?"); // reescreve com esse texto
        yield return createNewMessageFromYou("Acho que esse horário fica massa");
        yield return createNewMessageFromYou("Lá pelas 16:00 chego ai então", true);
        yield return createNewMessageFromYou("Fica esperto ein😠");
        yield return createNewMessageFromMe("Feshoww");

        textoFadeImage.text = "(TERES)";
        diaFadeImage.text = "Mais tarde no mesmo dia";
        posTransicao = "chat685";
        novoDia = "Dom 22:16";
        animationEnabled = true;
    }

    public IEnumerator chat685()
    {
        //Domingo 22:15
        Debug.Log("Cha685");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Eai");

        setButtonOptionsAndShow(
     "Resenha demais nosso papo", () => StartCoroutine(chat68()),
     "Meu aquele teres tava muito bom", () => StartCoroutine(chat69()),
     "A mãe adorou conversar contigo ein", () => StartCoroutine(chat70())
     );
    }

    public IEnumerator chat68()
    {
        Debug.Log("Cha68");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Resenha demais nosso papo");

        novoDia = "Seg 19:36";
        animationEnabled = true;
        textoFadeImage.text = "Deve ter dormido";
        diaFadeImage.text = "Seg 19:36";
        posTransicao = "chat715";

    }
    
    public IEnumerator chat69()
    {
        Debug.Log("Cha69");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Meu aquele teres tava muito bom");

        novoDia = "Seg 19:36";
        animationEnabled = true;
        textoFadeImage.text = "Deve ter dormido";
        diaFadeImage.text = "Seg 19:36";
        posTransicao = "chat715";
    }
    
    public IEnumerator chat70()
    {
        Debug.Log("Cha70");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("A mãe adorou conversar contigo ein🥺");

        novoDia = "Seg 19:36";
        animationEnabled = true;
        textoFadeImage.text = "Deve ter dormido";
        diaFadeImage.text = "Seg 19:36";
        posTransicao = "chat715";
    }
    
    public IEnumerator chat715()
    {
        //Segunda 19:36
        Debug.Log("Cha71.5");
        ButtonPanel.SetActive(false);

        /*
        // Define a cor desejada (164, 83, 171, 255) como um objeto Color
            Color novaCor = new Color(164 / 255f, 83 / 255f, 171 / 255f, 1f);

            // Altera a cor dos objetos desejados
            botoesFundo.color = novaCor;
            cabecalho.color = novaCor;
            fotoMaria.color = novaCor;
        */

        cabecalho.GetComponent<Image>().color = new Color32(190,162,162,255);
        botoesFundo.GetComponent<Image>().color = new Color32(190,162,162,255);
        
        yield return createNewMessageFromMe("Mariaa");

        setButtonOptionsAndShow(
     "Ta tudo certo?", () => StartCoroutine(chat71()),
     "Ta viva? kkkj", () => StartCoroutine(chat72()),
     "Alooou", () => StartCoroutine(chat73())
     );
    }
    
    public IEnumerator chat71()
    {
        Debug.Log("Cha71");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Ta tudo certo?");

        novoDia = "Ter 20:07";
        animationEnabled = true;
        textoFadeImage.text = "Que Vácuo   será que aconteceu algo?";
        diaFadeImage.text = "Ter 20:07";
        posTransicao = "chat74";
    }
    
    public IEnumerator chat72()
    {
        Debug.Log("Cha72");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Ta viva? kkkj");

        novoDia = "Ter 20:07";
        animationEnabled = true;
        textoFadeImage.text = "Que Vácuo   será que aconteceu algo?";
        diaFadeImage.text = "Ter 20:07";
        posTransicao = "chat74";
    }
    
    public IEnumerator chat73()
    {
        Debug.Log("Cha73");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Aloou");

        novoDia = "Ter 20:07";
        animationEnabled = true;
        textoFadeImage.text = "Que Vácuo   será que aconteceu algo?";
        diaFadeImage.text = "Ter 20:07";
        posTransicao = "chat74";
    }
    
    public IEnumerator chat74()
    {
        //Terça 20:07
        Debug.Log("Cha74");
        ButtonPanel.SetActive(false);

        cabecalho.GetComponent<Image>().color = new Color32(140,115,115,255);
        botoesFundo.GetComponent<Image>().color = new Color32(140,115,115,255);

        messagePreDelay= 0.000001f;
        yield return createNewMessageFromYou("Oi");
        yield return createNewMessageFromYou("Desculpa não consegui responder");
        yield return createNewMessageFromYou("Ontem e hoje foi bem corrido");

        setButtonOptionsAndShow(
     "Tranquilo", () => StartCoroutine(chat75()),
     "Aconteceu alguma coisa?", () => StartCoroutine(chat79()),
     "Ah, fiquei meio preocupado", () => StartCoroutine(chat80())
     );
    }

    public IEnumerator chat75()
    {
        Debug.Log("Cha75");
        ButtonPanel.SetActive(false);

        messagePreDelay = 2.0f;
        yield return createNewMessageFromMe("Tranquilo");
        yield return createNewMessageFromYou("Mas e vc ta td bem?");

        setButtonOptionsAndShow(
      "To tentando aproveitar as férias", () => StartCoroutine(chat76()),
      "to bem sim", () => StartCoroutine(chat79()),
      "Ah, fiquei meio preocupado", () => StartCoroutine(chat80())
      );
    }

    public IEnumerator chat76() //Aqui vai pular pro próximo dia
    {
        Debug.Log("Cha76");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Simm, to tentando aproveitar os últimos dias de fériaskkkkkk");
        yield return createNewMessageFromYou("E vc?");
        yield return createNewMessageFromYou("To bem também");

        novoDia = "Qui 19:50";
        animationEnabled = true;
        textoFadeImage.text = "Será que ele ta com ciúmes?                         Será que aconteceu alguma coisa?";
        diaFadeImage.text = "Qui 19:50";
        posTransicao = "chat87";
    }

    public IEnumerator chat77() //Aqui vai pular pro próximo dia
    {
        Debug.Log("Cha77");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("To bem sim");
        yield return createNewMessageFromYou("E vc?");
        yield return createNewMessageFromYou("To bem também");

        novoDia = "Qui 19:50";
        animationEnabled = true;
        textoFadeImage.text = "Será que ele ta com ciúmes?                      Será que aconteceu alguma coisa?";
        diaFadeImage.text = "Qui 19:50";
        posTransicao = "chat87";
    }
    
    public IEnumerator chat78()
    {
        Debug.Log("Cha78");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("To mas fiquei meio preocupado contigo");
        yield return createNewMessageFromYou("Só tava ocupada");
        yield return createNewMessageFromYou("O José tava aqui");

        setButtonOptionsAndShow(
     "Tranquilo", () => StartCoroutine(chat84()),
     "Aconteceu alguma coisa?", () => StartCoroutine(chat85()),
     "Ah, fiquei meio preocupado", () => StartCoroutine(chat86())
     );
    }
    
    public IEnumerator chat79()
    {
        Debug.Log("Cha79");
        ButtonPanel.SetActive(false);

        messagePreDelay = 2.0f;
        yield return createNewMessageFromMe("Aconteceu alguma coisa?");
        yield return createNewMessageFromYou("Nada não");
        yield return createNewMessageFromYou("Só tava ocupada");
        yield return createNewMessageFromYou("O josé tava aqui");

        setButtonOptionsAndShow(
     "Tranquilo", () => StartCoroutine(chat84()),
     "Aconteceu alguma coisa?", () => StartCoroutine(chat85()),
     "Ah, fiquei meio preocupado", () => StartCoroutine(chat86())
     );
    }
    
    public IEnumerator chat80()
    {
        Debug.Log("Cha80");
        ButtonPanel.SetActive(false);

        messagePreDelay = 2.0f;
        yield return createNewMessageFromMe("Ah, é que eu fiquei meio preocupado");
        yield return createNewMessageFromYou("Não tem pq se preocupar comigo");

        setButtonOptionsAndShow(
     "Ok", () => StartCoroutine(chat81()),
     "Tem Certeza?", () => StartCoroutine(chat82()),
     "Mas a gente é amigo", () => StartCoroutine(chat83())
     );
    }
    
    public IEnumerator chat81()
    {
        Debug.Log("Cha81");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Ok");
        yield return createNewMessageFromYou("Relaxa, ta td certo");
        yield return createNewMessageFromYou("Só tava ocupada");
        yield return createNewMessageFromYou("O José tava aqui");

        setButtonOptionsAndShow(
     "Se aconteceu algo pode me contar", () => StartCoroutine(chat85()),
     "Ele fez alguma coisa?", () => StartCoroutine(chat86()),
     "Entendi", () => StartCoroutine(chat84())
     );
    }
    
    public IEnumerator chat82()
    {
        Debug.Log("Cha82");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Tem certeza?");
        yield return createNewMessageFromYou("Relaxa, ta td certo");
        yield return createNewMessageFromYou("Só tava ocupada");
        yield return createNewMessageFromYou("O José tava aqui");

        setButtonOptionsAndShow(
     "Se aconteceu algo pode me contar", () => StartCoroutine(chat85()),
     "Ele fez alguma coisa?", () => StartCoroutine(chat86()),
     "Entendi", () => StartCoroutine(chat84())
     );
    }
    
    public IEnumerator chat83()
    {
        Debug.Log("Cha83");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Tem certeza?");
        yield return createNewMessageFromYou("Relaxa, ta td certo");
        yield return createNewMessageFromYou("Só tava ocupada");
        yield return createNewMessageFromYou("O José tava aqui");

        setButtonOptionsAndShow(
     "Se aconteceu algo pode me contar", () => StartCoroutine(chat85()),
     "Ele fez alguma coisa?", () => StartCoroutine(chat86()),
     "Entendi", () => StartCoroutine(chat84())
     );
    }

    public IEnumerator chat84() //Aqui muda de dia
    {
        Debug.Log("Cha84");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Entendi");

        novoDia = "Qui 19:50";
        animationEnabled = true;
        textoFadeImage.text = "Será que ele ta com ciúmes?                           Será que aconteceu alguma coisa?";
        diaFadeImage.text = "Qui 19:50";
        posTransicao = "chat87";
    }

    public IEnumerator chat85() //Aqui muda de dia
    {
        Debug.Log("Cha85");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Se aconteceu algo pode me contar");
        yield return createNewMessageFromYou("Pode deixar");
        yield return createNewMessageFromYou("Mas ta td bem, sério");
        yield return createNewMessageFromMe("Ta né");

        novoDia = "Qui 19:50";
        animationEnabled = true;
        textoFadeImage.text = "Será que ele ta com ciúmes?                   Será que aconteceu alguma coisa?";
        diaFadeImage.text = "Qui 19:50";
        posTransicao = "chat87";
    }
    
    public IEnumerator chat86()// Aqui muda de dia
    {
        Debug.Log("Cha86");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Ele fez alguma coisa?");
        yield return createNewMessageFromYou("???");
        yield return createNewMessageFromYou("Pq vc acha isso?");
        yield return createNewMessageFromMe("Nada não");
        yield return createNewMessageFromMe("desculpa");
        yield return createNewMessageFromYou("Ta ok");

        novoDia = "Qui 19:50";
        animationEnabled = true;
        textoFadeImage.text = "Será que ele ta com ciúmes?                   Será que aconteceu alguma coisa?";
        diaFadeImage.text = "Qui 19:50";
        posTransicao = "chat87";
    }

    public IEnumerator chat87()
    {
        Debug.Log("Cha87");
        ButtonPanel.SetActive(false);

        cabecalho.GetComponent<Image>().color = new Color32(79,65,65,255);
        botoesFundo.GetComponent<Image>().color = new Color32(79,65,65,255);

        yield return createNewMessageFromMe("Oi");
   
        setButtonOptionsAndShow(
     "Como estão as coisas?", () => StartCoroutine(chat88()),
     "Eai de boa?", () => StartCoroutine(chat89()),
     "Tudo certo", () => StartCoroutine(chat90())
     );
    }

    public IEnumerator chat88()
    {
        Debug.Log("Cha88");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Como estão as coisas?");
        yield return createNewMessageFromYou("Oi");
        yield return createNewMessageFromYou("Tranquilo");
        yield return createNewMessageFromMe("Semana que vem to indo embora ja");
        yield return createNewMessageFromMe("CTa afim de sair de novo antes de eu ir?");
        yield return createNewMessageFromMe("Amanhã talvez?");
        yield return createNewMessageFromYou("Cara, amanhã eu não consigo");
        yield return createNewMessageFromYou("Foi mal");

        setButtonOptionsAndShow(
     "Ok", () => StartCoroutine(chat91()),
     "E sábado?", () => StartCoroutine(chat92()),
     "Tudo bem, podemos ver outro dia então", () => StartCoroutine(chat93())
     );
    }

    public IEnumerator chat89()
    {
        Debug.Log("Cha89");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Eai de boas?");
        yield return createNewMessageFromYou("Oi");
        yield return createNewMessageFromYou("Tranquilo");
        yield return createNewMessageFromMe("Semana que vem to indo embora ja");
        yield return createNewMessageFromMe("CTa afim de sair de novo antes de eu ir?");
        yield return createNewMessageFromMe("Amanhã talvez?");
        yield return createNewMessageFromYou("Cara, amanhã eu não consigo");
        yield return createNewMessageFromYou("Foi mal");

        setButtonOptionsAndShow(
     "Ok", () => StartCoroutine(chat91()),
     "E sábado?", () => StartCoroutine(chat92()),
     "Tudo bem, podemos ver outro dia então", () => StartCoroutine(chat93())
     );
    }

    public IEnumerator chat90()
    {
        Debug.Log("Cha90");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Tudo certo?");
        yield return createNewMessageFromYou("Oi");
        yield return createNewMessageFromYou("Tudo sim");
        yield return createNewMessageFromMe("Semana que vem to indo embora ja");
        yield return createNewMessageFromMe("CTa afim de sair de novo antes de eu ir?");
        yield return createNewMessageFromMe("Amanhã talvez?");
        yield return createNewMessageFromYou("Cara, amanhã eu não consigo");
        yield return createNewMessageFromYou("Foi mal");

        setButtonOptionsAndShow(
     "Ok", () => StartCoroutine(chat91()),
     "E sábado?", () => StartCoroutine(chat92()),
     "Tudo bem, podemos ver outro dia então", () => StartCoroutine(chat93())
     );
    }

    public IEnumerator chat91()//Dia acabou
    {
        Debug.Log("Cha91");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Ok");

        novoDia = "Qua 09:16";
        animationEnabled = true;
        textoFadeImage.text = "Tenho certreza que o josé ta privando ela";
        diaFadeImage.text = "Duas semanas depois";
        posTransicao = "chat106";
    }

    public IEnumerator chat92()
    {
        Debug.Log("Cha92");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("E sábado?");
        yield return createNewMessageFromYou("Também não");
        yield return createNewMessageFromYou("Esse fim de semana não da");

        setButtonOptionsAndShow(
     "Tudo bem", () => StartCoroutine(chat94()),
     "Pq?", () => StartCoroutine(chat95()),
     "Tem alguma coisa a ver com o José?", () => StartCoroutine(chat96())
     );
    }

    public IEnumerator chat93()
    {
        Debug.Log("Cha93");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Tudo bem, podemos ver outro dia então");
        yield return createNewMessageFromYou("Sim vamos vendo");
        yield return createNewMessageFromYou("Mas não sei se vou poder");

        setButtonOptionsAndShow(
     "Tudo bem", () => StartCoroutine(chat94()),
     "Pq?", () => StartCoroutine(chat95()),
     "Tem alguma coisa a ver com o José?", () => StartCoroutine(chat96())
     );
    }

    public IEnumerator chat94()//dia acaba
    {
        Debug.Log("Cha94");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Tudo bem");

        novoDia = "Qua 09:16";
        animationEnabled = true;
        textoFadeImage.text = "Tenho certreza que o josé ta privando ela";
        diaFadeImage.text = "Duas semanas depois";
        posTransicao = "chat106";
    }

    public IEnumerator chat95()
    {
        Debug.Log("Cha95");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Pq?");
        yield return createNewMessageFromYou("Tô ocupada com uns trabalhos da facul");
        yield return createNewMessageFromYou("Desculpa mesmo");
        yield return createNewMessageFromYou("Queria muito poder ir");
        yield return createNewMessageFromMe("Você ta diferente");
        yield return createNewMessageFromYou("To?");

        setButtonOptionsAndShow(
     "Do nada tu ficou seca", () => StartCoroutine(chat97()),
     "Sei lá vc ta estranha", () => StartCoroutine(chat98()),
     "Tenho certeza que é seu namorado", () => StartCoroutine(chat99())
     );
    }

    public IEnumerator chat96()
    {
        Debug.Log("Cha96");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Tem lguma coisa a ver com o josé?",true);
        yield return createNewMessageFromMe("Pq?");
        yield return createNewMessageFromYou("Tô ocupada com uns trabalhos da facul");
        yield return createNewMessageFromYou("Desculpa mesmo");
        yield return createNewMessageFromYou("Queria muito poder ir");
        yield return createNewMessageFromMe("Você ta diferente");
        yield return createNewMessageFromYou("To?");

        setButtonOptionsAndShow(
     "Do nada tu ficou seca", () => StartCoroutine(chat97()),
     "Sei lá vc ta estranha", () => StartCoroutine(chat98()),
     "Tenho certeza que é seu namorado", () => StartCoroutine(chat99())
     );
    }
    
    public IEnumerator chat97()
    {
        Debug.Log("Cha97");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Do nada tu ficou seca");
        yield return createNewMessageFromYou("Olha");
        yield return createNewMessageFromYou("Não sei oq vc ta pensando");
        yield return createNewMessageFromYou("Mas eu só to cansada");

        setButtonOptionsAndShow(
     "Se tu diz...", () => StartCoroutine(chat100()),
     "Olha, eu sei que tem algo errado", () => StartCoroutine(chat101()),
     "Eu quero muito te ajudar", () => StartCoroutine(chat102())
     );
    }

    public IEnumerator chat98()
    {
        Debug.Log("Cha98");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Sei la vc ta estranha");
        yield return createNewMessageFromYou("Olha");
        yield return createNewMessageFromYou("Não sei oq vc ta pensando");
        yield return createNewMessageFromYou("Mas eu só to cansada");

        setButtonOptionsAndShow(
     "Se tu diz...", () => StartCoroutine(chat100()),
     "Olha, eu sei que tem algo errado", () => StartCoroutine(chat101()),
     "Eu quero muito te ajudar", () => StartCoroutine(chat102())
     );
    }

    public IEnumerator chat99()
    {
        Debug.Log("Cha99");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Tenho certeza que teu namorado ta te controlando",true);
        yield return createNewMessageFromMe("Sei la vc ta estranha");
        yield return createNewMessageFromYou("Olha");
        yield return createNewMessageFromYou("Não sei oq vc ta pensando");
        yield return createNewMessageFromYou("Mas eu só to cansada");

        setButtonOptionsAndShow(
     "Se tu diz...", () => StartCoroutine(chat100()),
     "Olha, eu sei que tem algo errado", () => StartCoroutine(chat101()),
     "Eu quero muito te ajudar", () => StartCoroutine(chat102())
     );
    }

    public IEnumerator chat100()
    {
        Debug.Log("Cha100");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Se tu diz...");
        yield return createNewMessageFromMe("Mas eu não acredito");
        yield return createNewMessageFromYou("Mesmo se tiver algo errado vc não tem pq se meter");
        yield return createNewMessageFromYou("Não quero causar confusão com isso");

        setButtonOptionsAndShow(
     "Me deixa falar com o José", () => StartCoroutine(chat103()),
     "Eu não to me metendo por me meter", () => StartCoroutine(chat104()),
     "Isso é ridículo", () => StartCoroutine(chat105())
     );
    }

    public IEnumerator chat101()
    {
        Debug.Log("Cha101");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Olha, eu sei que tem algo errado");
        yield return createNewMessageFromYou("E se tiver??");
        yield return createNewMessageFromYou("Vc não tem pq se meter");
        yield return createNewMessageFromYou("Não quero causar confusão com isso");

        setButtonOptionsAndShow(
     "Me deixa falar com o José", () => StartCoroutine(chat103()),
     "Eu não to me metendo por me meter", () => StartCoroutine(chat104()),
     "Isso é ridículo", () => StartCoroutine(chat105())
     );
    }

     public IEnumerator chat102()
    {
        Debug.Log("Cha102");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Olha eu quero muito poder te ajudar",true);
        yield return createNewMessageFromMe("Olha, eu sei que tem algo errado");
        yield return createNewMessageFromYou("E se tiver??");
        yield return createNewMessageFromYou("Vc não tem pq se meter");
        yield return createNewMessageFromYou("Não quero causar confusão com isso");

        setButtonOptionsAndShow(
     "Me deixa falar com o José", () => StartCoroutine(chat103()),
     "Eu não to me metendo por me meter", () => StartCoroutine(chat104()),
     "Isso é ridículo", () => StartCoroutine(chat105())
     );
    }
    public IEnumerator chat103()//troca pra mãe
    {
        Debug.Log("Cha103");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Me deixa falar com o José",true);
        yield return createNewMessageFromMe("Eu não to me metendo por me meter");
        yield return createNewMessageFromMe("Mas claramente vc ta assim por causa dele");
        yield return createNewMessageFromYou("É melhor pararmos de conversar");
        yield return createNewMessageFromYou("Vai ser melhor assim");
        yield return createNewMessageFromMe("Eu entendo");
        yield return createNewMessageFromMe("Isso não ta certo");
        yield return createNewMessageFromMe("Mas de qualquer forma se precisar de algo to aqui");
        yield return createNewMessageFromMe("Se cuida");
        yield return createNewMessageFromYou("Você também");

        novoDia = "Qua 09:16";
        animationEnabled = true;
        textoFadeImage.text = "Tenho certreza que o josé ta privando ela";
        diaFadeImage.text = "Duas semanas depois";
        posTransicao = "chat106";
    }

    public IEnumerator chat104()//troca pra mãe
    {
        Debug.Log("Cha104");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Eu não to me metendo por me meter");
        yield return createNewMessageFromMe("Eu não to me metendo por me meter");
        yield return createNewMessageFromMe("Mas claramente vc ta assim por causa dele");
        yield return createNewMessageFromYou("É melhor pararmos de conversar");
        yield return createNewMessageFromYou("Vai ser melhor assim");
        yield return createNewMessageFromMe("Eu entendo");
        yield return createNewMessageFromMe("Isso não ta certo");
        yield return createNewMessageFromMe("Mas de qualquer forma se precisar de algo to aqui");
        yield return createNewMessageFromMe("Se cuida");
        yield return createNewMessageFromYou("Você também");

        novoDia = "Qua 09:16";
        animationEnabled = true;
        textoFadeImage.text = "Tenho certreza que o josé ta privando ela";
        diaFadeImage.text = "Duas semanas depois";
        posTransicao = "chat106";
    }

    public IEnumerator chat105()//troca pra mãe
    {
        Debug.Log("Cha105");
        ButtonPanel.SetActive(false);

        yield return createNewMessageFromMe("Isso é ridiculo",true);
        yield return createNewMessageFromMe("Eu não to me metendo por me meter");
        yield return createNewMessageFromMe("Mas claramente vc ta assim por causa dele");
        yield return createNewMessageFromYou("É melhor pararmos de conversar");
        yield return createNewMessageFromYou("Vai ser melhor assim");
        yield return createNewMessageFromMe("Eu entendo");
        yield return createNewMessageFromMe("Isso não ta certo");
        yield return createNewMessageFromMe("Mas de qualquer forma se precisar de algo to aqui");
        yield return createNewMessageFromMe("Se cuida");
        yield return createNewMessageFromYou("Você também");

        novoDia = "Ter 12:32";
        animationEnabled = true;
        textoFadeImage.text = "Tenho certreza que o josé ta privando ela";
        diaFadeImage.text = "Duas semanas depois";
        posTransicao = "chat106";
    }

    public IEnumerator chat106()//Ultima conversa
    {
        Debug.Log("Cha106");
        ButtonPanel.SetActive(false);

        cabecalho.GetComponent<Image>().color = new Color32(0,0,0,255);
        botoesFundo.GetComponent<Image>().color = new Color32(0,0,0,255);

        messagePreDelay = 0.000001f;
        yield return createNewMessageFromYou("Filho");
        messagePreDelay = 2.0f;
        yield return createNewMessageFromMe("Oi mãe");
        yield return createNewMessageFromYou("Preciso te mostrar essa notícia");
        yield return createNewNoticiaFromYou();
        yield return createNewMessageFromYou("Não consigo acreditar nisso");
        yield return createNewMessageFromMe("A MARIA????");
        yield return createNewMessageFromYou("Sim filho");
        yield return createNewMessageFromYou("O namorado dela....");
        yield return createNewMessageFromYou("Sinto muito");
        yield return createNewMessageFromMe("Não não não");
        yield return createNewMessageFromMe("Não pode ser verdade");
        yield return createNewMessageFromYou("Que tragédia");
        yield return createNewMessageFromYou("Ela tava aqui com nós esses dias");
        yield return createNewMessageFromYou("Você sabia de alguma coisa?");
        yield return createNewMessageFromYou("Ela tava aqui com nós esses dias");
        yield return createNewMessageFromYou("Você sabia de alguma coisa?");
        yield return createNewMessageFromMe("Ela deu alguns indícios que ele era agresivo");
        yield return createNewMessageFromMe("Mas eu não imaginei que chegaria nesse nível");
        yield return createNewMessageFromMe("Mas eu não imaginei que chegaria nesse nível");
        yield return createNewMessageFromMe("Achei que ela não queria ajuda");
        yield return createNewMessageFromYou("Talvez ela estivesse com medo de pedir ajuda");
        yield return createNewMessageFromMe("Eu sabia mãe...");
        yield return createNewMessageFromMe("Mas fiquei com medo de me intrometer");
        yield return createNewMessageFromMe("Eu devia ter feito alguma coisa");
        yield return createNewMessageFromMe("Ela não merecia");
        yield return createNewMessageFromMe("Preciso de um tempo para processar isso tudo");
        yield return createNewMessageFromYou("Se precisar de apoio estou aqui filho");
        yield return createNewMessageFromMe("Te amo, mãe");
        yield return createNewMessageFromYou("Também te amo filho");

    }

    //AQUI COMEÇA OS CREDITOS

    

    //Cria as mensagens
    public IEnumerator createNewMessageFromMe(string mensagem, bool vaiSerDeletada=false, float preDelay=0f, float posDelay=0f)
    {
        yield return waitSecondsAndCreateDialogChat(rightMessagePrefab, mensagem, vaiSerDeletada, preDelay, posDelay);
        PlayerPrefs.SetFloat("Timer", PlayerPrefs.GetFloat("Timer") - timeFromMe);
        Debug.Log(PlayerPrefs.GetFloat("Timer"));
    }

    public IEnumerator createImageFromYou(int numberFoto, string mensagem = "", bool vaiSerDeletada = false, float preDelay = 0f, float posDelay = 0f)
    {
        imageLeftMessagePrefab.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = imageGirl[numberFoto];
        yield return waitSecondsAndCreateDialogChat(imageLeftMessagePrefab, mensagem, vaiSerDeletada, preDelay, posDelay);
        PlayerPrefs.SetFloat("Timer", PlayerPrefs.GetFloat("Timer") - timeFoto);
        Debug.Log(PlayerPrefs.GetFloat("Timer"));
    }

    public IEnumerator createNewMessageFromYou(string mensagem, bool vaiSerDeletada=false, float preDelay=0f, float posDelay=0f)
    {
        yield return waitSecondsAndCreateDialogChat(leftMessagePrefab, mensagem, vaiSerDeletada, preDelay, posDelay);
        PlayerPrefs.SetFloat("Timer", PlayerPrefs.GetFloat("Timer") - timeToMe);
        Debug.Log(PlayerPrefs.GetFloat("Timer"));
    }

    public IEnumerator createNewNoticiaFromYou(float preDelay=0f, float posDelay=0f)
    {
        if (preDelay == 0f) {
            preDelay = messagePreDelay;
        }

        yield return new WaitForSeconds(preDelay);

        GameObject newMessage = Instantiate(noticiaPrefab, contentConversation.transform);
        newMessage.transform.localScale = new Vector3(1f, 1f, 1f);
        
        if (posDelay == 0f) {
            posDelay = messagePosDelay;
        }
        
        yield return new WaitForSeconds(posDelay);
    }

    public IEnumerator waitSecondsAndCreateDialogChat(GameObject prefab, string mensagem, bool vaiSerDeletada=false, float preDelay=0f, float posDelay=0f)
    {
        //se nao passar o predelay ele pega da variavel da classe
        if (preDelay == 0f) {
            preDelay = messagePreDelay;
        }

        yield return new WaitForSeconds(preDelay);

        GameObject newMessage = Instantiate(prefab, contentConversation.transform);

        newMessage.transform.localScale = new Vector3(1f, 1f, 1f);
        GameObject textMessage = newMessage.transform.Find("MessagePanel/MessageText").gameObject;
        Debug.Log(textMessage);
        TMP_Text textMeshPro = textMessage.GetComponent<TMP_Text>();
        textMeshPro.text = mensagem;
        
        //se nao passar o predelay ele pega da variavel da classe
        if (posDelay == 0f) {
            posDelay = messagePosDelay;
        }
        
        //esse intervalo vai ser executado antes de deletar a mensagem, caso isso for configurado
        yield return new WaitForSeconds(posDelay);

        if (vaiSerDeletada) {
            yield return new WaitForSeconds(messagePreDelay);
            textMeshPro.text = "🚫 Esta mensagem foi apagada";
        }

    }

    public void setButtonOptionsAndShow(string messageButton1, UnityAction actionButton1,
                                        string messageButton2, UnityAction actionButton2,
                                        string messageButton3, UnityAction actionButton3)
    {

        textoBotao1.text = messageButton1;
        botao1.onClick.RemoveAllListeners();
        botao1.onClick.AddListener(actionButton1);

        textoBotao2.text = messageButton2;
        botao2.onClick.RemoveAllListeners();
        botao2.onClick.AddListener(actionButton2);

        textoBotao3.text = messageButton3;
        botao3.onClick.RemoveAllListeners();
        botao3.onClick.AddListener(actionButton3);

        ButtonPanel.SetActive(true);

        if(messageButton3 == "CLOSE3"){
            botao3.gameObject.SetActive(false);
        } else{
            botao3.gameObject.SetActive(true);
        }
        if (messageButton2 == "CLOSE2")
        {
            botao2.gameObject.SetActive(false);
        }
        else
        {
            botao2.gameObject.SetActive(true);
        }
        if (messageButton1 == "CLOSE1")
        {
            botao1.gameObject.SetActive(false);
        }
        else
        {
            botao1.gameObject.SetActive(true);
        }

    }
    
    public void telasDeNotificacao(){
        if(posTransicao == "chat36"){
            notificacaoDia2.gameObject.SetActive(true);
        }else if(posTransicao == "chat465"){
            notificacaoDia22.gameObject.SetActive(true);
        }else if(posTransicao == "chat64"){
            notificacaoDia3.gameObject.SetActive(true);
        }else if(posTransicao == "chat685") {
            notificacaoDia32.gameObject.SetActive(true);
        }else if(posTransicao == "chat715"){
            notificacaoDia4.gameObject.SetActive(true);
        }else if(posTransicao == "chat74"){
            notificacaoDia5.gameObject.SetActive(true);
        }else if(posTransicao == "chat87"){
            notificacaoDia6.gameObject.SetActive(true);
        }else if(posTransicao == "chat106"){
            notificacaoDia7.gameObject.SetActive(true);
        }
    }

    
    public void OnBotaoTransicaoClick()
    {      
        botaoTransicao.gameObject.SetActive(false);

        if(posTransicao == "chat36")
        {
            notificacaoDia2.gameObject.SetActive(false);
            StartCoroutine(chat36());
        } else if(posTransicao == "chat74"){
            notificacaoDia5.gameObject.SetActive(false);
            StartCoroutine(chat74());
        }else if(posTransicao == "chat106"){
            notificacaoDia7.gameObject.SetActive(false);
            DestroiChatMessagesAll();
            StartCoroutine(chat106());
        }
        
    }

    public void ConfigureButtonTransition()
    {
        botaoTransicao.onClick.RemoveAllListeners();
        botaoTransicao.onClick.AddListener(OnBotaoTransicaoClick);
    }

    public void OnBotaoTransicao2Click(){

        botaoTransicao2.gameObject.SetActive(false);
        if(posTransicao == "chat465")
        {
            notificacaoDia22.gameObject.SetActive(false);
            textoBotaoTransicao2.gameObject.SetActive(false);
           // StartCoroutine(chat465());
        } else if( posTransicao == "chat64"){
            notificacaoDia3.gameObject.SetActive(false);
            textoBotaoTransicao2.gameObject.SetActive(false);
            StartCoroutine(chat64());
        }else if(posTransicao == "chat685"){
            notificacaoDia32.gameObject.SetActive(false);
            textoBotaoTransicao2.gameObject.SetActive(false);
            StartCoroutine(chat685());
        }else if(posTransicao == "chat715"){
            notificacaoDia4.gameObject.SetActive(false);
            textoBotaoTransicao2.gameObject.SetActive(false);
            StartCoroutine(chat715());
        } else if(posTransicao == "chat87"){
            notificacaoDia6.gameObject.SetActive(false);
            textoBotaoTransicao2.gameObject.SetActive(false);
            StartCoroutine(chat87());
        }
    }

    public void ConfigureButtonTransition2()
    {
        botaoTransicao2.onClick.RemoveAllListeners();
        botaoTransicao2.onClick.AddListener(OnBotaoTransicao2Click);
    }
    
    public void ToggleMusic()
    {
        if (musicAudioSource.isPlaying)
        {
            musicAudioSource.Pause();
            SomOn.gameObject.SetActive(false);
            SomOff.gameObject.SetActive(true);
        }
        else
        {
            musicAudioSource.Play();
            SomOff.gameObject.SetActive(false);
            SomOn.gameObject.SetActive(true);
        }
    }

    public void DestroiChatMessagesAll()
    {
        foreach(Transform child in contentConversation.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void UpdateTransition() 
    {
        if (animationEnabled)
        {
            //Inicio da transicao
            Debug.Log("ficando preto?");
            fadeImageObject.SetActive(true);
            elapsedTime += Time.deltaTime;

            float actualAlpha = elapsedTime / blinkDuration;
            fadeImage.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(minAlpha, maxAlpha, actualAlpha);

            if (elapsedTime > blinkDuration)
            {
                //Preto
                if (elapsedTextTime < textDuration)
                {
                    Debug.Log("Inicio");
                    elapsedTextTime += Time.deltaTime;
                    dia.text = novoDia;    
                    
                }

                else if (reverse == false)
                {
                    //voltando do preto
                    Debug.Log("Meio");
                    float temp = maxAlpha;
                    maxAlpha = minAlpha;
                    minAlpha = temp;
                    elapsedTime = 0f;
                    elapsedTextTime = 0f;
                    reverse = true;   
                    telasDeNotificacao();    
                }
                else
                {
                    //acabou
                    Debug.Log("Fim da transição");
                    animationEnabled = false;
                    reverse = false;
                    elapsedTime = 0f;
                    elapsedTextTime = 0f;
                    maxAlpha = 1f;
                    minAlpha = 0f;
                    fadeImageObject.SetActive(false);
                    if(posTransicao == "chat36"){
                        botaoTransicao.gameObject.SetActive(true);
                    } else if( posTransicao == "chat465"){
                        botaoTransicao2.gameObject.SetActive(true);
                        textoBotaoTransicao2.gameObject.SetActive(true);
                    } else if( posTransicao == "chat64"){
                        botaoTransicao2.gameObject.SetActive(true);
                        textoBotaoTransicao2.gameObject.SetActive(true);
                    } else if(posTransicao == "chat685"){
                        botaoTransicao2.gameObject.SetActive(true);
                        textoBotaoTransicao2.gameObject.SetActive(true);
                    }else if( posTransicao == "chat715"){
                        botaoTransicao2.gameObject.SetActive(true);
                        textoBotaoTransicao2.gameObject.SetActive(true);
                    } else if( posTransicao == "chat74"){
                        botaoTransicao.gameObject.SetActive(true);
                    }else if(posTransicao == "chat87"){
                        botaoTransicao2.gameObject.SetActive(true);
                        textoBotaoTransicao2.gameObject.SetActive(true);
                    } else if(posTransicao == "chat106"){
                        botaoTransicao.gameObject.SetActive(true);
                    }
                    
                    ConfigureButtonTransition();
                    ConfigureButtonTransition2();
                }
            }
        }
    }
}
