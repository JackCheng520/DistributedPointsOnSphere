using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// ================================
//* 功能描述：DragCtrl  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/11/3 14:19:32
// ================================
namespace Assets.JackCheng.distributed_points_on_sphere
{
    public class DragCtrl : MonoBehaviour
    {
        //是否被拖拽//    
        private bool beDrag = false;
        //旋转速度//   
        private float speed = 3f;
        //阻尼速度//    
        private float tempSpeed;
        //鼠标沿水平方向移动的增量// 
        private float axisX;
        //鼠标沿竖直方向移动的增量//
        private float axisY;
        //滑动距离（鼠标）
        private float cXY;

        //鼠标移动的距离    
        void OnMouseDown()
        {
            //接受鼠标按下的事件//
            axisX = 0f;
            axisY = 0f;
        }

        //鼠标拖拽时的操作
        void OnMouseDrag()
        {
            beDrag = true;
            axisX = -1*Input.GetAxis("Mouse X");
            axisY = Input.GetAxis("Mouse Y");
            Debug.logger.Log("X:"+axisX + " -- Y:" + axisY);
            cXY = Mathf.Sqrt(axisX * axisX + axisY * axisY);
            //计算鼠标移动的长度//
            if (cXY == 0f)
            {
                cXY = 1f;
            }
        }

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
                        tempSpeed -= speed * 2 * Time.deltaTime / cXY;
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
            // 根据计算出的阻尼和X，Y轴的偏移来旋转大球
            gameObject.transform.Rotate(new Vector3(axisY, axisX, 0) * Rigid(), Space.World);
            // 如果鼠标离开屏幕则标记为已经不再拖拽
            if (!Input.GetMouseButton(0))
            {
                beDrag = false;
            }
        }
    }
}
