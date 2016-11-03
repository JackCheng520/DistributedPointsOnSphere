using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// ================================
//* 功能描述：CameraDragCtrl  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/11/3 14:58:14
// ================================
namespace Assets.JackCheng.distributed_points_on_sphere
{
    class CameraDragCtrl : MonoBehaviour
    {
        //是否被拖拽//    
        private bool beDrag = false;
        //旋转速度//   
        [SerializeField]
        private float speed = 0.1f;
        //阻尼速度//    
        private float tempSpeed;
        //鼠标沿水平方向移动的增量// 
        private float axisX;
        //鼠标沿竖直方向移动的增量//
        private float axisY;
        //滑动距离（鼠标）
        private float cXY;
        [SerializeField]
        private float rigidFactor = 2f;


        //计算阻尼速度
        float Rigid()
        {
            if (beDrag)
            {
                tempSpeed = speed;
            }
            else
            {
                if (tempSpeed > 0)
                {
                    //通过除以鼠标移动长度实现拖拽越长速度减缓越慢    
                    if (cXY != 0)
                    {
                        tempSpeed -= speed * rigidFactor * Time.deltaTime / cXY;
                    }
                }
                else
                {
                    tempSpeed = 0;
                }
            }
            return tempSpeed;
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //接受鼠标按下的事件//
                axisX = 0f;
                axisY = 0f;
            }

            if (Input.GetMouseButton(0)) {
                beDrag = true;
                axisX = -1*Input.GetAxis("Mouse X");
                axisY = -1*Input.GetAxis("Mouse Y");
                //Debug.Log("X: "+axisX+" Y: "+axisY);
                cXY = Mathf.Sqrt(axisX * axisX + axisY * axisY);
                //计算鼠标移动的长度//
                if (cXY == 0f)
                {
                    cXY = 1f;
                }
                
            }

            if (Input.GetMouseButtonUp(0)) {
                // 如果鼠标离开屏幕则标记为已经不再拖拽
                beDrag = false;
            }

        }
        [SerializeField]
        private float lerpSpeed = 1f;
        void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(axisX, axisY, 0) * Rigid(), lerpSpeed);
            //transform.Translate(new Vector3(axisX, axisY, 0) * Rigid(), Space.World);
        }
    }
}
