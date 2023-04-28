/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVerticalText : BaseMeshEffect
{
    //public float _Spacing;//字符之间的距离
    //public bool _Virtical = false;
    private float lineSpacing = 1;
    private float textSpacing = 1;
    private float xOffset = 0;
    private float yOffset = 0;

    private int _TextCount = 0;
    private float _LastUpdateTime = 0f;
    public float UPDATE_INTERVAL = 0.1f;//更新间隔

    public GameObject _Text;

    public override void ModifyMesh(VertexHelper helper)
    {
        if(!IsActive())
        {
            return;
        }
        
        List<UIVertex> vertexs = new List<UIVertex>();
        helper.GetUIVertexStream(vertexs);

        Text textObject = _Text.GetComponent<Text>();
        string realText = textObject.text;

        TextGenerator textGenerator = textObject.cachedTextGenerator;
        lineSpacing = textObject.fontSize * lineSpacing;
        textSpacing = textObject.fontSize * textSpacing;
        //float textSpacing = textObject.fontSize * _Spacing;


        //x与y偏移，实现居中对齐
        float xOffset = textObject.rectTransform.sizeDelta.x / 2 - textObject.fontSize / 2;
        float yOffset = textObject.rectTransform.sizeDelta.y / 2 - textObject.fontSize / 2;

        List<UILineInfo> lines = new List<UILineInfo>();
        textGenerator.GetLines(lines);

        for(int i = 0; i < lines.Count; i++)
        {
            UILineInfo line = lines[i];

            int step = i;
            int current = 0;
            int endCharIdx = (i + 1 == lines.Count) ? textGenerator.characterCountVisible : lines[i + 1].startCharIdx - 1;
            
            for(int j = line.startCharIdx; j < endCharIdx; j++)
            {
                ModifyText(helper, j, step, current++);
            }
        }
    }

    void ModifyText(VertexHelper helper, int i, int charXPos, int charYPos)
    {
        UIVertex lbVertex = new UIVertex();
        helper.PopulateUIVertex(ref lbVertex, i * 4);

        UIVertex ltVertex = new UIVertex();
        helper.PopulateUIVertex(ref ltVertex, i * 4 + 1);

        UIVertex rtVertex = new UIVertex();
        helper.PopulateUIVertex(ref rtVertex, i * 4 + 2);

        UIVertex rbVertex = new UIVertex();
        helper.PopulateUIVertex(ref rbVertex, i * 4 + 3);

        Vector3 center = Vector3.Lerp(lbVertex.position, rtVertex.position, 0.5f);//插值，取lbVertex与rtVertex的中间位置
        Matrix4x4 move = Matrix4x4.TRS(-center, Quaternion.identity, Vector3.one);//平移矩阵

        float x = -charXPos * lineSpacing + xOffset;
        float y = -charYPos * textSpacing + yOffset;

        Vector3 pos = new Vector3(x, y, 0);
        Matrix4x4 place = Matrix4x4.TRS(pos, Quaternion.identity, Vector3.one);
        Matrix4x4 transform = place * move;

        lbVertex.position = transform.MultiplyPoint(lbVertex.position);
        ltVertex.position = transform.MultiplyPoint(ltVertex.position);
        rtVertex.position = transform.MultiplyPoint(rtVertex.position);
        rbVertex.position = transform.MultiplyPoint(rbVertex.position);

        helper.SetUIVertex(lbVertex, i * 4);
        helper.SetUIVertex(ltVertex, i * 4 + 1);
        helper.SetUIVertex(rtVertex, i * 4 + 2);
        helper.SetUIVertex(rbVertex, i * 4 + 3);
    }


    private void Update()
    {
        *//*if(_TextCount >= this.GetComponent<Text>().text.Length || Time.time - _LastUpdateTime < UPDATE_INTERVAL)
        {
            return;
        }
        _TextCount++;
        this.GetComponent<Text>().text = this.GetComponent<Text>().text.Substring(0, _TextCount);
        _LastUpdateTime = Time.time;*//*
    }
}
*/