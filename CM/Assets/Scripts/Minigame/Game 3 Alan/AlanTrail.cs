using UnityEngine;

public class AlanTrail : MonoBehaviour
{
    TrailRenderer trailRenderer;
    float delay;

    void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        delay = 0.25f;
        this.trailRenderer.enabled = false;
        this.trailRenderer.emitting = false;
    }

    public void Play()
    {
        Play(0.25f);
    }

    public void Play(float delay)
    {
        this.trailRenderer.Clear();

        this.trailRenderer.enabled = true;
        this.trailRenderer.emitting = false;

        this.delay = delay;
    }

    public void Stop()
    {
        this.trailRenderer.Clear();
        this.trailRenderer.enabled = false;
        this.trailRenderer.emitting = false;
    }
    public void Reset()
    {
        Awake();
    }

    private void Update()
    {
        if (delay <= 0) return;

        delay -= Time.deltaTime;

        if (delay <= 0)
        {
            trailRenderer.emitting = true;
        }
    }
}
