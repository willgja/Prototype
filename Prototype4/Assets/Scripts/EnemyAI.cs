using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade de movimento do inimigo
    public float desiredDistance = 9f; // Distância desejada em relação ao jogador
    public float stoppingDistance = 5f; // Distância em que o inimigo para de se mover
    private bool isMovingRight = true; // Variável para controlar o movimento lateral
    public float patrolDistance = 5f; // Distância para o patrulhamento

    public Transform player; // Referência ao transform do jogador

    private bool canDodge = true; // Flag para verificar se pode executar a esquiva
    private float dodgeDelay = 4f; // Tempo de espera entre as esquivas

    void Start()
    {
        // Encontrar o transform do jogador pelo tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Verificar a distância entre o inimigo e o jogador
        float distance = Vector3.Distance(transform.position, player.position);

        // Se a distância for maior que a desejada + a distância de parada, mover-se em direção ao jogador
        if (distance > desiredDistance + stoppingDistance)
        {
            Vector3 targetPosition = player.position + (player.forward * desiredDistance);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else if (distance < desiredDistance) // Se a distância for menor que a distância desejada, parar de se mover
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
        // Obtenha todos os projéteis em cena
        Bullet[] projectiles = FindObjectsOfType<Bullet>();

        // Encontre o projétil mais próximo
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

        // Se houver um projétil próximo, calcule a direção de esquiva
        if (closestProjectile != null)
        {
            Vector3 dodgeDirection = transform.position - closestProjectile.transform.position;
            dodgeDirection.Normalize();

            // Ajuste a direção de esquiva para evitar colisões
            Vector3 desiredPosition = transform.position + dodgeDirection * desiredDistance;

            // Mova o inimigo em direção à posição de esquiva
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, moveSpeed * Time.deltaTime);

            // Define a flag de esquiva para false
            canDodge = false;

            // Aguarda o tempo de delay antes de permitir a próxima esquiva
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
        // Desenhar o Gizmo de visualização da distância
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, player.position);
    }

}
