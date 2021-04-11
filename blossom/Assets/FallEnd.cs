using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallEnd : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.gameObject.name == "char1")
        {
            DialogSystem.singleton.dialog_related.SetActive(true);
            DialogSystem.onDialog = true;
            Camera.main.transform.position = new Vector3(1.22f, 0.32f, -10);
            DialogSystem.dialogCount++;
            Vector3 pos = DialogSystem.singleton.dialogText.transform.parent.GetComponent<RectTransform>().anchoredPosition;
            pos.y = 293;
            MusicSystem.setStage(MusicSystem.Stage.DEFEAT);
            DialogSystem.singleton.fight_related.SetActive(false);
            DialogSystem.singleton.dialogText.transform.parent.GetComponent<RectTransform>().anchoredPosition = pos;
            DialogSystem.singleton.dialog_sprite.enabled = true;
            DialogSystem.singleton.dialog_sprite.sprite = Resources.Load<Sprite>("Sprites/dialogs/5");
            DialogSystem.singleton.dialogText.text = "Çok güçlüler oğlum uzay mekiğine kaçç!!??\n[Kaybettiniz, ama üzülmeyin. Ana menüye dönmek için tıklayabilirsiniz.]";
            DialogSystem.singleton.dialogText.transform.parent.GetComponent<Button>().onClick.RemoveAllListeners();
            DialogSystem.singleton.dialogText.transform.parent.GetComponent<Button>().onClick.AddListener(() =>
            {
                Main.singleton.GameOver();
            });
        }
        else if (animator.gameObject.name == "char2")
        {
            Main.singleton.NextStage(0);
        }
        Main.singleton.ko.SetActive(false);
        Main.singleton.char1.SetActive(false);
        Main.singleton.char2.SetActive(false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
