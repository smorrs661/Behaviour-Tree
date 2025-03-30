using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Linq;

namespace NodeCanvas.Tasks.Actions
{
    public class DetectClosestEnemy : ActionTask<Transform>
    {

        [RequiredField]
        public BBParameter<GameObject> EnemyGameObject;

        public string enemyTag = "Enemy";
        public float detectionRadius = 10f;

        protected override void OnExecute()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

            GameObject closest = null;
            float minDist = Mathf.Infinity;

            foreach (var enemy in enemies)
            {
                float dist = Vector3.Distance(agent.position, enemy.transform.position);
                if (dist < minDist && dist <= detectionRadius)
                {
                    minDist = dist;
                    closest = enemy;
                }
            }

            if (closest != null)
            {
                EnemyGameObject.value = closest;
                EndAction(true); // Found one
            }
            else
            {
                EnemyGameObject.value = null;
                EndAction(false); // Nothing found
            }
        }
    }
}
