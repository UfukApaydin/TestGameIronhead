using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject soruPaneli;

    [SerializeField]
    GameObject dogruicon , yanlisicon;

    [SerializeField]
    GameObject robot1, robot2, robot3;

    [SerializeField]
    GameObject oyuncuPrefab;
    [SerializeField]
    GameObject dogruSonuc, yanlisSonuc;






    OyuncuHareketManager oyuncuHareketManager;





    public bool sorucevaplansinmi;
    public string dogrucevap;
    public int kalanhak;
    public int dogruadet;

    SorularManager sorularManager;

    private void Awake()
    {
        oyuncuHareketManager = Object.FindObjectOfType<OyuncuHareketManager>();
        sorularManager = Object.FindObjectOfType<SorularManager>();
      


    }


    private void Start()
    {
        StartCoroutine(oyunuAcRouitine());
        kalanhak = 3;
        dogruadet = 0;
            

    }

    IEnumerator oyunuAcRouitine()
    {
        yield return new WaitForSeconds(.1f);
        soruPaneli.GetComponent<RectTransform>().DOAnchorPosX(-462, 1f);

        yield return new WaitForSeconds(1.1f);
        sorularManager.SorulariYazdir();

    }
 
    public void SonucuKontrolEt(string gelenCevap)
    {
        if(gelenCevap ==dogrucevap)
        {
            //Sonuc Do�ru ise yapilacaklar
            dogruadet++;

            if(dogruadet >=10)
            {
                dogruSonucGoster();

            }
            else
            {
                sorularManager.SorulariYazdir();

            }
            DogruIconuAktiflestir();


        }
        else
        {
            //Sonuc yanl�s ise yap�lacaklar
            YanlisIconuAktiflestir();
            StartCoroutine(OyuncuHataYaptiGeriGeldi());

            



        }
    }

    void DogruIconuAktiflestir()
    {
        dogruicon.GetComponent<CanvasGroup>().DOFade(1, .3f);

        Invoke("DogruiconuPasiflestir", .8f);

    }
    void YanlisIconuAktiflestir()
    {
       yanlisicon.GetComponent<CanvasGroup>().DOFade(1, .3f);

        Invoke("YanlisiconuPasiflestir", .8f);
    }

    void DogruiconuPasiflestir()
    {
        dogruicon.GetComponent<CanvasGroup>().DOFade(0, .3f);
    }
    void YanlisiconuPasiflestir()
    {
        yanlisicon.GetComponent<CanvasGroup>().DOFade(0, .3f);

    }

    IEnumerator OyuncuHataYaptiGeriGeldi()
    {
        yield return new WaitForSeconds(1f);

        oyuncuHareketManager.OyuncuHataYapti();

        yield return new WaitForSeconds(1.4f);

        kalanhak--;

        HakKaybet();


        if(kalanhak>0)
        {
            oyuncuHareketManager.OyuncuGeriGelsin();

            yield return new WaitForSeconds(1f);

            sorularManager.SorulariYazdir();

        }
        else
        {

            //oyunbitti
            yanlisSonucGoster();
   
        }





    }

    void HakKaybet()
    {
        if (kalanhak==2)
        {
            robot3.SetActive(false);
            robot2.SetActive(true);
            robot1.SetActive(true);
        }
        else if(kalanhak==1)
        {
            robot3.SetActive(false);
            robot2.SetActive(false);
            robot1.SetActive(true);
        }
        else if (kalanhak==0)
        {
            robot3.SetActive(false);
            robot2.SetActive(false);
            robot1.SetActive(false);
        }


    }

    void dogruSonucGoster()
    {
        soruPaneli.GetComponent<RectTransform>().DOAnchorPosX(-1530, 1f);
        dogruSonuc.GetComponent<CanvasGroup>().DOFade(1, .5f);
        dogruSonuc.GetComponent<RectTransform>().DOScale(1, .5f).SetEase(Ease.OutBack);

    }
    void yanlisSonucGoster()
    {
        soruPaneli.GetComponent<RectTransform>().DOAnchorPosX(-1530, 1f);
        yanlisSonuc.GetComponent<CanvasGroup>().DOFade(1, .5f);
        yanlisSonuc .GetComponent<RectTransform>().DOScale(1, .5f).SetEase(Ease.OutBack);

    }

}
