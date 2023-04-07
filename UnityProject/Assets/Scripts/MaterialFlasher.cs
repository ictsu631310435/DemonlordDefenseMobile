using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script for flashing materials
/// </summary>
public class MaterialFlasher : MonoBehaviour
{
    #region Data Members
    [Header("# Can use both types of Mesh Renderers")]
    public MeshRenderer[] meshRenderers;
    public SkinnedMeshRenderer[] skinnedMeshRenderers;

    private Material[] _ogMaterials;
    private Material[] _ogSkinnedMaterials;

    [Tooltip("Material that will flash")]
    public Material flashMaterial;

    [Tooltip("Duration that material will flash")]
    public float flashDuration;

    private Coroutine[] _flashCoroutines;
    private int _coroutineCount;
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    void Start()
    {
        // set _coroutineCount based on Length, the greater Length will be the value
        _coroutineCount = meshRenderers.Length > skinnedMeshRenderers.Length ? 
            meshRenderers.Length : skinnedMeshRenderers.Length;

        _flashCoroutines = new Coroutine[_coroutineCount];

        if (meshRenderers != null)
        {
            _ogMaterials = new Material[meshRenderers.Length];

            for (int i = 0; i < meshRenderers.Length; i++)
            {
                _ogMaterials[i] = meshRenderers[i].material;
            }
        }

        if (skinnedMeshRenderers != null)
        {
            _ogSkinnedMaterials = new Material[skinnedMeshRenderers.Length];

            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                _ogSkinnedMaterials[i] = skinnedMeshRenderers[i].material;
            }
        }
    }
    #endregion

    #region Methods
    // Method for starting FlashCoroutines
    public void Flash()
    {
        // If there is a _flashCoroutines running, stop it
        if (_flashCoroutines != null)
        {
            StopAllCoroutines();
        }
        // Start new _flashCoruotines
        for (int i = 0; i < _coroutineCount; i++)
        {
            _flashCoroutines[i] = StartCoroutine(FlashCoroutine(i));
        }
    }

    // Method for flashing Material
    private IEnumerator FlashCoroutine(int i)
    {
        // Change original Material to flashMaterial
        if (i < meshRenderers.Length)
        {
            meshRenderers[i].material = flashMaterial;
        }
        if (i < skinnedMeshRenderers.Length)
        {
            skinnedMeshRenderers[i].material = flashMaterial;
        }

        // Wait for flashDuration
        yield return new WaitForSeconds(flashDuration);

        // Change back to original Material
        if (i < meshRenderers.Length)
        {
            meshRenderers[i].material = _ogMaterials[i];
        }
        if (i < skinnedMeshRenderers.Length)
        {
            skinnedMeshRenderers[i].material = _ogSkinnedMaterials[i];
        }

        // Reset after finished
        _flashCoroutines[i] = null;
    }

    public void FindMeshRenderers()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    public void ResetMeshRenderers()
    {
        meshRenderers = null;

        skinnedMeshRenderers = null;
    }
    #endregion
}
