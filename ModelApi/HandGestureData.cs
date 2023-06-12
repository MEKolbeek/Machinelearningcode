using Microsoft.ML.Data;
using System.IO;

namespace ModelApi
{
    public class HandGestureDataSet
    {
        [LoadColumn(0)]
        public float Wrist_x { get; set; }

        [LoadColumn(1)]
        public float Wrist_y { get; set; }

        [LoadColumn(2)]
        public float Wrist_z { get; set; }

        [LoadColumn(3)]
        public float ThumbMetacarpal_x { get; set; }

        [LoadColumn(4)]
        public float ThumbMetacarpal_y { get; set; }

        [LoadColumn(5)]
        public float ThumbMetacarpal_z { get; set; }

        [LoadColumn(6)]
        public float ThumbProximal_x { get; set; }

        [LoadColumn(7)]
        public float ThumbProximal_y { get; set; }

        [LoadColumn(8)]
        public float ThumbProximal_z { get; set; }

        [LoadColumn(9)]
        public float ThumbDistal_x { get; set; }

        [LoadColumn(10)]
        public float ThumbDistal_y { get; set; }

        [LoadColumn(11)]
        public float ThumbDistal_z { get; set; }

        [LoadColumn(12)]
        public float ThumbTip_x { get; set; }

        [LoadColumn(13)]
        public float ThumbTip_y { get; set; }

        [LoadColumn(14)]
        public float ThumbTip_z { get; set; }

        [LoadColumn(15)]
        public float IndexFingerMetacarpal_x { get; set; }

        [LoadColumn(16)]
        public float IndexFingerMetacarpal_y { get; set; }

        [LoadColumn(17)]
        public float IndexFingerMetacarpal_z { get; set; }

        [LoadColumn(18)]
        public float IndexFingerProximal_x { get; set; }

        [LoadColumn(19)]
        public float IndexFingerProximal_y { get; set; }

        [LoadColumn(20)]
        public float IndexFingerProximal_z { get; set; }

        [LoadColumn(21)]
        public float IndexFingerDistal_x { get; set; }

        [LoadColumn(22)]
        public float IndexFingerDistal_y { get; set; }

        [LoadColumn(23)]
        public float IndexFingerDistal_z { get; set; }

        [LoadColumn(24)]
        public float IndexFingerTip_x { get; set; }

        [LoadColumn(25)]
        public float IndexFingerTip_y { get; set; }

        [LoadColumn(26)]
        public float IndexFingerTip_z { get; set; }

        [LoadColumn(27)]
        public float MiddleFingerMetacarpal_x { get; set; }

        [LoadColumn(28)]
        public float MiddleFingerMetacarpal_y { get; set; }

        [LoadColumn(29)]
        public float MiddleFingerMetacarpal_z { get; set; }

        [LoadColumn(30)]
        public float MiddleFingerProximal_x { get; set; }

        [LoadColumn(31)]
        public float MiddleFingerProximal_y { get; set; }

        [LoadColumn(32)]
        public float MiddleFingerProximal_z { get; set; }

        [LoadColumn(33)]
        public float MiddleFingerDistal_x { get; set; }

        [LoadColumn(34)]
        public float MiddleFingerDistal_y { get; set; }

        [LoadColumn(35)]
        public float MiddleFingerDistal_z { get; set; }

        [LoadColumn(36)]
        public float MiddleFingerTip_x { get; set; }

        [LoadColumn(37)]
        public float MiddleFingerTip_y { get; set; }

        [LoadColumn(38)]
        public float MiddleFingerTip_z { get; set; }

        [LoadColumn(39)]
        public float RingFingerMetacarpal_x { get; set; }

        [LoadColumn(40)]
        public float RingFingerMetacarpal_y { get; set; }

        [LoadColumn(41)]
        public float RingFingerMetacarpal_z { get; set; }

        [LoadColumn(42)]
        public float RingFingerProximal_x { get; set; }

        [LoadColumn(43)]
        public float RingFingerProximal_y { get; set; }

        [LoadColumn(44)]
        public float RingFingerProximal_z { get; set; }

        [LoadColumn(45)]
        public float RingFingerDistal_x { get; set; }

        [LoadColumn(46)]
        public float RingFingerDistal_y { get; set; }

        [LoadColumn(47)]
        public float RingFingerDistal_z { get; set; }

        [LoadColumn(48)]
        public float RingFingerTip_x { get; set; }

        [LoadColumn(49)]
        public float RingFingerTip_y { get; set; }

        [LoadColumn(50)]
        public float RingFingerTip_z { get; set; }

        [LoadColumn(51)]
        public float LittleFingerMetacarpal_x { get; set; }

        [LoadColumn(52)]
        public float LittleFingerMetacarpal_y { get; set; }

        [LoadColumn(53)]
        public float LittleFingerMetacarpal_z { get; set; }

        [LoadColumn(54)]
        public float LittleFingerProximal_x { get; set; }

        [LoadColumn(55)]
        public float LittleFingerProximal_y { get; set; }

        [LoadColumn(56)]
        public float LittleFingerProximal_z { get; set; }

        [LoadColumn(57)]
        public float LittleFingerDistal_x { get; set; }

        [LoadColumn(58)]
        public float LittleFingerDistal_y { get; set; }

        [LoadColumn(59)]
        public float LittleFingerDistal_z { get; set; }

        [LoadColumn(60)]
        public float LittleFingerTip_x { get; set; }

        [LoadColumn(61)]
        public float LittleFingerTip_y { get; set; }

        [LoadColumn(62)]
        public float LittleFingerTip_z { get; set; }

        [LoadColumn(63), ColumnName("Label")]
        public float Label { get; set; }
    }

    // Definieer de structuur van de dataset
    public class HandGestureData
    {
        // Eigenschap die de kenmerken (features) bevat
        [LoadColumn(0, 62), VectorType(63)]
        public float[] Features { get; set; }

        // Eigenschap die de label bevat
        [LoadColumn(63), ColumnName("Label")]
        public float Label { get; set; }
    }
}