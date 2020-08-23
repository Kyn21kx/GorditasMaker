using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Programming {
    public static class PhysicsOperations {

        public static void LerpVelocity (ref Rigidbody rg, Vector3 dir, float speed) {
            Vector3 pos = Vector3.Lerp(rg.velocity, speed * dir, Time.deltaTime * 8f);
            rg.velocity = pos;
        }

    }
}
