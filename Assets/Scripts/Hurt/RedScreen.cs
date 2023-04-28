using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedScreen : PostEffectsBase
{
    public Shader m_RedScreenShader;
    private Material m_RedScreenMaterial;

    //�Ƿ�Ҫ��ʾ��ɫ��Ļ
    public bool m_RedScreenEnabled = false;

    public Texture2D m_RedScreenTexture;

    //͸����
    [Range(0f, 1f)]
    public float m_Alpha = 0.5f;

    public Material material
    {
        get
        {
            return m_RedScreenMaterial = CheckShaderAndCreateMaterial(m_RedScreenShader, m_RedScreenMaterial);
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null && m_RedScreenTexture != null)
        {
            //ʵ�ֺ���Ч�����жϣ�д�ò�̫��
            if (!m_RedScreenEnabled)
            {
                m_Alpha += Time.deltaTime;
                if (m_Alpha >= 1.0)
                {
                    m_RedScreenEnabled = true;
                }
            }
            else
            {
                m_Alpha -= Time.deltaTime;
                if (m_Alpha <= 0)
                {
                    m_RedScreenEnabled = false;
                }
            }

            material.SetTexture("_BloodTex", m_RedScreenTexture);
            material.SetFloat("_Alpha", m_Alpha);
            Graphics.Blit(src, dest, material);

        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }

}
