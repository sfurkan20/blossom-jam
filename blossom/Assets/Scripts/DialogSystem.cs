using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public GameObject fight_related;
    public GameObject char1, char2;
    public GameObject dialog_related;
    public GameObject game_over;
    public static int[] dialog_images = { 1, 1, 2, 3, 3, 3 };
    public static int[] dialog_speakimages = { 1, 2, 1, 2, 1, 2 };
    public static string[] dialog_strings = { "Vay be, sonunda ulaştık şu Ay'a.", "Yani, o kadar uğraştık, ama sonunda Ay'a hakim olan tek ülke olacağız.", "Hehehe, haklısın.", "Bu ne yaa??!! Benim gördüğümü sen de görüyor musun?", "...", "Gel şunlara hadlerini bildirelim.", "Adamlar çok güçlü oğlum kaç uzay mekiğine!!",};
    public static Dialog[] dialogs;
    public Image dialog_sprite;
    public class Dialog
    {
        public enum post_state { NEW_DIALOG, START_FIGHT, END_GAME };
        post_state poststate;
        public int id;
        public string dialog;
        public delegate void state_setter(int id);
        public state_setter setter;
        public Dialog(int id, post_state state)
        {
            this.id = id;
            poststate = state;
            dialog = dialog_strings[id];
            if(poststate == post_state.NEW_DIALOG)
            {
                setter = new state_setter(setDialog);
            }
            else if(poststate == post_state.START_FIGHT)
            {
                setter = new state_setter(Main.singleton.NextStage);
            }
            else if(poststate == post_state.END_GAME)
            {
                DialogSystem.singleton.game_over.SetActive(true);
            }
        }
    }
    public static Dialog currentDialog;
    public static DialogSystem singleton;
    public static bool onDialog = true;
    static int _dialogCount = 0;
    public static int dialogCount
    {
        get
        {
            return _dialogCount;
        }
        set
        {
            _dialogCount = value;
            singleton.dialogText.text = dialog_strings[value];
        }
    }
    public Text dialogText;
    void Start()
    {
        singleton = this;
        dialogs = new Dialog[100];
        for(int i = 0; i < 5; i++)
        {
            dialogs[i] = new Dialog(i,Dialog.post_state.NEW_DIALOG);
        }
        dialogs[5] = new Dialog(5, Dialog.post_state.START_FIGHT);
        dialogCount = 0;
        MusicSystem.setStage(MusicSystem.Stage.FIRST_DIALOGS);
    }

    void Update()
    {
        if(onDialog && Input.GetMouseButtonDown(0))
        {
            if (dialogCount <= 5)
            {
                dialogs[dialogCount].setter(dialogCount);
            }
        }
    }

    public static void setDialog(int id)
    {
        singleton.dialogText.transform.parent.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/dialogpps/" + dialog_speakimages[id + 1].ToString());
        DialogSystem.singleton.dialog_sprite.enabled = true;
        DialogSystem.singleton.dialog_sprite.sprite = Resources.Load<Sprite>("Sprites/dialogs/" + dialog_images[id + 1].ToString());
        singleton.char1.SetActive(false);
        singleton.char2.SetActive(false);
        DialogSystem.singleton.fight_related.SetActive(false);
        DialogSystem.singleton.dialog_related.SetActive(true);
        dialogCount++;
    }
}
