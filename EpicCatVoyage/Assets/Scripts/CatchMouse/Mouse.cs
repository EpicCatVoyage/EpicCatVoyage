using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    private bool isDead;
    public float speed = 5.0f;
    GameObject director;

    // Start is called before the first frame update
    void Start()
    {
        this.director = GameObject.Find("GameDirector");
        InvokeRepeating("ChangeDirection", 0.5f, 0.5f);
    }

    void ChangeDirection()
    {
        float angle = 0.0f;
        int random = Random.Range(0, 3);

        switch (random)
        {
            case 0:
                angle = 45;
                break;

            case 1:
                angle = 0;
                break;

            case 2:
                angle = -45;
                break;
        }

        transform.Rotate(0.0f, 0.0f, angle);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;

        transform.position += this.transform.up * speed * Time.deltaTime;
    }

    private IEnumerator OnMouseDown()
    {
        isDead = true;
        CancelInvoke();

        Animator animator = GetComponent<Animator>();
        animator.SetBool("IsDead", true);

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
        this.director.GetComponent<GameDirector>().GetMouse();

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
