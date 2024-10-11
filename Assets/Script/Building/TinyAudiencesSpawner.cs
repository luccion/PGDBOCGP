using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TinyAudiencesSpawner : MonoBehaviour
{
    [SerializeField] GameObject audiencePrefab;
    [Range(1, 3000)]
    [SerializeField] int poolSize = 20;
    private readonly Queue<GameObject> audiencePool = new();
    private SpriteRenderer areaSprite;
    void Start()
    {
        Initialize();
        GenerateAudiences();
    }

    private void Initialize()
    {
        areaSprite = this.GetComponent<SpriteRenderer>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject audience = Instantiate(audiencePrefab, this.transform);
            audience.SetActive(false);
            audiencePool.Enqueue(audience);
        }
    }

    public GameObject GetAudience()
    {
        if (audiencePool.Count > 0)
        {
            GameObject audience = audiencePool.Dequeue();
            audience.SetActive(true);
            return audience;
        }
        else
        {
            Debug.LogWarning("对象池已用尽，无法获取新的小观众。");
            return null; // 池已用尽
        }
    }

    public void ReturnAudience(GameObject audience)
    {
        audience.SetActive(false);
        audiencePool.Enqueue(audience);
    }

    private void HideAreaSprite()
    {    // 禁用或隐藏透明区域的SpriteRenderer
        if (areaSprite.TryGetComponent<SpriteRenderer>(out var areaSpriteRenderer))
        {
            areaSpriteRenderer.color = new Color(0, 0, 0, 0);
        }

    }
    private void GenerateAudiences()
    {

        // 获取透明Sprite的大小，定义生成区域
        SpriteRenderer areaSpriteRenderer = areaSprite.GetComponent<SpriteRenderer>();
        if (areaSpriteRenderer != null)
        {
            Bounds bounds = areaSpriteRenderer.bounds; // 获取透明Sprite的边界
            foreach (GameObject audience in audiencePool)
            {
                if (audience != null)
                {
                    // 在Sprite的边界内随机生成位置
                    float randomX = Random.Range(bounds.min.x, bounds.max.x);
                    float randomY = Random.Range(bounds.min.y, bounds.max.y);
                    audience.transform.position = new Vector3(randomX, randomY, 0);
                    audience.SetActive(true);
                }
            }
        }
        HideAreaSprite();

    }
#if UNITY_EDITOR
    [ContextMenu("Respawn Audiences")]
    public void RespawnAudiences()
    {
        foreach (GameObject audience in audiencePool)
        {
            audience.SetActive(false);
        }
        GenerateAudiences();
    }


    [CanEditMultipleObjects]
    [CustomEditor(typeof(TinyAudiencesSpawner))]
    public class AudiencePoolEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            TinyAudiencesSpawner pool = (TinyAudiencesSpawner)target;
            if (GUILayout.Button("Respawn Audiences"))
            {
                pool.RespawnAudiences();
            }
        }
    }
#endif
}



