using UnityEngine;
using UnityEngine.Events;

public class SceneAnimationCloseFinishEvent : StateMachineBehaviour
{

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.FindGameObjectWithTag("CloseScene").GetComponent<GameStateManager>().loadGameOverScene();
    }

}
