using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    public int Index { get; set; }

    public event Action<Checkpoint> OnPlayerPassed;

    [SerializeField] private GameObject checkpointMessageUI;
    [SerializeField] private Text checkpointText;
    [SerializeField] private bool isFinalCheckpoint = false;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            OnPlayerPassed?.Invoke(this);

            if (isFinalCheckpoint)
            {
                ShowCheckpointMessage("Поздравляем! Вы прошли трассу!");
            }
            else
            {
                ShowCheckpointMessage("Чекпоинт пройден!");
            }

            StartCoroutine(HideCheckpointMessageAfterDelay(2f));
        }
    }

    private void ShowCheckpointMessage(string message)
    {
        if (checkpointMessageUI != null && checkpointText != null)
        {
            checkpointMessageUI.SetActive(true);
            checkpointText.text = message;
        }
    }

    private IEnumerator HideCheckpointMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (checkpointMessageUI != null)
        {
            checkpointMessageUI.SetActive(false);
        }
    }
}
