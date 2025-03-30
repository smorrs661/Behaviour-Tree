using UnityEngine;
using UnityEngine.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.Actions
{
    public class MoveToTarget : ActionTask<Transform>
    {

        [RequiredField]
        public BBParameter<GameObject> target;

        public float stoppingDistance = 2f;

        private NavMeshAgent NavAgent;

        protected override string OnInit()
        {
            NavAgent = NavAgent ?? agent.GetComponent<NavMeshAgent>();
            if (agent == null)
            {
                return "Missing NavMeshAgent component on the agent!";
            }

            return null;
        }

        protected override void OnExecute()
        {
            if (target.isNull || target.value == null)
            {
                Debug.LogWarning("MoveToTarget: Target is null or not assigned.");
                EndAction(false);
                return;
            }

            NavAgent.stoppingDistance = stoppingDistance;
            NavAgent.isStopped = false;
            NavAgent.SetDestination(target.value.transform.position);
        }

        protected override void OnUpdate()
        {
            if (target.isNull || target.value == null)
            {
                EndAction(false);
                return;
            }

            NavAgent.SetDestination(target.value.transform.position); // Optional: dynamic target

            float distance = Vector3.Distance(agent.transform.position, target.value.transform.position);
            if (distance <= stoppingDistance)
            {
                NavAgent.isStopped = true;
                EndAction(true); // Reached destination
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
