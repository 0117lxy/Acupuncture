using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class ClickToInstantiate : MonoBehaviour
{
    public Button _Button;//点击生成针
    public GameObject _NiddlePrefab;//被生成的针的prefab
    public GameObject _NiddleObject;//实例化的针
    public Quaternion _NiddleRotation;//旋转
    public float _NiddleOffsetX;//X轴偏移
    public float _NiddleOffsetY;//Y轴偏移
    public float _NiddleOffsetZ;//Z轴偏移
    private Vector3 _NiddleOffset;
    private Vector3 _MousePos;//鼠标位置
    private Vector3 _NiddlePos;//针的位置
    public float _NiddleHeight;//针的高度

    public bool _IsSpawn;//是否实例化针
    public bool _IsAcupuncture;//是否正在下针

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
