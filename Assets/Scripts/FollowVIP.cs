using UnityEngine;
using UnityEngine.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.Actions
{

    [Category("Custom/Movement")]
    [Description("Follow the VIP using NavMeshAgent, with periodic reevaluation to check for enemies.")]
    public class FollowVIP : ActionTask<Transform>
    {

        [RequiredField]
        public BBParameter<Transform> vipTarget;

        public float stoppingDistance = 2f;
        public float followDuration = 1.0f; // Duration before reevaluating

        private NavMeshAgent navAgent;
        private float followTimer;

        protected override string OnInit()
        {
            navAgent = agent.GetComponent<NavMeshAgent>();
            if (navAgent == null)
            {
                return "Missing NavMeshAgent on agent GameObject!";
            }
            return null;
        }

        protected override void OnExecute()
        {
            if (vipTarget.isNull || vipTarget.value == null)
            {
                EndAction(false);
                return;
            }

            followTimer = followDuration;

            navAgent.stoppingDistance = stoppingDistance;
            navAgent.isStopped = false;
            navAgent.SetDestination(vipTarget.value.position);
        }

        protected override void OnUpdate()
        {
            if (vipTarget.isNull || vipTarget.value == null)
            {
                EndAction(false);
                return;
            }

            followTimer -= Time.deltaTime;

            navAgent.SetDestination(vipTarget.value.position); // Update VIP position dynamically

            if (followTimer <= 0f)
            {
                navAgent.isStopped = true;
                EndAction(true); // Reevaluate behavior tree
            }
        }

        protected override void OnStop()
        {
            if (navAgent != null)
            {
                navAgent.isStopped = true;
            }
        }

        protected override void OnPause()
        {
            if (navAgent != null)
            {
                navAgent.isStopped = true;
            }
        }
    }
}
