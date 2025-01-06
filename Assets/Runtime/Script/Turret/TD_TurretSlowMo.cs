using System.Collections;
using UnityEditor;
using UnityEngine;

public class TD_TurretSlowMo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask _enemiesMask;

    [Header("Attributes")]
    [SerializeField] private float _targetInRange = 5f;
    [SerializeField] private float _attackSpeed = 4f;
    [SerializeField] private float _freezeTime = 1f;

    private float _timeUntilFire; 

    private void Update()
    {
        _timeUntilFire += Time.deltaTime;

        if (_timeUntilFire >= 1f / _attackSpeed)
        {
            FreezeEnemies();
            _timeUntilFire = 0f;
        }
        
    }

    private void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _targetInRange, (Vector2)transform.position, 0f, _enemiesMask);

        if (hits.Length > 0 )
        {
            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                TD_EnnemyMovement em = hit.transform.GetComponent<TD_EnnemyMovement>();
                em.UpdateSpeed(0.5f);
                StartCoroutine(ResetEnnemySpeed(em));
            }
        }
    }

    private IEnumerator ResetEnnemySpeed(TD_EnnemyMovement em)
    {
        yield return new WaitForSeconds(_freezeTime);
        em.ResetSpeed();
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, _targetInRange);
    }
}
