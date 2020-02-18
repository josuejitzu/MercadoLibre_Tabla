using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTG

{
    public class ActivarGizmos : MonoBehaviour
    {
        public string nombreObjeto;
        public bool activado;

        ObjectTransformGizmo objectTransformGizmo;
        ObjectTransformGizmo objectRotationGizmo;
        // Start is called before the first frame update
        void Start()
        {
            objectTransformGizmo = RTGizmosEngine.Get.CreateObjectMoveGizmo();
            GameObject targetObject = GameObject.Find(nombreObjeto);
            objectTransformGizmo.SetTargetObject(targetObject);
            objectTransformGizmo.Gizmo.SetEnabled(false);

            objectRotationGizmo = RTGizmosEngine.Get.CreateObjectRotationGizmo();
            GameObject targetObjectR = GameObject.Find(nombreObjeto);
            objectRotationGizmo.SetTargetObject(targetObject);
            objectRotationGizmo.Gizmo.SetEnabled(false);


        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    ActivarGizmo();
                }
            }
        }

        public void ActivarGizmo()
        {
            if (!activado)
            {
                objectTransformGizmo.Gizmo.SetEnabled(true);
              //  objectRotationGizmo.Gizmo.SetEnabled(true);

                activado = true;
            }
            else if(activado)
            {
              objectTransformGizmo.Gizmo.SetEnabled(false);
              //  objectRotationGizmo.Gizmo.SetEnabled(false);


                activado = false;
            }
        }
    }
}
