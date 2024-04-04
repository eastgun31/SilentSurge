using UnityEngine;

public class SeePlayer : MonoBehaviour
{
    public Transform player; // 플레이어의 위치를 저장하기 위한 변수
    public float followSpeed = 5f; // 플레이어를 추적하는 속도

    private void Update()
    {
        if (CanSeePlayer())
        {
            FollowPlayer();
        }
    }

    bool CanSeePlayer()
    {
        if (player == null)
            return false;

        // 적이 플레이어를 시야에 볼 수 있는지 여부를 반환하는 로직
        // 이 예시에서는 간단히 적과 플레이어 사이에 장애물이 있는지 여부로 판단
        RaycastHit hit;
        if (Physics.Linecast(transform.position, player.position, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    void FollowPlayer()
    {
        // 적이 플레이어를 추적하는 로직
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * followSpeed * Time.deltaTime;
        // 적이 플레이어를 바라보게 하려면 아래 주석을 해제하세요.
        // transform.rotation = Quaternion.LookRotation(direction);
    }
}