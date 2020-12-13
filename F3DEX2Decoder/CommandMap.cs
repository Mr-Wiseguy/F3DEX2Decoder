using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F3DEX2Decoder
{
    public enum F3DEX2CommandId
    {
        G_NOOP              = 0x00,
        G_RDPHALF_2         = 0xf1,
        G_SETOTHERMODE_H    = 0xe3,
        G_SETOTHERMODE_L    = 0xe2,
        G_RDPHALF_1         = 0xe1,
        G_SPNOOP            = 0xe0,
        G_ENDDL             = 0xdf,
        G_DL                = 0xde,
        G_LOAD_UCODE        = 0xdd,
        G_MOVEMEM           = 0xdc,
        G_MOVEWORD          = 0xdb,
        G_MTX               = 0xda,
        G_GEOMETRYMODE      = 0xd9,
        G_POPMTX            = 0xd8,
        G_TEXTURE           = 0xd7,
        G_DMA_IO            = 0xd6,
        G_SPECIAL_1         = 0xd5,
        G_SPECIAL_2         = 0xd4,
        G_SPECIAL_3         = 0xd3,
        G_VTX               = 0x01,
        G_MODIFYVTX         = 0x02,
        G_CULLDL            = 0x03,
        G_BRANCH_Z          = 0x04,
        G_TRI1              = 0x05,
        G_TRI2              = 0x06,
        G_QUAD              = 0x07,
        G_LINE3D            = 0x08,

        G_SETCIMG           = 0xff,
        G_SETZIMG           = 0xfe,
        G_SETTIMG           = 0xfd,
        G_SETCOMBINE        = 0xfc,
        G_SETENVCOLOR       = 0xfb,
        G_SETPRIMCOLOR      = 0xfa,
        G_SETBLENDCOLOR     = 0xf9,
        G_SETFOGCOLOR       = 0xf8,
        G_SETFILLCOLOR      = 0xf7,
        G_FILLRECT          = 0xf6,
        G_SETTILE           = 0xf5,
        G_LOADTILE          = 0xf4,
        G_LOADBLOCK         = 0xf3,
        G_SETTILESIZE       = 0xf2,
        G_LOADTLUT          = 0xf0,
        G_RDPSETOTHERMODE   = 0xef,
        G_SETPRIMDEPTH      = 0xee,
        G_SETSCISSOR        = 0xed,
        G_SETCONVERT        = 0xec,
        G_SETKEYR           = 0xeb,
        G_SETKEYGB          = 0xea,
        G_RDPFULLSYNC       = 0xe9,
        G_RDPTILESYNC       = 0xe8,
        G_RDPPIPESYNC       = 0xe7,
        G_RDPLOADSYNC       = 0xe6,
        G_TEXRECTFLIP       = 0xe5,
        G_TEXRECT           = 0xe4,
    }

    public enum RDPImgFormat
    {
        G_IM_FMT_RGBA   = 0,
        G_IM_FMT_YUV    = 1,
        G_IM_FMT_CI     = 2,
        G_IM_FMT_IA     = 3,
        G_IM_FMT_I      = 4,
    }

    public enum RDPImgSize
    {
        G_IM_SIZ_4b      = 0,
        G_IM_SIZ_8b      = 1,
        G_IM_SIZ_16b     = 2,
        G_IM_SIZ_32b     = 3,
        G_IM_SIZ_DD      = 5,
    }

    public enum RDPMirror
    {
        G_TX_NOMIRROR   = 0,
        G_TX_MIRROR     = 0x1,
    }

    public enum RDPWrapClamp
    {
        G_TX_WRAP = 0,
        G_TX_CLAMP = 0x2,
    }

    public enum ConvertCoeff
    {
        G_CV_K0 = 175,
        G_CV_K1 = -43,
        G_CV_K2 = -89,
        G_CV_K3 = 222,
        G_CV_K4 = 114,
        G_CV_K5 = 42,
    }

    public enum ScissorMode
    {
        G_SC_NON_INTERLACE  = 0,
        G_SC_ODD_INTERLACE  = 3,
        G_SC_EVEN_INTERLACE = 2,
    }

    public enum OthermodeHShift
    {
        G_MDSFT_BLENDMASK   = 0,
        G_MDSFT_ALPHADITHER = 4,
        G_MDSFT_RGBDITHER   = 6,
        G_MDSFT_COMBKEY     = 8,
        G_MDSFT_TEXTCONV    = 9,
        G_MDSFT_TEXTFILT    = 12,
        G_MDSFT_TEXTLUT     = 14,
        G_MDSFT_TEXTLOD     = 16,
        G_MDSFT_TEXTDETAIL  = 17,
        G_MDSFT_TEXTPERSP   = 19,
        G_MDSFT_CYCLETYPE   = 20,
        G_MDSFT_COLORDITHER = 22,
        G_MDSFT_PIPELINE    = 23,
    }

    public enum PipelineMode
    {
        G_PM_1PRIMITIVE = (1 << OthermodeHShift.G_MDSFT_PIPELINE),
        G_PM_NPRIMITIVE = (0 << OthermodeHShift.G_MDSFT_PIPELINE),
        Mask = G_PM_1PRIMITIVE,
    }

    public enum CycleType
    {
        G_CYC_1CYCLE    = (0 << OthermodeHShift.G_MDSFT_CYCLETYPE),
        G_CYC_2CYCLE    = (1 << OthermodeHShift.G_MDSFT_CYCLETYPE),
        G_CYC_COPY      = (2 << OthermodeHShift.G_MDSFT_CYCLETYPE),
        G_CYC_FILL      = (3 << OthermodeHShift.G_MDSFT_CYCLETYPE),
        Mask = G_CYC_2CYCLE | G_CYC_COPY | G_CYC_FILL,
    }

    public enum TexturePersp
    {
        G_TP_NONE   = (0 << OthermodeHShift.G_MDSFT_TEXTPERSP),
        G_TP_PERSP  = (1 << OthermodeHShift.G_MDSFT_TEXTPERSP),
        Mask = G_TP_PERSP,
    }

    public enum TextureDetail
    {
        G_TD_CLAMP      = (0 << OthermodeHShift.G_MDSFT_TEXTDETAIL),
        G_TD_SHARPEN    = (1 << OthermodeHShift.G_MDSFT_TEXTDETAIL),
        G_TD_DETAIL     = (2 << OthermodeHShift.G_MDSFT_TEXTDETAIL),
        Mask = G_TD_SHARPEN | G_TD_DETAIL,
    }

    public enum TextureLOD
    {
        G_TL_TILE   = (0 << OthermodeHShift.G_MDSFT_TEXTLOD),
        G_TL_LOD    = (1 << OthermodeHShift.G_MDSFT_TEXTLOD),
        Mask = G_TL_LOD,
    }

    public enum TextureLUT
    {
        G_TT_NONE   = (0 << OthermodeHShift.G_MDSFT_TEXTLUT),
        G_TT_RGBA16 = (2 << OthermodeHShift.G_MDSFT_TEXTLUT),
        G_TT_IA16   = (3 << OthermodeHShift.G_MDSFT_TEXTLUT),
        Mask = G_TT_RGBA16 | G_TT_IA16,
    }

    public enum TextureFilter
    {
        G_TF_POINT      = (0 << OthermodeHShift.G_MDSFT_TEXTFILT),
        G_TF_AVERAGE    = (3 << OthermodeHShift.G_MDSFT_TEXTFILT),
        G_TF_BILERP     = (2 << OthermodeHShift.G_MDSFT_TEXTFILT),
        Mask = G_TF_AVERAGE | G_TF_BILERP,
    }

    public enum TextureConvert
    {
        G_TC_CONV       = (0 << OthermodeHShift.G_MDSFT_TEXTCONV),
        G_TC_FILTCONV   = (5 << OthermodeHShift.G_MDSFT_TEXTCONV),
        G_TC_FILT       = (6 << OthermodeHShift.G_MDSFT_TEXTCONV),
        Mask = G_TC_FILTCONV | G_TC_FILT,
    }

    public enum CombineKey
    {
        G_CK_NONE   = (0 << OthermodeHShift.G_MDSFT_COMBKEY),
        G_CK_KEY    = (1 << OthermodeHShift.G_MDSFT_COMBKEY),
        Mask = G_CK_KEY,
    }

    public enum ColorDither
    {
        G_CD_MAGICSQ        = (0 << OthermodeHShift.G_MDSFT_RGBDITHER),
        G_CD_BAYER          = (1 << OthermodeHShift.G_MDSFT_RGBDITHER),
        G_CD_NOISE          = (2 << OthermodeHShift.G_MDSFT_RGBDITHER),
        G_CD_DISABLE        = (3 << OthermodeHShift.G_MDSFT_RGBDITHER),
        G_CD_ENABLE         = G_CD_NOISE,
        Mask = G_CD_BAYER | G_CD_NOISE | G_CD_DISABLE | G_CD_ENABLE,
    }

    public enum AlphaDither
    {
        G_AD_PATTERN        = (0 << OthermodeHShift.G_MDSFT_ALPHADITHER),
        G_AD_NOTPATTERN     = (1 << OthermodeHShift.G_MDSFT_ALPHADITHER),
        G_AD_NOISE          = (2 << OthermodeHShift.G_MDSFT_ALPHADITHER),
        G_AD_DISABLE        = (3 << OthermodeHShift.G_MDSFT_ALPHADITHER),
        Mask = G_AD_NOTPATTERN | G_AD_NOISE | G_AD_DISABLE,
    }

    public enum OthermodeLShift
    {
        G_MDSFT_ALPHACOMPARE        = 0,
        G_MDSFT_ZSRCSEL             = 2,
        G_MDSFT_RENDERMODE          = 3,
        G_MDSFT_BLENDER             = 16,
    }

    public enum AlphaCompare
    {
        G_AC_NONE           = (0 << OthermodeLShift.G_MDSFT_ALPHACOMPARE),
        G_AC_THRESHOLD      = (1 << OthermodeLShift.G_MDSFT_ALPHACOMPARE),
        G_AC_DITHER         = (3 << OthermodeLShift.G_MDSFT_ALPHACOMPARE),
        Mask = G_AC_THRESHOLD | G_AC_DITHER,
    }

    public enum DepthSource
    {
        G_ZS_PIXEL      = (0 << OthermodeLShift.G_MDSFT_ZSRCSEL),
        G_ZS_PRIM       = (1 << OthermodeLShift.G_MDSFT_ZSRCSEL),
        Mask = G_ZS_PRIM,
    }

    public class F3DEX2Command
    {
        public F3DEX2CommandId CommandId { get; private set; }
        public ulong Words { get; private set; }

        public F3DEX2Command(ulong words)
        {
            CommandId = (F3DEX2CommandId)(words >> (32 + 24));
            Words = words;
        }

        public string ParseString(List<F3DEX2Command> nextCmds, out int cmdsSkipped, ref int totalTris, ref int totalVertices, string indentation)
        {
            cmdsSkipped = 0;
            switch (CommandId)
            {
                case F3DEX2CommandId.G_NOOP:
                    {
                        if ((Words & 0xFFFFFF) == 0)
                        {
                            return "gsDPNoOp()";
                        }
                        else
                        {
                            return string.Format("gsDPNoOpTag({0})", Words & 0xFFFFFF);
                        }
                    }
                case F3DEX2CommandId.G_RDPHALF_2: break;
                case F3DEX2CommandId.G_SETOTHERMODE_H:
                    {
                        F3DEX2Decoder.ParseOtherMode(this,
                            out int sft, out int len, out ulong data);

                        OthermodeHShift sftType = (OthermodeHShift)sft;

                        switch (sftType)
                        {
                            case OthermodeHShift.G_MDSFT_ALPHADITHER:
                                if (len == 2)
                                    return string.Format("gsDPSetAlphaDither({0})", (AlphaDither)data);
                                break;
                            case OthermodeHShift.G_MDSFT_RGBDITHER:
                                if (len == 2)
                                    return string.Format("gsDPSetColorDither({0})", (ColorDither)data);
                                break;
                            case OthermodeHShift.G_MDSFT_COMBKEY:
                                if (len == 1)
                                    return string.Format("gsDPSetCombineKey({0})", (CombineKey)data);
                                break;
                            case OthermodeHShift.G_MDSFT_TEXTCONV:
                                if (len == 3)
                                    return string.Format("gsDPSetTextureConvert({0})", (TextureConvert)data);
                                break;
                            case OthermodeHShift.G_MDSFT_TEXTFILT:
                                if (len == 2)
                                    return string.Format("gsDPSetTextureFilter({0})", (TextureFilter)data);
                                break;
                            case OthermodeHShift.G_MDSFT_TEXTLUT:
                                if (len == 2)
                                    return string.Format("gsDPSetTextureLUT({0})", (TextureLUT)data);
                                break;
                            case OthermodeHShift.G_MDSFT_TEXTLOD:
                                if (len == 1)
                                    return string.Format("gsDPSetTextureLOD({0})", (TextureLOD)data);
                                break;
                            case OthermodeHShift.G_MDSFT_TEXTDETAIL:
                                if (len == 2)
                                    return string.Format("gsDPSetTextureDetail({0})", (TextureDetail)data);
                                break;
                            case OthermodeHShift.G_MDSFT_TEXTPERSP:
                                if (len == 1)
                                    return string.Format("gsDPSetTexturePersp({0})", (TexturePersp)data);
                                break;
                            case OthermodeHShift.G_MDSFT_CYCLETYPE:
                                if (len == 2)
                                    return string.Format("gsDPSetCycleType({0})", (CycleType)data);
                                break;
                            case OthermodeHShift.G_MDSFT_PIPELINE:
                                if (len == 1)
                                    return string.Format("gsDPPipelineMode({0})", (PipelineMode)data);
                                break;
                        }
                        break;
                    }
                case F3DEX2CommandId.G_SETOTHERMODE_L:
                    {
                        F3DEX2Decoder.ParseOtherMode(this,
                            out int sft, out int len, out ulong data);

                        OthermodeLShift sftType = (OthermodeLShift)sft;

                        switch (sftType)
                        {
                            case OthermodeLShift.G_MDSFT_ALPHACOMPARE:
                                if (len == 2)
                                    return string.Format("gsDPSetAlphaCompare({0})", (AlphaCompare)data);
                                break;
                            case OthermodeLShift.G_MDSFT_ZSRCSEL:
                                if (len == 1)
                                    return string.Format("gsDPSetDepthSource({0})", (DepthSource)data);
                                break;
                            case OthermodeLShift.G_MDSFT_RENDERMODE:
                                if (len == 29)
                                {
                                    RenderMode rm = new RenderMode(Words);
                                    return string.Format("gsDPSetRenderMode({0})", rm);
                                }
                                break;
                            case OthermodeLShift.G_MDSFT_BLENDER:
                                break;
                        }

                        break;
                    }
                case F3DEX2CommandId.G_RDPHALF_1:
                    {
                        ulong rdpHalfData = Words & 0xFFFFFFFF;

                        if (nextCmds.Count >= 1)
                        {
                            // gsSPBranchLessZrg/raw
                            if (nextCmds[0].CommandId == F3DEX2CommandId.G_BRANCH_Z)
                            {
                                F3DEX2Decoder.ParseBranchLessZ(nextCmds[0],
                                    out int vtx, out int zval);

                                cmdsSkipped = 1;

                                return string.Format("gsSPBranchLessZraw(0x{0:X8}, {1}, {2})", rdpHalfData, vtx, zval);
                            }
                            // gsSPLoadUCodeEx
                            else if (nextCmds[0].CommandId == F3DEX2CommandId.G_LOAD_UCODE)
                            {
                                F3DEX2Decoder.ParseLoadUcode(nextCmds[0],
                                    out ulong uc_start, out int uc_dsize);

                                cmdsSkipped = 1;

                                return string.Format("gsSPLoadUcodeEx(0x{0:X8}, 0x{1:X8}, 0x{2:X8})", uc_start, rdpHalfData, uc_dsize);
                            }
                        }

                        return string.Format("gImmp1(pkt, G_RDPHALF_1, {0})", rdpHalfData);
                    }
                case F3DEX2CommandId.G_SPNOOP:
                    return "gsSPNoOp()";
                case F3DEX2CommandId.G_ENDDL:
                    return "gsSPEndDisplayList()";
                case F3DEX2CommandId.G_DL:
                    if ((Words >> (32 + 16) & 0xFF) == 0x01)
                    {
                        return string.Format("gsSPBranchList(0x{0:X8})", Words & 0xFFFFFFFF);
                    }
                    else
                    {
                        return string.Format("gsSPDisplayList(0x{0:X8})", Words & 0xFFFFFFFF);
                    }
                case F3DEX2CommandId.G_LOAD_UCODE: break;
                case F3DEX2CommandId.G_MOVEMEM:
                    {
                        MoveMem mm = new MoveMem(Words);
                        return mm.ToString();
                    }
                case F3DEX2CommandId.G_MOVEWORD:
                    {
                        MoveWord mw = new MoveWord(Words);
                        return mw.ToString(nextCmds, out cmdsSkipped);
                    }
                case F3DEX2CommandId.G_MTX:
                    {
                        F3DEX2Decoder.ParseMatrix(this,
                            out MatrixType mType, out MatrixLoadType loadType, out MatrixPushType pushType, out ulong mAddr,
                            out int size, out int offset);

                        if (size == 64 && offset == 0) // TODO remove magic number
                        {
                            return string.Format("gsSPMatrix(0x{0:X8}, {1} | {2} | {3})", mAddr, mType, loadType, pushType);
                        }
                        else
                        {
                            return string.Format("gsDma2p(G_MTX, {0:X8}, {1}, {2} | {3} | {4}, {5}", mAddr, size, mType, loadType, pushType, offset);
                        }
                    }
                case F3DEX2CommandId.G_GEOMETRYMODE:
                    {
                        GeometryMode gm = new GeometryMode(Words);
                        return gm.ToString();
                    }
                case F3DEX2CommandId.G_POPMTX:
                    {
                        F3DEX2Decoder.ParsePopMtx(this,
                            out int bytes, out int idx);

                        if (idx == 2)
                        {
                            if (bytes == 64)
                            {
                                return "gsSPPopMatrix(G_MTX_MODELVIEW)";
                            }
                            else if (bytes % 64 == 0)
                            {
                                return string.Format("gsSPPopMatrixN(G_MTX_MODELVIEW, {0})", bytes / 64);
                            }
                        }

                        break;
                    }
                case F3DEX2CommandId.G_TEXTURE:
                    {
                        F3DEX2Decoder.ParseTexture(this,
                            out int s, out int t, out int level, out int tile, out int on);

                        string tileString;

                        if (tile == 7)
                        {
                            tileString = "G_TX_LOADTILE";
                        }
                        else if (tile == 0)
                        {
                            tileString = "G_TX_RENDERTILE";
                        }
                        else
                        {
                            tileString = tile.ToString();
                        }

                        string onString;

                        if (on == 1)
                        {
                            onString = "G_ON";
                        }
                        else if (on == 0)
                        {
                            onString = "G_OFF";
                        }
                        else
                        {
                            onString = on.ToString();
                        }

                        return string.Format("gsSPTexture(0x{0:X4}, 0x{1:X4}, {2}, {3}, {4})", s, t, level, tileString, onString);
                    }
                case F3DEX2CommandId.G_DMA_IO: break;
                case F3DEX2CommandId.G_SPECIAL_1:
                case F3DEX2CommandId.G_SPECIAL_2:
                case F3DEX2CommandId.G_SPECIAL_3:
                    {
                        // TODO This will not work in the general case, and is tailored to F3DEX2 2.04H, which is the only ucode I've seen
                        // that uses any G_SPECIAL commands (G_SPECIAL_1 in that case).
                        return string.Format("gsImmp1({0}, {1})", CommandId, Words & 0xFFFFFFFF);
                    }

                case F3DEX2CommandId.G_VTX:
                    {
                        F3DEX2Decoder.ParseVtx(this,
                            out int vtxCount, out int vtxOffset, out ulong vtxAddr);

                        totalVertices += vtxCount;

                        return string.Format("gsSPVertex(0x{0:X8}, {1}, {2})", vtxAddr, vtxCount, vtxOffset);
                    }
                case F3DEX2CommandId.G_MODIFYVTX: break;
                case F3DEX2CommandId.G_CULLDL:
                    {
                        F3DEX2Decoder.ParseCullDL(this,
                            out int vstart, out int vend);
                        return string.Format("gsSPCullDisplayList({0}, {1})", vstart, vend);
                    }
                case F3DEX2CommandId.G_BRANCH_Z: break;
                case F3DEX2CommandId.G_TRI1:
                    {
                        F3DEX2Decoder.ParseTri(this, true,
                            out int a, out int b, out int c);

                        totalTris++;

                        return string.Format("gsSP1Triangle({0}, {1}, {2}, 0)", a, b, c);
                    }
                case F3DEX2CommandId.G_TRI2:
                    {
                        F3DEX2Decoder.ParseTri(this, true,
                            out int a1, out int b1, out int c1);
                        F3DEX2Decoder.ParseTri(this, false,
                            out int a2, out int b2, out int c2);

                        totalTris += 2;

                        return string.Format("gsSP2Triangles({0}, {1}, {2}, 0, {3}, {4}, {5}, 0)", a1, b1, c1, a2, b2, c2);
                    }
                case F3DEX2CommandId.G_QUAD: // TODO test this
                    {
                        F3DEX2Decoder.ParseTri(this, true,
                            out int a1, out int b1, out int c1);
                        F3DEX2Decoder.ParseTri(this, false,
                            out int _, out int _, out int c2);

                        return string.Format("gsSP1Quadrangle({0}, {1}, {2}, {3}, 0)", a1, b1, c1, c2);
                    }
                case F3DEX2CommandId.G_LINE3D: break;
                case F3DEX2CommandId.G_SETCIMG:
                    {
                        F3DEX2Decoder.ParseSetImage(this,
                            out int fmt, out int siz, out int width, out ulong i);

                        return string.Format("gsDPSetColorImage({0}, {1}, {2}, 0x{3:X8})",
                            (RDPImgFormat)fmt, (RDPImgSize)siz,
                            width, i);
                    }
                case F3DEX2CommandId.G_SETZIMG:
                    {
                        F3DEX2Decoder.ParseSetImage(this,
                            out int fmt, out int siz, out int width, out ulong i);

                        if (fmt == 0 && siz == 0 && width == 1)
                        {
                            return string.Format("gsDPSetDepthImage(0x{0:X8})", i);
                        }
                        else
                        {
                            return string.Format("gsSetImage(G_SETZIMG, {0}, {1}, {2}, 0x{3:X8})", fmt, siz, width, i);
                        }
                    }
                case F3DEX2CommandId.G_SETTIMG:
                    {
                        F3DEX2Decoder.ParseSetImage(this,
                            out int fmt, out int siz, out int width, out ulong i);

                        return string.Format("gsDPSetTextureImage({0}, {1}, {2}, 0x{3:X8})",
                            (RDPImgFormat)fmt, (RDPImgSize)siz,
                            width, i);
                    }
                case F3DEX2CommandId.G_SETCOMBINE:
                    {
                        F3DEX2Decoder.ParseSetCombine(this,
                            out ulong ulongMode);

                        CombineMode mode = new CombineMode(ulongMode);

                        string modeStr = mode.ToString();

                        if (modeStr.StartsWith("G_CC"))
                        {
                            return string.Format("gsDPSetCombine({0})", modeStr);
                        }
                        else
                        {
                            return string.Format("gsDPSetCombineLerp({0})", modeStr);
                        }
                    }
                case F3DEX2CommandId.G_SETENVCOLOR:
                    {
                        F3DEX2Decoder.ParseSetColor(this,
                            out ulong col);
                        F3DEX2Decoder.GetComponents(col,
                            out int r, out int g, out int b, out int a);

                        return string.Format("gsDPSetEnvColor(0x{0:X2}, 0x{1:X2}, 0x{2:X2}, 0x{3:X2})", r, g, b, a);
                    }
                case F3DEX2CommandId.G_SETPRIMCOLOR:
                    {
                        F3DEX2Decoder.ParseSetColor(this,
                            out ulong col);
                        F3DEX2Decoder.GetComponents(col,
                            out int r, out int g, out int b, out int a);

                        int m = (int)(Words >> (32 + 8)) & 0xFF;
                        int l = (int)(Words >> (32 + 0)) & 0xFF;

                        return string.Format("gsDPSetPrimColor(0x{0:X2}, 0x{1:X2}, 0x{2:X2}, 0x{3:X2}, 0x{4:X2}, 0x{5:X2})", m, l, r, g, b, a);
                    }
                case F3DEX2CommandId.G_SETBLENDCOLOR:
                    {
                        F3DEX2Decoder.ParseSetColor(this,
                            out ulong col);
                        F3DEX2Decoder.GetComponents(col,
                            out int r, out int g, out int b, out int a);

                        return string.Format("gsDPSetBlendColor(0x{0:X2}, 0x{1:X2}, 0x{2:X2}, 0x{3:X2})", r, g, b, a);
                    }
                case F3DEX2CommandId.G_SETFOGCOLOR:
                    {
                        F3DEX2Decoder.ParseSetColor(this,
                            out ulong col);
                        F3DEX2Decoder.GetComponents(col,
                            out int r, out int g, out int b, out int a);

                        return string.Format("gsDPSetFogColor(0x{0:X2}, 0x{1:X2}, 0x{2:X2}, 0x{3:X2})", r, g, b, a);
                    }
                case F3DEX2CommandId.G_SETFILLCOLOR:
                    {
                        F3DEX2Decoder.ParseSetColor(this,
                            out ulong col);

                        return string.Format("gsDPSetFillColor({0})", F3DEX2Decoder.PackedColorWordToStr(col));
                    }
                case F3DEX2CommandId.G_FILLRECT:
                    {
                        F3DEX2Decoder.ParseRect(this,
                            out int xl, out int yl, out int xh, out int yh, out int _);

                        return string.Format("gsDPFillRectangle({0}, {1}, {2}, {3})",
                            xl >> 2, yl >> 2, (xh >> 2), (yh >> 2));
                    }
                case F3DEX2CommandId.G_SETTILE:
                    {
                        F3DEX2Decoder.ParseSetTile(this,
                            out int fmt, out int siz, out int line, out int tmem, out int tile, out int palette, out int cmt,
                            out int maskt, out int shiftt, out int cms, out int masks, out int shifts);
                        RDPMirror mirrorT = (RDPMirror)(cmt & 0x1);
                        RDPWrapClamp clampT = (RDPWrapClamp)(cmt & 0x2);
                        RDPMirror mirrorS = (RDPMirror)(cms & 0x1);
                        RDPWrapClamp clampS = (RDPWrapClamp)(cms & 0x2);

                        return string.Format("gsDPSetTile({0}, {1}, {2}, {3}, {4}, {5}, {6}|{7}, {8}, {9}, {10}|{11}, {12}, {13})",
                            (RDPImgFormat)fmt, (RDPImgSize)siz,
                            line, tmem, tile, palette,
                            mirrorT, clampT,
                            maskt, shiftt,
                            mirrorS, clampS,
                            masks, shifts);
                    }
                case F3DEX2CommandId.G_LOADTILE:
                    {
                        F3DEX2Decoder.ParseLoadTileGeneric(this,
                            out int tile, out int uls, out int ult, out int lrs, out int lrt);

                        string ulsStr = F3DEX2Decoder.TexCoord102ToStr(uls);
                        string ultStr = F3DEX2Decoder.TexCoord102ToStr(ult);
                        string lrsStr = F3DEX2Decoder.TexCoord102ToStr(lrs, true);
                        string lrtStr = F3DEX2Decoder.TexCoord102ToStr(lrt, true);

                        return string.Format("gsDPLoadTile({0}, {1}, {2}, {3}, {4})",
                            tile, ulsStr, ultStr, lrsStr, lrtStr); // TODO CI4 textures are loaded as 8b, so this may not produce a clean output
                    }
                case F3DEX2CommandId.G_LOADBLOCK:
                    {
                        F3DEX2Decoder.ParseLoadTileGeneric(this,
                            out int tile, out int uls, out int ult, out int lrs, out int dxt);

                        return string.Format("gsDPLoadBlock({0}, 0x{1:X3}, 0x{2:X3}, 0x{3:X3}, 0x{4:X3})",
                            tile, uls, ult, lrs, dxt);
                    }
                case F3DEX2CommandId.G_SETTILESIZE:
                    {
                        F3DEX2Decoder.ParseLoadTileGeneric(this,
                            out int tile, out int uls, out int ult, out int lrs, out int lrt);

                        string ulsStr = F3DEX2Decoder.TexCoord102ToStr(uls);
                        string ultStr = F3DEX2Decoder.TexCoord102ToStr(ult);
                        string lrsStr = F3DEX2Decoder.TexCoord102ToStr(lrs, true);
                        string lrtStr = F3DEX2Decoder.TexCoord102ToStr(lrt, true);

                        return string.Format("gsDPSetTileSize({0}, {1}, {2}, {3}, {4})",
                            tile, ulsStr, ultStr, lrsStr, lrtStr);
                    }
                case F3DEX2CommandId.G_LOADTLUT:
                    {
                        F3DEX2Decoder.ParseLoadTLUT(this,
                            out int tile, out int count);
                        return string.Format("gsDPLoadTLUTCmd({0}, {1})", tile, count);
                    }
                case F3DEX2CommandId.G_RDPSETOTHERMODE:
                    {
                        F3DEX2Decoder.ParseRDPSetOtherMode(this,
                            out PipelineMode pm, out CycleType cyc, out TexturePersp tp, out TextureDetail td, out TextureLOD tl,
                            out TextureLUT tt, out TextureFilter tf, out TextureConvert tc, out CombineKey ck, out ColorDither cd,
                            out AlphaDither ad, out AlphaCompare ac, out DepthSource zs, out RenderMode rm);

                        return string.Format("gsDPSetOtherMode(\n" +
                            indentation + "{0} | {1} | {2} | {3} | {4} | {5}\n" +
                            indentation + "{6} | {7} | {8} | {9} | {10},\n" +
                            indentation + "{11} | {12} | {13})",
                            pm, cyc, tp, td, tl, tt, tf, tc, ck, cd, ad,
                            ac, zs, rm.ToString().Replace(",", " |"));
                    }
                case F3DEX2CommandId.G_SETPRIMDEPTH: break;
                case F3DEX2CommandId.G_SETSCISSOR:
                    {
                        F3DEX2Decoder.ParseScissor(this,
                            out int mode, out int ulx, out int uly, out int lrx, out int lry);

                        return string.Format("gsDPSetScissor({0}, {1:F2}f, {2:F2}f, {3:F2}f, {4:F2}f)",
                            (ScissorMode)mode, ulx / 4.0f, uly / 4.0f, lrx / 4.0f, lry / 4.0f);
                    }
                case F3DEX2CommandId.G_SETCONVERT:
                    {
                        F3DEX2Decoder.ParseConvert(this,
                            out int[] coeffs);

                        string k0Str = (coeffs[0] == (short)ConvertCoeff.G_CV_K0) ? "G_CV_K0" : coeffs[0].ToString();
                        string k1Str = (coeffs[1] == (short)ConvertCoeff.G_CV_K1) ? "G_CV_K1" : coeffs[1].ToString();
                        string k2Str = (coeffs[2] == (short)ConvertCoeff.G_CV_K2) ? "G_CV_K2" : coeffs[2].ToString();
                        string k3Str = (coeffs[3] == (short)ConvertCoeff.G_CV_K3) ? "G_CV_K3" : coeffs[3].ToString();
                        string k4Str = (coeffs[4] == (short)ConvertCoeff.G_CV_K4) ? "G_CV_K4" : coeffs[4].ToString();
                        string k5Str = (coeffs[5] == (short)ConvertCoeff.G_CV_K5) ? "G_CV_K5" : coeffs[5].ToString();

                        return string.Format("gsDPSetConvert({0}, {1}, {2}, {3}, {4}, {5})",
                            k0Str, k1Str, k2Str, k3Str, k4Str, k5Str);
                    }
                case F3DEX2CommandId.G_SETKEYR: break;
                case F3DEX2CommandId.G_SETKEYGB: break;
                case F3DEX2CommandId.G_RDPFULLSYNC:
                    return "gsDPFullSync()";
                case F3DEX2CommandId.G_RDPTILESYNC:
                    return "gsDPTileSync()";
                case F3DEX2CommandId.G_RDPPIPESYNC:
                    return "gsDPPipeSync()";
                case F3DEX2CommandId.G_RDPLOADSYNC:
                    return "gsDPLoadSync()";
                case F3DEX2CommandId.G_TEXRECTFLIP: // Fall through
                case F3DEX2CommandId.G_TEXRECT:
                    {
                        F3DEX2Decoder.ParseRect(this,
                            out int xl, out int yl, out int xh, out int yh, out int tile);

                        string xlStr = F3DEX2Decoder.TexCoord102ToStr(xl);
                        string ylStr = F3DEX2Decoder.TexCoord102ToStr(yl);

                        string xhStr = F3DEX2Decoder.TexCoord102OffsetToStr(xl, xh);
                        string yhStr = F3DEX2Decoder.TexCoord102OffsetToStr(yl, yh);

                        F3DEX2Command nextCmd0 = nextCmds[0];
                        F3DEX2Command nextCmd1 = nextCmds[1];

                        if (nextCmd0.CommandId == F3DEX2CommandId.G_RDPHALF_1 && nextCmd1.CommandId == F3DEX2CommandId.G_RDPHALF_2)
                        {
                            F3DEX2Decoder.ParseRDPHalf_HH(nextCmd0, out int s, out int t);
                            F3DEX2Decoder.ParseRDPHalf_HH(nextCmd1, out int dsdx, out int dtdy);

                            cmdsSkipped = 2;

                            if (CommandId == F3DEX2CommandId.G_TEXRECT)
                            {
                                return string.Format("gsSPTextureRectangle({0}, {1}, {2}, {3}, {4}, 0x{5:X4}, 0x{6:X4}, 0x{7:X4}, 0x{8:X4})",
                                    xlStr, ylStr, xhStr, yhStr, tile, s, t, dsdx, dtdy);
                            }
                            else
                            {
                                return string.Format("gsSPTextureRectangleFlip({0}, {1}, {2}, {3}, {4}, 0x{5:X4}, 0x{6:X4}, 0x{7:X4}, 0x{8:X4})",
                                    xlStr, ylStr, xhStr, yhStr, tile, s, t, dsdx, dtdy);
                            }
                        }
                        break;
                    }
            }
            return string.Format("{0:X8},{1:X8}", Words >> 32, Words & 0xFFFFFFFF);
        }

        public override string ToString()
        {
            return string.Format("0x{0:X8},0x{1:X8}", Words >> 32, Words & 0xFFFFFFFF);
        }
    }
}
