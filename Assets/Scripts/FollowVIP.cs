using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions
{
    public class FollowVIP : ActionTask<Transform>
    {

        [RequiredField]
        public BBParameter<Transform> vipTarget;

        public float stoppingDistance = 2f;

        private NavMeshAgent NavAgent;

        protected override string OnInit()
        {
            NavAgent = NavAgent ?? agent.GetComponent<NavMeshAgent>();
            if (agent == null)
            {
                return "NavMeshAgent is missing on the agent!";
            }
            return null;
        }

        protected override void OnExecute()
        {
            if (vipTarget.isNull || vipTarget.value == null)
            {
                Debug.LogWarning("VIP target is not assigned or missing!");
                EndAction(false);
                return;
            }

            NavAgent.isStopped = false;
        }

        protected override void OnUpdate()
        {
            if (vipTarget.isNull || vipTarget.value == null)
            {
                EndAction(false);
                return;
            }

            Vector3 targetPos = vipTarget.value.position;
            float distance = Vector3.Distance(agent.transform.position, targetPos);

            NavAgent.SetDestination(targetPos);

            if (distance <= stoppingDistance)
            {
                NavAgent.isStopped = true;
                EndAction(true); // Task successful
            }
        }

        protected override void OnStop()
        {
            if (agent != null)
            {
                NavAgent.isStopped = true;
            }
        }

        protected override void OnPause()
        {
            if (agent != null)
            {
                NavAgent.isStopped = true;
            }
        }
    }
}
