using Assets.JackCheng.Probe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// ================================
//* 功能描述：球体上平均分布点  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/11/3 13:12:06
// ================================
namespace Assets.JackCheng.distributed_points_on_sphere
{
    public class DistributedPointOnSphere : MonoBehaviour
    {
        public int distributedNum;

        public int sphereSize;

        public bool Action;

        private J_Echo echoSystem = new J_Echo();

        private List<Vector3> listDistributedPoints;

        private int idx = 0;

        void Update() 
        {
            if (Action) {
                Action = false;
                DistributedPoints();
            }
        }

        void LateUpdate() {
            echoSystem.Update();
        }

        private void DistributedPoints() 
        {
            echoSystem.Lock();
            listDistributedPoints = FibonacciSphere(distributedNum, sphereSize);
            for (int i = 0; i < listDistributedPoints.Count; i++) 
            {
                echoSystem.Add(CreatePoint, 0.1f);
            }

            idx = 0;
            echoSystem.UnLock();
        }

        private void CreatePoint() 
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.SetParent(this.transform,false);
            go.transform.position = listDistributedPoints[idx];
            go.transform.localScale = Vector3.one;
            idx++;
        }

        public List<Vector3> DistributedPos(int N, int size)
        {
            List<Vector3> listPoints = new List<Vector3>();
            float inc = Mathf.PI * (3f - Mathf.Sqrt(5f));
            float off = 2f / N;
            for (int k = 0; k < N; k++)
            {
                float y = k * off - 1 + (off / 2f);
                float r = Mathf.Sqrt(1 - y * y);
                float phi = k * inc;
                listPoints.Add(new Vector3(Mathf.Cos(phi) * r * size, y * size, Mathf.Sin(phi) * r * size));
            }
            return listPoints;
        }

        public List<Vector3> FibonacciSphere(int samples,int size)
        {
            float rnd = 1;
            List<Vector3> points = new List<Vector3>();
            float offset = 2f / samples;
            double increment = Math.PI * (3f- Math.Sqrt(5f));

            for (int i = 0; i < samples; i++)
            {
                float y = ((i * offset) - 1f) + (offset / 2f);
                double r = Math.Sqrt(1f - Math.Pow(y, 2f));

                double phi = ((i + rnd) % samples) * increment;

                double x = Math.Cos(phi) * r;
                double z = Math.Sin(phi) * r;

                points.Add(new Vector3((float)x * size, (float)y * size, (float)z * size));

            }

            return points;
        }

    }
}
