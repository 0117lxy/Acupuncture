using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowLightController : MonoBehaviour
{
    private float widthRate = 1;
    private float heightRate = 1;
    private float xOffsetRate = 0;
    private float yOffsetRate = 0;
    private MaskableGraphic maskableGraphic;
    public Texture2D flowTex;
    public Color tintCol;
    public float speed = 2;
    public Vector2 tiling = Vector2.one;
    public float amScale = 0.05f;
    public float width = 1;
    public float power = 1f;
    public float LargeWidth = 1f;
    public float LittleWidth = 1f;


    void Awake()
    {
        maskableGraphic = GetComponent<MaskableGraphic>();
        if (maskableGraphic)
        {
            Image image = maskableGraphic as Image;
            if (image)
            {
                image.material = new Material(Shader.Find("Unlit/FlowLight"));
                widthRate = image.sprite.textureRect.width * 1.0f / image.sprite.texture.width;
                heightRate = image.sprite.textureRect.height * 1.0f / image.sprite.texture.height;
                xOffsetRate = (image.sprite.textureRect.xMin) * 1.0f / image.sprite.texture.width;
                yOffsetRate = (image.sprite.textureRect.yMin) * 1.0f / image.sprite.texture.height;
            }
        }
        Debug.Log(string.Format(" widthRate{0}, heightRate{1}�� xOffsetRate{2}�� yOffsetRate{3}", widthRate, heightRate, xOffsetRate, yOffsetRate));
    }
    void Start()
    {
        SetShader();
    }
    //����ʹ�ã��ɿ�ʵʱЧ������Ŀ�н���ر�
    void Update()
    {
        SetShader();
    }
    public void SetShader()
    {
        maskableGraphic.material.SetTexture("_FlowTex", flowTex);
        maskableGraphic.material.SetColor("_FlowlightColor", tintCol);
        maskableGraphic.material.SetFloat("_MoveSpeed", speed);
        maskableGraphic.material.SetVector("_Tiling", tiling);
        maskableGraphic.material.SetFloat("_AmScale", amScale);
        maskableGraphic.material.SetFloat("_WidthRate", widthRate);
        maskableGraphic.material.SetFloat("_HeightRate", heightRate);
        maskableGraphic.material.SetFloat("_XOffset", xOffsetRate);
        maskableGraphic.material.SetFloat("_YOffset", yOffsetRate);
        maskableGraphic.material.SetFloat("_ClipSoftX", 10);
        maskableGraphic.material.SetFloat("_ClipSoftY", 10);
        maskableGraphic.material.SetFloat("_Width", width);
        maskableGraphic.material.SetFloat("_Power", power);
        maskableGraphic.material.SetFloat("_LargeWidth", LargeWidth);
        maskableGraphic.material.SetFloat("_LittleWidth", LittleWidth);
    }

}