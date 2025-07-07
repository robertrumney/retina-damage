using UnityEngine;
using System;

public class RetinaDamage : MonoBehaviour
{
    public Transform looker;
    public Transform target;

    private readonly float thresholdAngle = 16.0f;
    private float thresholdAngleSquaredCosine;
    private float damageTimer = 0f;

    private int count = 0;
    private bool isDamaging = false;

    private void Start()
    {
        if (target == null)
        {
            foreach (var l in FindObjectsOfType<Light>())
            {
                if (l.type == LightType.Directional)
                {
                    target = l.transform;
                    break;
                }
            }
        }

        if (GameLoader.instance.skydome)
        {
            GameLoader.instance.skydome.Moon.MeshBrightness = 0.15f;
        }
        else
        {
            enabled = false;
            return;
        }

        thresholdAngleSquaredCosine = Mathf.Cos(thresholdAngle * Mathf.Deg2Rad);
        thresholdAngleSquaredCosine *= thresholdAngleSquaredCosine;
    }

    private void Update()
    {
        if (target == null) return;

        if (IsLookingAtTarget())
        {
            if (!isDamaging)
            {
                int hour = DateTime.Now.Hour;
                if (hour > 8 && hour < 17)
                {
                    isDamaging = true;
                    damageTimer = 0f;
                }
            }

            if (isDamaging)
            {
                damageTimer += Time.deltaTime;
                if (damageTimer >= 1f)
                {
                    DamagePlayer();
                    damageTimer = 0f;
                }
            }
        }
        else
        {
            isDamaging = false;
        }
    }

    private bool IsLookingAtTarget()
    {
        Vector3 targetDirection = target.position - looker.position;
        Vector3 lookerForward = looker.forward;

        float dotProduct = Vector3.Dot(lookerForward, targetDirection);
        float lookerMagnitudeSq = lookerForward.sqrMagnitude;
        float targetMagnitudeSq = targetDirection.sqrMagnitude;
        float cosineOfAngleSq = (dotProduct * dotProduct) / (lookerMagnitudeSq * targetMagnitudeSq);

        return cosineOfAngleSq >= thresholdAngleSquaredCosine;
    }

    private void DamagePlayer()
    {
        if (count < 3)
        {
            Debug.Log("DON'T STARE DIRECTLY INTO THE SUN!");
        }

        count++;
    }
}
