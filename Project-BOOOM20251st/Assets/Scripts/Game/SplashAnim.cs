using UnityEngine;
using DG.Tweening;

public class TextureChanger : MonoBehaviour
{
    public Renderer targetRenderer; // 目标渲染器组件
    public Texture[] textures;     // 纹理数组，包含要更换的纹理
    private int currentTextureIndex = 0; // 当前纹理索引
    private Vector3 startScale;
    public Vector3 targetScale = new Vector3(10f, 10f, 10f);

    void Start()
    {
        transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

        // 检查是否有目标渲染器和纹理数组
        if (targetRenderer == null)
        {
            Debug.LogError("Target Renderer is not assigned.");
            return;
        }

        if (textures == null || textures.Length == 0)
        {
            Debug.LogError("Textures array is empty.");
            return;
        }

        // 初始化第一个纹理
        targetRenderer.material.mainTexture = textures[currentTextureIndex];

        // 每隔 0.5 秒调用一次 ChangeTexture 方法
        InvokeRepeating("ChangeTexture", 0.2f, 0.2f);

        transform.DOScale(targetScale, 0.5f);
    }

    void ChangeTexture()
    {
        // 切换到下一个纹理
        currentTextureIndex++;
        targetRenderer.material.mainTexture = textures[currentTextureIndex];
        if (currentTextureIndex >= textures.Length)
        {
            CancelInvoke("ChangeTexture");
        }

    }


        void OnDestroy()
    {
        // 停止调用 ChangeTexture 方法
        CancelInvoke("ChangeTexture");
    }
    
}