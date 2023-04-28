using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class ClickToInstantiate : MonoBehaviour
{
    public Button _Button;//���������
    public GameObject _NiddlePrefab;//�����ɵ����prefab
    public GameObject _NiddleObject;//ʵ��������
    public Quaternion _NiddleRotation;//��ת
    public float _NiddleOffsetX;//X��ƫ��
    public float _NiddleOffsetY;//Y��ƫ��
    public float _NiddleOffsetZ;//Z��ƫ��
    private Vector3 _NiddleOffset;
    private Vector3 _MousePos;//���λ��
    private Vector3 _NiddlePos;//���λ��
    public float _NiddleHeight;//��ĸ߶�

    public bool _IsSpawn;//�Ƿ�ʵ������
    public bool _IsAcupuncture;//�Ƿ���������

    public Transform _Target;
    private Vector3 _TargetPos;
    public float _MoveSpeed;
    public Vector3 _MaxMoveDis;
    private Vector3 _MaxMovePos;
    
    // Start is called before the first frame update
    void Start()
    {
        _IsSpawn = false;
        _IsAcupuncture = false;
        _NiddleOffset = new Vector3(_NiddleOffsetX, _NiddleOffsetY, _NiddleOffsetZ);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_IsSpawn == true && _IsAcupuncture == false)
        {
            _MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _NiddleObject.transform.position = _MousePos + _NiddleOffset;
        }
        if(_IsSpawn == true && _IsAcupuncture == true)
        {
            _MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public void OnClick()
    {
        if(_IsSpawn == false)
        {
            _MousePos = Camera.main.ScreenToWorldPoint(_MousePos);
            _NiddleObject = Instantiate(_NiddlePrefab, _MousePos + _NiddleOffset, _NiddleRotation);
            _IsSpawn = true;
            //Bounds bounds = _NiddleObject.GetComponent<MeshRenderer>().bounds;
            //_NiddleHeight = bounds.size.y;
        }
    }

    
}
