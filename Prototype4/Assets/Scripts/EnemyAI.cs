using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade de movimento do inimigo
    public float desiredDistance = 9f; // Dist�ncia desejada em rela��o ao jogador
    public float stoppingDistance = 5f; // Dist�ncia em que o inimigo para de se mover
    private bool isMovingRight = true; // Vari�vel para controlar o movimento lateral
    public float patrolDistance = 5f; // Dist�ncia para o patrulhamento

    public Transform player; // Refer�ncia ao transform do jogador

    private bool canDodge = true; // Flag para verificar se pode executar a esquiva
    private float dodgeDelay = 4f; // Tempo de espera entre as esquivas

    void Start()
    {
        // Encontrar o transform do jogador pelo tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Verificar a dist�ncia entre o inimigo e o jogador
        float distance = Vector3.Distance(transform.position, player.position);

        // Se a dist�ncia for maior que a desejada + a dist�ncia de parada, mover-se em dire��o ao jogador
        if (distance > desiredDistance + stoppingDistance)
        {
            Vector3 targetPosition = player.position + (player.forward * desiredDistance);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else if (distance < desiredDistance) // Se a dist�ncia for menor que a dist�ncia desejada, parar de se mover
        {
            Patrol();
        }

        if (canDodge)
        {
            DodgeProjectiles();
        }
    }

    private void DodgeProjectiles()
    {
        // Obtenha todos os proj�teis em cena
        Bullet[] projectiles = FindObjectsOfType<Bullet>();

        // Encontre o proj�til mais pr�ximo
        Bullet closestProjectile = null;
        float closestDistance = float.MaxValue;
        foreach (Bullet projectile in projectiles)
        {
            float distance = Vector3.Distance(transform.position, projectile.transform.position);
            if (distance < closestDistance)
            {
                closestProjectile = projectile;
                closestDistance = distance;
            }
        }

        // Se houver um proj�til pr�ximo, calcule a dire��o de esquiva
        if (closestProjectile != null)
        {
            Vector3 dodgeDirection = transform.position - closestProjectile.transform.position;
            dodgeDirection.Normalize();

            // Ajuste a dire��o de esquiva para evitar colis�es
            Vector3 desiredPosition = transform.position + dodgeDirection * desiredDistance;

            // Mova o inimigo em dire��o � posi��o de esquiva
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, moveSpeed * Time.deltaTime);

            // Define a flag de esquiva para false
            canDodge = false;

            // Aguarda o tempo de delay antes de permitir a pr�xima esquiva
            StartCoroutine(ResetDodgeDelay());
        }
    }

    private IEnumerator ResetDodgeDelay()
    {
        yield return new WaitForSeconds(dodgeDelay);
        canDodge = true;
    }

    private void Patrol()
    {
        // Movimento lateral entre dois pontos
        if (isMovingRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= patrolDistance)
            {
                isMovingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= -patrolDistance)
            {
                isMovingRight = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Desenhar o Gizmo de visualiza��o da dist�ncia
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, player.position);
    }

}
