using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class AIFieldOfView : MonoBehaviour
{
    [Header("Refrence")]
    private AIUtility aiUtil;

    [Header("View Handler")]
    [Tooltip("Change in General AI Stats, Vision Range")] public float viewRadius;
    private float viewRad;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMastk;
    
    public List<GameObject> visibleObj = new List<GameObject>();

    private void Start()
    {
        aiUtil = GetComponent<AIUtility>();
        if (aiUtil)
        {
            viewRad = aiUtil.getAIFOVRange();
        }
        StartCoroutine("FindDelay", .1f);
    }

    
    IEnumerator FindDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            findVisiobleTarget();
        }
    }

    public Vector3 DirFromAngle(float angleDeg, bool angeIsGlob)
    {
        if (!angeIsGlob)
        {
            angleDeg += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleDeg * Mathf.Deg2Rad), 0, Mathf.Cos(angleDeg * Mathf.Deg2Rad));
    }

    public float getViewRad()
    {
        return viewRad;
    }

    void findVisiobleTarget()
    {
        visibleObj.Clear();
        bool invisible = true;
        Collider[] targetInFOV = Physics.OverlapSphere(transform.position, viewRad, targetMask);

        for (int i = 0; i < targetInFOV.Length; i++)
        {
            invisible = false;
            Transform target = targetInFOV[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMastk))
                {
                    if (!visibleObj.Contains(target.gameObject))
                    {
                        visibleObj.Add(target.gameObject);
                        aiUtil.addDetectedTarget(target.gameObject);
                        return;
                    }
                }
            }
        }

        if (invisible)
        {

        }
    }
}
