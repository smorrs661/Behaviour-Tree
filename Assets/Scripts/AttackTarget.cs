using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.Actions
{
    public class AttackTarget : ActionTask
    {

        [RequiredField]
        public BBParameter<GameObject> target;

        protected override string OnInit()
        {
            return null;
        }

        protected override void OnExecute()
        {
            if (target.isNull || target.value == null)
            {
                Debug.LogWarning("AttackTarget: Target is null or missing.");
                EndAction(false);
                return;
            }

            float distance = Vector3.Distance(agent.transform.position, target.value.transform.position);
            if (distance > 2f) // Not close enough, fail task
            {
                Debug.Log("AttackTarget: Target is too far to attack.");
                EndAction(false);
                return;
            }

            Object.Destroy(target.value);
            EndAction(true);
        }

    }
}