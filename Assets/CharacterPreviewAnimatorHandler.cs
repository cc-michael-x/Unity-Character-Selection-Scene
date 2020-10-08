using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPreviewAnimatorHandler : StateMachineBehaviour
{
    private static readonly string gameManager = "GameManager";
    private static readonly string characterPreviewComponents = "CharacterPreviewComponents";
    private static readonly string characterPreviewCanvas = "CharacterPreviewCanvas";
    private static readonly int Play = Animator.StringToHash("Play");
    private static readonly int CharacterPreview = Animator.StringToHash("CharacterPreview");
    private GameObject _characterPreviewParentCanvas;
    private RectTransform _subCanvasScale;
    private CharacterPreview _characterPreviewClass;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*if (animator.GetBool(CharacterPreview))
            return;*/
        
        string currentCharacterSelected = GameObject.Find(gameManager).GetComponent<GameManager>().currentCharacterSelected;
        GameObject currentCharacterObject = GameObject.Find(currentCharacterSelected);

        if (currentCharacterObject == null)
            return;
        
        // sift through a list of child objects to find the specific one
        for (var i = 0; i < currentCharacterObject.transform.childCount; i++)
        {
            // object is not what we're looking for
            if (currentCharacterObject.transform.GetChild(i).gameObject.name !=
                characterPreviewComponents) continue;
        
            // found the game object we're looking for
            _characterPreviewClass = currentCharacterObject.transform.GetChild(i).gameObject.GetComponent<CharacterPreview>();
        }
        
        _characterPreviewClass.BlockSelect();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        string currentCharacterSelected = GameObject.Find(gameManager).GetComponent<GameManager>().currentCharacterSelected;
        GameObject currentCharacterObject = GameObject.Find(currentCharacterSelected);
        
        // sift through a list of child objects to find the specific one
        for (var i = 0; i < currentCharacterObject.transform.childCount; i++)
        {
            // object is not what we're looking for
            if (currentCharacterObject.transform.GetChild(i).gameObject.name !=
                characterPreviewCanvas) continue;
        
            // found the game object we're looking for
            _characterPreviewParentCanvas = currentCharacterObject.transform.GetChild(i).gameObject;

            // keep the scale of the character preview sub canvas for showing it or not
            _subCanvasScale = 
                _characterPreviewParentCanvas.transform.GetChild(0).GetChild(0).gameObject.GetComponent<RectTransform>();
        }

        if (animator.GetBool(CharacterPreview))
        {
            _subCanvasScale.localScale = new Vector3(1, 1, 1);
        }
        else
            _subCanvasScale.localScale = new Vector3(0, 0, 0);
        
        // reset the play to false to not play animations on loop 
        _characterPreviewParentCanvas.GetComponent<Animator>().SetBool(Play, false);
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
