using Microsoft.ML.Data;

namespace ModelApi
{
    public class HandGestureNumber
    {

        public Vector3Data Wrist { get; set; }
        public Vector3Data ThumbMetacarpalJoint { get; set; }
        public Vector3Data ThumbProximalInterphalangealJoint { get; set; }
        public Vector3Data ThumbDistalInterphalangealJoint { get; set; }
        public Vector3Data ThumbTip { get; set; }
        public Vector3Data IndexFingerMetacarpalJoint { get; set; }
        public Vector3Data IndexFingerProximalInterphalangealJoint { get; set; }
        public Vector3Data IndexFingerDistalInterphalangealJoint { get; set; }
        public Vector3Data IndexFingerTip { get; set; }
        public Vector3Data MiddleFingerMetacarpalJoint { get; set; }
        public Vector3Data MiddleFingerProximalInterphalangealJoint { get; set; }
        public Vector3Data MiddleFingerDistalInterphalangealJoint { get; set; }
        public Vector3Data MiddleFingerTip { get; set; }
        public Vector3Data RingFingerMetacarpalJoint { get; set; }
        public Vector3Data RingFingerProximalInterphalangealJoint { get; set; }
        public Vector3Data RingFingerDistalInterphalangealJoint { get; set; }
        public Vector3Data RingFingerTip { get; set; }
        public Vector3Data LittleFingerMetacarpalJoint { get; set; }
        public Vector3Data LittleFingerProximalInterphalangealJoint { get; set; }
        public Vector3Data LittleFingerDistalInterphalangealJoint { get; set; }
        public Vector3Data LittleFingerTip { get; set; }
        
        public float Label { get; set; }
    }
}
