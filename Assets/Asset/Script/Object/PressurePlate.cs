using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public string id = "pp";
    private Animator animator;
    
    [SerializeField] private List<MovableBlock> blocks = new List<MovableBlock>();
    [SerializeField] private AreaCleaner areaCleaner;
    [SerializeField] private Collider2D _collider;

    public bool isActivated = false;

    private int triggerAnimation = Animator.StringToHash("isPressed");

    void Awake()
    {   
        animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        blocks = GetComponentsInChildren<MovableBlock>(true).ToList();

        if(!SaveManager.instance.pressurePlates.Contains(this))
        {
            SaveManager.instance.pressurePlates.Add(this);
        }

        if(isActivated) animator.SetTrigger(triggerAnimation);
    }

    void OnTriggerEnter2D(Collider2D collision)
    { 
        animator.SetTrigger(triggerAnimation);
        if(areaCleaner != null) areaCleaner.ClearArea();
        foreach (var block in blocks)
        {
            block.gameObject.SetActive(true);
            block.StartMove();
        }
        isActivated = true;
        _collider.enabled = false;
        // Destroy(this);
    }

    public void SkipActivate()
    {
        animator.SetTrigger(triggerAnimation);
        if(areaCleaner != null) areaCleaner.ClearArea();
        foreach (var block in blocks)
        {
            block.gameObject.SetActive(false);
        }
        _collider.enabled = false;
        isActivated = true;
    }

    public void Reset()
    {
        animator.Rebind();

        foreach (var block in blocks)
        {
            block.gameObject.SetActive(block.ogState);
            _collider.enabled = true;
            block.Reset();
        }
    }
}
