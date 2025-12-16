using System;
using Cysharp.Threading.Tasks;
using Digi.VisualScreen;
using UnityEngine;

public class DamageFeedback : MonoBehaviour
{
    [SerializeField] private GameObject _destroyParticle;
    [SerializeField] private Material _damageMaterial;
    [SerializeField] private Material _killMaterial;
    [SerializeField] private float _materialChangeDuration;

    private Material _defaultMaterial;
    private MeshRenderer _meshRenderer;

    public async void Damage()
    {
        _meshRenderer.material = _damageMaterial;
        await UniTask.Delay(TimeSpan.FromSeconds(_materialChangeDuration));
        _meshRenderer.material = _defaultMaterial;
    }

    public void Kill()
    {
        GameObject killParticle = ObjectPooler.Instance.SpawnFromPool(_destroyParticle);
        killParticle.transform.position = transform.position;
    }

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _defaultMaterial = _meshRenderer.material;
    }
}
