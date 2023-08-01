using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private LessOrMore _lessOrMore;
    [SerializeField] private float _transitionRange;
    
    private void Update() 
    {
        if (_lessOrMore == LessOrMore.Less)
            if (Vector3.Distance(transform.position, Target.transform.position) < _transitionRange)
                NeedTransit = true;
        else if (_lessOrMore == LessOrMore.More)
            if (Vector3.Distance(transform.position, Target.transform.position) > _transitionRange)
                NeedTransit = true;   
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _transitionRange);
    }
}

enum LessOrMore
{
    Less,
    More
}
