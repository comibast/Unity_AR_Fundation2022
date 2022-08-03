using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;


namespace Comibast
{
    /// <summary>
    /// �I����ͦ�����
    /// </summary>
    [RequireComponent(typeof(ARRaycastManager))]
    public class TapToSpawnObject : MonoBehaviour
    {
        #region
        [SerializeField, Header("�n�ͦ�������")]
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
        /// �I���P�ͦ�
        /// </summary>
        private void TapAndSpawn()
        {
            // �p�G �I�� ����οù�
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                // �x�s�I���y��
                touchPoint = Input.mousePosition;
                // �g�u�޲z��.�g�u�I��(�y��, �I������M��, ����)
                if (arManager.Raycast(touchPoint, hits, TrackableType.PlaneWithinPolygon))
                {
                    // �x�s�g�u�I������Ĥ@�����y�и�T
                    Pose pose = hits[0].pose;
                    // �ͦ�(����, �y��, ����)
                    GameObject temp = Instantiate(goSpawnObject, pose.position, Quaternion.identity);
                    // ��v���y��
                    Vector3 cameraPos = transform.position;
                    // ��v�� Y = �ͦ����� Y
                    cameraPos.y = temp.transform.position.y;
                    // �ͦ����� ���V(��v���y��)
                    temp.transform.LookAt(cameraPos);
                }
            }
        }

    }


}

