using System;
using System.Collections.Generic;
using System.Text;

namespace F3DEX2Decoder
{
    public enum GeometryModeOperation
    {
        Set,
        Clear,
        Load,
        Both,
    }

    class GeometryMode
    {
        public static readonly string[] MODE_NAMES =
        {
            "G_ZBUFFER",            // 0x00000001
            "0x00000002",           // 0x00000002
            "G_SHADE",              // 0x00000004
            "0x00000008",           // 0x00000008
            "0x00000010",           // 0x00000010
            "0x00000020",           // 0x00000020
            "0x00000040",           // 0x00000040
            "0x00000080",           // 0x00000080
            "0x00000100",           // 0x00000100
            "G_CULL_FRONT",         // 0x00000200
            "G_CULL_BACK",          // 0x00000400
            "0x00000800",           // 0x00000800
            "0x00001000",           // 0x00001000
            "0x00002000",           // 0x00002000
            "0x00004000",           // 0x00004000
            "0x00008000",           // 0x00008000
            "G_FOG",                // 0x00010000
            "G_LIGHTING",           // 0x00020000
            "G_TEXTURE_GEN",        // 0x00040000
            "G_TEXTURE_GEN_LINEAR", // 0x00080000
            "G_LOD",                // 0x00100000
            "G_SHADING_SMOOTH",     // 0x00200000
            "G_POINT_LIGHTING",     // 0x00400000
            "G_CLIPPING",           // 0x00800000
        };

        public readonly ulong ClearValue;
        public readonly ulong SetValue;
        public readonly string ClearString;
        public readonly string SetString;

        public readonly GeometryModeOperation Operation;
        public readonly ulong OperationValue;
        public readonly string OperationString;

        public static string ModeValueToString(ulong value)
        {
            string valueString = "";

            if (value == 0)
            {
                valueString = "0x00000000";
            }
            else
            {
                for (int i = 0; i < MODE_NAMES.Length; i++)
                {
                    if ((value & 0x01) != 0)
                    {
                        valueString += MODE_NAMES[i] + " | ";
                    }
                    value >>= 1;
                }
                valueString = valueString.Substring(0, valueString.Length - 3);
            }

            return valueString;
        }

        public GeometryMode(ulong words)
        {
            ClearValue = ((~words) >> 32) & 0xFFFFFF;
            SetValue =   (( words) >>  0) & 0xFFFFFF;
            ClearString = ModeValueToString(ClearValue);
            SetString = ModeValueToString(SetValue);

            if (ClearValue == 0)
            {
                OperationValue = SetValue;
                OperationString = SetString;
                Operation = GeometryModeOperation.Set;
            }
            else if (SetValue == 0)
            {
                OperationValue = ClearValue;
                OperationString = ClearString;
                Operation = GeometryModeOperation.Clear;
            }
            else if (ClearValue == 0xFFFFFF)
            {
                OperationValue = SetValue;
                OperationString = SetString;
                Operation = GeometryModeOperation.Load;
            }
            else
            {
                Operation = GeometryModeOperation.Both;
            }
        }

        public override string ToString()
        {
            if (Operation == GeometryModeOperation.Both)
            {
                return string.Format("gsSPGeometryMode({0}, {1})", ClearString, SetString);
            }
            else
            {
                return string.Format("gsSP{0}GeometryMode({1})", Operation, OperationString);
            }
        }
    }
}
