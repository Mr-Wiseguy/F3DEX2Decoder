using System;
using System.Collections.Generic;
using System.Text;

namespace F3DEX2Decoder
{
    public enum MoveWordIndex
    {
        G_MW_MATRIX = 0x00,
        G_MW_NUMLIGHT = 0x02,
        G_MW_CLIP = 0x04,
        G_MW_SEGMENT = 0x06,
        G_MW_FOG = 0x08,
        G_MW_LIGHTCOL = 0x0a,
        G_MW_FORCEMTX = 0x0c,
        G_MW_PERSPNORM = 0x0e,
    }
    public enum MoveWordOffsetNumlights
    {
        G_MWO_NUMLIGHT = 0x00,
    }
    public enum MoveWordOffsetClip
    {
        G_MWO_CLIP_RNX = 0x04,
        G_MWO_CLIP_RNY = 0x0c,
        G_MWO_CLIP_RPX = 0x14,
        G_MWO_CLIP_RPY = 0x1c,
    }
    public enum MoveWordOffsetSegment
    {
        G_MWO_SEGMENT_0 = 0x00 * 4,
        G_MWO_SEGMENT_1 = 0x01 * 4,
        G_MWO_SEGMENT_2 = 0x02 * 4,
        G_MWO_SEGMENT_3 = 0x03 * 4,
        G_MWO_SEGMENT_4 = 0x04 * 4,
        G_MWO_SEGMENT_5 = 0x05 * 4,
        G_MWO_SEGMENT_6 = 0x06 * 4,
        G_MWO_SEGMENT_7 = 0x07 * 4,
        G_MWO_SEGMENT_8 = 0x08 * 4,
        G_MWO_SEGMENT_9 = 0x09 * 4,
        G_MWO_SEGMENT_A = 0x0a * 4,
        G_MWO_SEGMENT_B = 0x0b * 4,
        G_MWO_SEGMENT_C = 0x0c * 4,
        G_MWO_SEGMENT_D = 0x0d * 4,
        G_MWO_SEGMENT_E = 0x0e * 4,
        G_MWO_SEGMENT_F = 0x0f * 4,
    }
    public enum MoveWordOffsetFog
    {
        G_MWO_FOG = 0x00,
    }
    public enum MoveWordOffsetLightColor
    {
        G_MWO_aLIGHT_1 = 0x00,
        G_MWO_bLIGHT_1 = 0x04,
        G_MWO_aLIGHT_2 = 0x18,
        G_MWO_bLIGHT_2 = 0x1c,
        G_MWO_aLIGHT_3 = 0x30,
        G_MWO_bLIGHT_3 = 0x34,
        G_MWO_aLIGHT_4 = 0x48,
        G_MWO_bLIGHT_4 = 0x4c,
        G_MWO_aLIGHT_5 = 0x60,
        G_MWO_bLIGHT_5 = 0x64,
        G_MWO_aLIGHT_6 = 0x78,
        G_MWO_bLIGHT_6 = 0x7c,
        G_MWO_aLIGHT_7 = 0x90,
        G_MWO_bLIGHT_7 = 0x94,
        G_MWO_aLIGHT_8 = 0xa8,
        G_MWO_bLIGHT_8 = 0xac,
    }
    public enum MoveWordOffsetInsertMatrix
    {
        G_MWO_MATRIX_XX_XY_I = 0x00,
        G_MWO_MATRIX_XZ_XW_I = 0x04,
        G_MWO_MATRIX_YX_YY_I = 0x08,
        G_MWO_MATRIX_YZ_YW_I = 0x0c,
        G_MWO_MATRIX_ZX_ZY_I = 0x10,
        G_MWO_MATRIX_ZZ_ZW_I = 0x14,
        G_MWO_MATRIX_WX_WY_I = 0x18,
        G_MWO_MATRIX_WZ_WW_I = 0x1c,
        G_MWO_MATRIX_XX_XY_F = 0x20,
        G_MWO_MATRIX_XZ_XW_F = 0x24,
        G_MWO_MATRIX_YX_YY_F = 0x28,
        G_MWO_MATRIX_YZ_YW_F = 0x2c,
        G_MWO_MATRIX_ZX_ZY_F = 0x30,
        G_MWO_MATRIX_ZZ_ZW_F = 0x34,
        G_MWO_MATRIX_WX_WY_F = 0x38,
        G_MWO_MATRIX_WZ_WW_F = 0x3c,
        G_MWO_POINT_RGBA = 0x10,
        G_MWO_POINT_ST = 0x14,
        G_MWO_POINT_XYSCREEN = 0x18,
        G_MWO_POINT_ZSCREEN = 0x1c,
    }
    public enum MoveWordOffsetForceMatrix
    {
        ZERO = 0,
    }


    public enum FrustumRatio
    {
        FR_NEG_FRUSTRATIO_1 = 0x00000001,
        FR_POS_FRUSTRATIO_1 = 0x0000ffff,
        FR_NEG_FRUSTRATIO_2 = 0x00000002,
        FR_POS_FRUSTRATIO_2 = 0x0000fffe,
        FR_NEG_FRUSTRATIO_3 = 0x00000003,
        FR_POS_FRUSTRATIO_3 = 0x0000fffd,
        FR_NEG_FRUSTRATIO_4 = 0x00000004,
        FR_POS_FRUSTRATIO_4 = 0x0000fffc,
        FR_NEG_FRUSTRATIO_5 = 0x00000005,
        FR_POS_FRUSTRATIO_5 = 0x0000fffb,
        FR_NEG_FRUSTRATIO_6 = 0x00000006,
        FR_POS_FRUSTRATIO_6 = 0x0000fffa,
    }

    public enum MoveWordOffsetPerspNorm
    {
        ZERO = 0,
    }

    class MoveWord
    {
        public static readonly Dictionary<MoveWordIndex, Type> MoveWordEnums = new Dictionary<MoveWordIndex, Type>()
        {
            { MoveWordIndex.G_MW_MATRIX    , typeof(MoveWordOffsetInsertMatrix) },
            { MoveWordIndex.G_MW_NUMLIGHT  , typeof(MoveWordOffsetNumlights) },
            { MoveWordIndex.G_MW_CLIP      , typeof(MoveWordOffsetClip) },
            { MoveWordIndex.G_MW_SEGMENT   , typeof(MoveWordOffsetSegment) },
            { MoveWordIndex.G_MW_FOG       , typeof(MoveWordOffsetFog) },
            { MoveWordIndex.G_MW_LIGHTCOL  , typeof(MoveWordOffsetLightColor) },
            { MoveWordIndex.G_MW_FORCEMTX  , typeof(MoveWordOffsetForceMatrix) },
            { MoveWordIndex.G_MW_PERSPNORM , typeof(MoveWordOffsetPerspNorm) },
        };

        public readonly MoveWordIndex Index;
        public readonly int Offset;
        public readonly ulong Data;

        public MoveWord(ulong words)
        {
            Index =   (MoveWordIndex)((words >> (32 + 16)) & 0x00FF);
            Offset = (int)((words >> (32 +  0)) & 0xFFFF);
            Data = words & 0xFFFFFFFF;
        }

        public string ToString(List<F3DEX2Command> nextCommands, out int commandsSkipped)
        {
            MoveWord[] nextMoveWords = new MoveWord[nextCommands.Count];
            int numMoveWords;
            commandsSkipped = 0;
            for (numMoveWords = 0; numMoveWords < nextMoveWords.Length; numMoveWords++)
            {
                if (nextCommands[numMoveWords] != null && nextCommands[numMoveWords].CommandId == F3DEX2CommandId.G_MOVEWORD)
                {
                    nextMoveWords[numMoveWords] = new MoveWord(nextCommands[numMoveWords].Words);
                }
                else
                {
                    break;
                }
            }
            switch (Index)
            {
                case MoveWordIndex.G_MW_MATRIX:
                    break; // Not supported in F3DEX2
                case MoveWordIndex.G_MW_NUMLIGHT:
                    return string.Format("gsSPNumLights(NUMLIGHTS_{0})", Data / 24);
                case MoveWordIndex.G_MW_CLIP:
                    if (numMoveWords >= 3)
                    {
                        if (                                                                      Offset == (int)MoveWordOffsetClip.G_MWO_CLIP_RNX &&
                            nextMoveWords[0].Index == MoveWordIndex.G_MW_CLIP && nextMoveWords[0].Offset == (int)MoveWordOffsetClip.G_MWO_CLIP_RNY &&
                            nextMoveWords[1].Index == MoveWordIndex.G_MW_CLIP && nextMoveWords[1].Offset == (int)MoveWordOffsetClip.G_MWO_CLIP_RPX &&
                            nextMoveWords[2].Index == MoveWordIndex.G_MW_CLIP && nextMoveWords[2].Offset == (int)MoveWordOffsetClip.G_MWO_CLIP_RPY)
                        {
                            if (Data >= 1 && Data <= 6 &&                         // Check that it's a valid ratio
                                                 Data == nextMoveWords[0].Data && // Check that the first two are the same negative ratio
                                nextMoveWords[1].Data == nextMoveWords[2].Data && // Check that the second two are the same positive ratio
                                Data + nextMoveWords[1].Data == 0x10000 )         // Check that the first two and second two are in the same ratio pair
                            {
                                commandsSkipped = 3;
                                return string.Format("gsSPClipRatio({0})", ((FrustumRatio)Data).ToString().Substring(7));
                            }
                        }
                    }
                    break;
                case MoveWordIndex.G_MW_SEGMENT:
                    return string.Format("gsSPSegment(0x{0:X2}, 0x{1:X8})", Offset / 4, Data);
                case MoveWordIndex.G_MW_FOG:
                    return string.Format("gsSPFogFactor({0}, {1})", (int)(Data >> 16) & 0xFFFF, (int)Data & 0xFFFF);
                case MoveWordIndex.G_MW_LIGHTCOL: // TODO test this
                    if (numMoveWords >= 1)
                    {
                        if (nextMoveWords[0].Index == MoveWordIndex.G_MW_LIGHTCOL &&        // Check if the next is also G_MW_LIGHTCOL
                            (Offset % 8) == 0 && nextMoveWords[0].Offset == (Offset + 4) && // Check if this is G_MWO_aLIGHT_x and if the next is G_MWO_bLIGHT_x
                            Data == nextMoveWords[0].Data)                                  // Check if the next has the same color
                        {
                            commandsSkipped = 1;
                            return string.Format("gsSPLightColor({0}, 0x{1:X8})", ((MoveWordOffsetLightColor)Offset).ToString().Substring(7), Data);
                        }
                    }
                    break;
                // TODO ForceMtx
                case MoveWordIndex.G_MW_PERSPNORM:
                    if (Offset == 0)
                    {
                        return string.Format("gsSPPerspNormalize(0x{0:X4})", Data);
                    }
                    break;



            }
            return string.Format("gsMoveWd({0}, {1}, 0x{2:X})", Index, Enum.ToObject(MoveWordEnums[Index], Offset), Data);
        }
    }
}
