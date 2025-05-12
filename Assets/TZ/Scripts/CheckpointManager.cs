using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private List<Checkpoint> checkpoints;
    [SerializeField] private bool isLooping;
    [SerializeField] private GameObject bezierFollower;

    private int currentIndex;

    public event Action OnRouteCompleted;

    private void Start()
    {
        InitializeCheckpoints();
        UpdateBezierPath();
    }

    private void InitializeCheckpoints()
    {
        for (int i = 0; i < checkpoints.Count; i++)
        {
            checkpoints[i].Index = i;
            checkpoints[i].OnPlayerPassed += OnCheckpointPassed;
        }

        currentIndex = 0;
    }

    private void OnCheckpointPassed(Checkpoint checkpoint)
    {
        if (checkpoint.Index != currentIndex) return;

        currentIndex++;
        if (currentIndex >= checkpoints.Count)
        {
            OnRouteCompleted?.Invoke();
            currentIndex = isLooping ? 0 : checkpoints.Count - 1;
        }

        UpdateBezierPath();
    }

    public void ResetRoute()
    {
        currentIndex = 0;
        UpdateBezierPath();
    }

    private void UpdateBezierPath()
    {
        if (currentIndex < checkpoints.Count - 1)
        {
            BezierCurveRenderer.Instance.DrawCurve(
                checkpoints[currentIndex].transform.position,
                checkpoints[(currentIndex + 1) % checkpoints.Count].transform.position
            );
        }
    }

    public void AddCheckpoint(Checkpoint checkpoint)
    {
        checkpoints.Add(checkpoint);
        InitializeCheckpoints();
    }

    public void RemoveCheckpoint(Checkpoint checkpoint)
    {
        checkpoints.Remove(checkpoint);
        InitializeCheckpoints();
    }
}