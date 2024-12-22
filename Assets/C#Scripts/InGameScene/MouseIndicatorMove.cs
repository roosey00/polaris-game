using UnityEngine;

public class MouseIndicatorMove : MonoBehaviour
{
    new private ParticleSystem particleSystem = null;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.groundMouseHit.MousePos.x != Vector3.positiveInfinity.x)
        {
            transform.position = GameManager.ChangeY(GameManager.Instance.groundMouseHit.MousePos, transform.position.y);
        }
        if (Input.GetMouseButtonDown(1))
        {
            particleSystem.Play();
        }
        if (Input.GetMouseButtonUp(1))
        {
            particleSystem.Clear(); // 파티클 정지
            particleSystem.Stop(); // 파티클 정지
        }
    }
}
