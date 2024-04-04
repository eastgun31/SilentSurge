using UnityEngine;

public class SeePlayer : MonoBehaviour
{
    public Transform player; // �÷��̾��� ��ġ�� �����ϱ� ���� ����
    public float followSpeed = 5f; // �÷��̾ �����ϴ� �ӵ�

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

        // ���� �÷��̾ �þ߿� �� �� �ִ��� ���θ� ��ȯ�ϴ� ����
        // �� ���ÿ����� ������ ���� �÷��̾� ���̿� ��ֹ��� �ִ��� ���η� �Ǵ�
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
        // ���� �÷��̾ �����ϴ� ����
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * followSpeed * Time.deltaTime;
        // ���� �÷��̾ �ٶ󺸰� �Ϸ��� �Ʒ� �ּ��� �����ϼ���.
        // transform.rotation = Quaternion.LookRotation(direction);
    }
}