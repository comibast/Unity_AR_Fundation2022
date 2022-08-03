using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;


namespace Comibast
{
    /// <summary>
    /// 點擊後生成物件
    /// </summary>
    [RequireComponent(typeof(ARRaycastManager))]
    public class TapToSpawnObject : MonoBehaviour
    {
        #region
        [SerializeField, Header("要生成的物件")]
        private GameObject goSpawnObject;

        private ARRaycastManager arManager;
        private Vector2 touchPoint;
        private List<ARRaycastHit> hits = new List<ARRaycastHit>();
        #endregion

        private void Awake()
        {
            arManager = GetComponent<ARRaycastManager>();
        }

        private void Update()
        {
            TapAndSpawn();
        }

        /// <summary>
        /// 點擊與生成
        /// </summary>
        private void TapAndSpawn()
        {
            // 如果 點擊 左鍵或螢幕
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                // 儲存點擊座標
                touchPoint = Input.mousePosition;
                // 射線管理器.射線碰撞(座標, 碰撞物件清單, 類型)
                if (arManager.Raycast(touchPoint, hits, TrackableType.PlaneWithinPolygon))
                {
                    // 儲存射線碰撞物件第一筆的座標資訊
                    Pose pose = hits[0].pose;
                    // 生成(物件, 座標, 角度)
                    GameObject temp = Instantiate(goSpawnObject, pose.position, Quaternion.identity);
                    // 攝影機座標
                    Vector3 cameraPos = transform.position;
                    // 攝影機 Y = 生成物件的 Y
                    cameraPos.y = temp.transform.position.y;
                    // 生成物件 面向(攝影機座標)
                    temp.transform.LookAt(cameraPos);
                }
            }
        }

    }


}

