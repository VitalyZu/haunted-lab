using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjectComponent : MonoBehaviour
{
    [SerializeField] float _force;
    private static Dictionary<int, Rigidbody2D> _rbs = new Dictionary<int, Rigidbody2D>();
    public void PushTarget(GameObject target)
    {
        var id = target.GetInstanceID();
        var direction = GetDirection(target);
        var force = new Vector2(_force * direction, 1f);

        Rigidbody2D targetRB;
        if (_rbs.TryGetValue(id, out targetRB))
        {
            
            targetRB.AddForce(force, ForceMode2D.Impulse);
        }
        else
        {
            Debug.Log("Capture RB");
            Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(force, ForceMode2D.Impulse);
                _rbs.Add(id, rb);
            }
        }
    }

    private int GetDirection(GameObject target)
    {
        return gameObject.transform.position.x > target.transform.position.x ? -1 : 1;
    }
}
