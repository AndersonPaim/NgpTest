using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DamageFeedback : MonoBehaviour
{
    [SerializeField] private GameObject _destroyParticle;
    [SerializeField] private Material _damageMaterial;
    [SerializeField] private Material _killMaterial;
    [SerializeField] private float _materialChangeDuration;

    private List<Material> _defaultMaterials = new();
    private MeshRenderer[] _meshRenderers;

    public async void Damage()
    {
        SetDamageMaterial();
        await UniTask.Delay(TimeSpan.FromSeconds(_materialChangeDuration));
        SetDefaultMaterial();
    }

    public void Kill()
    {
        GameObject killParticle = ObjectPooler.Instance.SpawnFromPool(_destroyParticle);
        killParticle.transform.position = transform.position;
    }

    private void Start()
    {
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        
        foreach (MeshRenderer meshRenderer in _meshRenderers)
        {
            _defaultMaterials.Add(meshRenderer.material);
        }
    }

    private void SetDamageMaterial()
    {
        foreach (MeshRenderer meshRenderer in _meshRenderers)
        {
            meshRenderer.material = _damageMaterial;
        }
    }
    
    private void SetDefaultMaterial()
    {
        for (int i = 0; i < _meshRenderers.Length; i++)
        {
            _meshRenderers[i].material = _defaultMaterials[i];
        }
    }
}
