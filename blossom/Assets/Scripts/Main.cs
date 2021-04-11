using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public static int stage = 0;
    public static Main singleton;
    public Image hp1img, hp2img, ragebar;
    public AnimationClip koclip;
    public GameObject char1, char2;
    public GameObject ending, fightstart, ko;
    float _hp1;
    public float hp1
    {
        get
        {
            return _hp1;
        }
        set
        {
            hp1img.transform.localScale = new Vector3(value / 100f, 1, 1);
            if(value <= 0)
            {
                value = 0;
                StartCoroutine(KO(char1));
            }
            _hp1 = value;
        }
    }
    float _hp2;
    public float hp2
    {
        get
        {
            return _hp2;
        }
        set
        {
            if(value <= 0)
            {
                value = 0;
                StartCoroutine(KO(char2));
            }
            hp2img.transform.localScale = new Vector3(value / 100f, 1, 1);
            _hp2 = value;
        }
    }
    float _rage;
    public float rage
    {
        get
        {
            return _rage;
        }
        set
        {
            if(value >= 100)
            {
                value = 100;
                FightSystem.singleton.animator.SetBool("Rage", true);
            }
            ragebar.transform.localScale = new Vector3(value / 100f, 1, 1);
            _rage = value;
        }
    }
    void Start()
    {
        singleton = this;
        hp1 = 100;
        hp2 = 100;
        rage = 0;
    }

    void Update()
    {
        DialogSystem.currentDialog = new DialogSystem.Dialog(6, DialogSystem.Dialog.post_state.NEW_DIALOG);
    }

    IEnumerator KO(GameObject character)
    {
        ko.SetActive(true);
        Animation a = ko.GetComponent<Animation>();
        character.GetComponent<Animator>().SetBool("Fall", true);
        yield return new WaitForSeconds(1.6f);
        character.GetComponent<Animator>().SetBool("Fall", false);
    }

    public void setHP(string name, float decrease)
    {
        switch (name)
        {
            case "char2":
                hp1 += decrease;
                break;
            case "char1":
                hp2 += decrease;
                break;
        }
    }

    public void GameOver()
    {
        Destroy(GameObject.Find("MusicSystem"));
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void NextStage(int i)
    {
        if(stage == 3)
        {
            DialogSystem.singleton.dialog_related.SetActive(true);
            DialogSystem.onDialog = true;
            Camera.main.transform.position = new Vector3(1.22f, 0.32f, -10);
            DialogSystem.dialogCount++;
            DialogSystem.singleton.dialog_sprite.sprite = Resources.Load<Sprite>("Sprites/dialogs/4");
            DialogSystem.singleton.fight_related.SetActive(false);
            Vector3 pos = DialogSystem.singleton.dialogText.transform.parent.GetComponent<RectTransform>().anchoredPosition;
            pos.y = 293;
            DialogSystem.singleton.dialogText.transform.parent.GetComponent<RectTransform>().anchoredPosition = pos;
            DialogSystem.singleton.dialogText.text = "Herifleri nasıl kovduk ama?\n[Tebrikler, kazandınız! Ana menüye dönmek için tıklayın.]";
            DialogSystem.singleton.dialogText.transform.parent.GetComponent<Button>().onClick.RemoveAllListeners();
            DialogSystem.singleton.dialogText.transform.parent.GetComponent<Button>().onClick.AddListener(()=>
            {
                GameOver();
            });
        }
        else
        {
            stage++;
            DialogSystem.singleton.dialog_sprite.sprite = null;
            DialogSystem.onDialog = false;
            DialogSystem.singleton.fight_related.SetActive(false);
            DialogSystem.singleton.dialog_related.SetActive(false);
            fightstart.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/pp/" + stage.ToString());
            fightstart.SetActive(true);
        }
    }
}
