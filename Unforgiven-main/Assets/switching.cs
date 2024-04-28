using UnityEngine;
using UnityEngine.InputSystem;

public class switching : MonoBehaviour
{
    [SerializeField]
    private InputAction action;
    private Animator animator;
    private bool main = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        action.Enable();
    }
    private void OnDisable()
    {
        action.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        action.performed += _ => switchstate();

    }

    private void switchstate()
    {
        if (main)
        {
            animator.Play("static");
        }
        else
        {
            animator.Play("main");
        }
        main = !main;
    }
}
