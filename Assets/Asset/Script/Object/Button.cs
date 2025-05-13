using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private Animator animator;
    
    [SerializeField] private List<MovableBlock> blocks = new List<MovableBlock>();
    [SerializeField] private AreaCleaner areaCleaner;

    void Awake()
    {   
        animator = GetComponent<Animator>();
        blocks = GetComponentsInChildren<MovableBlock>().ToList();

    }

    private int triggerAnimation = Animator.StringToHash("isPressed");
    void OnTriggerEnter2D(Collider2D collision)
    { 
        animator.SetTrigger(triggerAnimation);
        if(areaCleaner != null) areaCleaner.ClearArea();
        foreach (var block in blocks)
        {
            block.StartMove();
        }
    }
}
