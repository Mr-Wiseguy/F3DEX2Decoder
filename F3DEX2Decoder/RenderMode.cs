using System;
using System.Collections.Generic;
using System.Text;

namespace F3DEX2Decoder
{
    public enum RenderModeFlags
    {
        G_AC_DITHER = 0x3,
        AA_EN = 0x8,
        Z_CMP = 0x10,
        Z_UPD = 0x20,
        IM_RD = 0x40,
        CLR_ON_CVG = 0x80,
        CVG_DST_CLAMP = 0,
        CVG_DST_WRAP = 0x100,
        CVG_DST_FULL = 0x200,
        CVG_DST_SAVE = 0x300,
        ZMODE_OPA = 0,
        ZMODE_INTER = 0x400,
        ZMODE_XLU = 0x800,
        ZMODE_DEC = 0xc00,
        CVG_X_ALPHA = 0x1000,
        ALPHA_CVG_SEL = 0x2000,
        FORCE_BL = 0x4000,
        TEX_EDGE = 0x0000,
    }
    public enum BlenderColorInput
    {
        G_BL_CLR_IN = 0,
        G_BL_CLR_MEM = 1,
        G_BL_CLR_BL = 2,
        G_BL_CLR_FOG = 3,
    }

    public enum BlenderAlphaInput
    {
        G_BL_1MA = 0,
        G_BL_A_MEM = 1,
        G_BL_A_IN = 0,
        G_BL_A_FOG = 1,
        G_BL_A_SHADE = 2,
        G_BL_1 = 2,
        G_BL_0 = 3,
    }

    public class HalfMode
    {
        public static readonly Dictionary<HalfMode, string> BASE_MODE_NAMES = new Dictionary<HalfMode, string>()
        {
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.Z_CMP | RenderModeFlags.Z_UPD | RenderModeFlags.CVG_DST_CLAMP | RenderModeFlags.ZMODE_OPA | RenderModeFlags.ALPHA_CVG_SEL,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_A_MEM),
                "RM_RA_ZB_OPA_SURF" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.Z_CMP | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_WRAP | RenderModeFlags.CLR_ON_CVG | RenderModeFlags.FORCE_BL | RenderModeFlags.ZMODE_XLU,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_AA_ZB_XLU_SURF" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.Z_CMP | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_WRAP | RenderModeFlags.ALPHA_CVG_SEL | RenderModeFlags.ZMODE_DEC,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_A_MEM),
                "RM_AA_ZB_OPA_DECAL" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.Z_CMP | RenderModeFlags.CVG_DST_WRAP | RenderModeFlags.ALPHA_CVG_SEL | RenderModeFlags.ZMODE_DEC,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_A_MEM),
                "RM_RA_ZB_OPA_DECAL" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.Z_CMP | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_WRAP | RenderModeFlags.CLR_ON_CVG | RenderModeFlags.FORCE_BL | RenderModeFlags.ZMODE_DEC,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_AA_ZB_XLU_DECAL" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.Z_CMP | RenderModeFlags.Z_UPD | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_CLAMP | RenderModeFlags.ALPHA_CVG_SEL | RenderModeFlags.ZMODE_INTER,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_A_MEM),
                "RM_AA_ZB_OPA_INTER" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.Z_CMP | RenderModeFlags.Z_UPD | RenderModeFlags.CVG_DST_CLAMP | RenderModeFlags.ALPHA_CVG_SEL | RenderModeFlags.ZMODE_INTER,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_A_MEM),
                "RM_RA_ZB_OPA_INTER" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.Z_CMP | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_WRAP | RenderModeFlags.CLR_ON_CVG | RenderModeFlags.FORCE_BL | RenderModeFlags.ZMODE_INTER,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_AA_ZB_XLU_INTER" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.Z_CMP | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_CLAMP | RenderModeFlags.CVG_X_ALPHA | RenderModeFlags.ALPHA_CVG_SEL | RenderModeFlags.FORCE_BL | RenderModeFlags.ZMODE_XLU,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_AA_ZB_XLU_LINE" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.Z_CMP | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_SAVE | RenderModeFlags.CVG_X_ALPHA | RenderModeFlags.ALPHA_CVG_SEL | RenderModeFlags.FORCE_BL | RenderModeFlags.ZMODE_DEC,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_AA_ZB_DEC_LINE" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.Z_CMP | RenderModeFlags.Z_UPD | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_CLAMP | RenderModeFlags.CVG_X_ALPHA | RenderModeFlags.ALPHA_CVG_SEL | RenderModeFlags.ZMODE_OPA | RenderModeFlags.TEX_EDGE,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_A_MEM),
                "RM_AA_ZB_TEX_EDGE" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.Z_CMP | RenderModeFlags.Z_UPD | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_CLAMP | RenderModeFlags.CVG_X_ALPHA | RenderModeFlags.ALPHA_CVG_SEL | RenderModeFlags.ZMODE_INTER | RenderModeFlags.TEX_EDGE,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_A_MEM),
                "RM_AA_ZB_TEX_INTER" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.Z_CMP | RenderModeFlags.Z_UPD | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_FULL | RenderModeFlags.ZMODE_OPA | RenderModeFlags.ALPHA_CVG_SEL,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_A_MEM),
                "RM_AA_ZB_SUB_SURF" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.Z_CMP | RenderModeFlags.Z_UPD | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_CLAMP | RenderModeFlags.ZMODE_OPA | RenderModeFlags.G_AC_DITHER,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_AA_ZB_PCL_SURF" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.Z_CMP | RenderModeFlags.Z_UPD | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_CLAMP | RenderModeFlags.ZMODE_OPA | RenderModeFlags.ALPHA_CVG_SEL,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_AA_ZB_OPA_TERR" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.Z_CMP | RenderModeFlags.Z_UPD | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_CLAMP | RenderModeFlags.CVG_X_ALPHA | RenderModeFlags.ALPHA_CVG_SEL | RenderModeFlags.ZMODE_OPA | RenderModeFlags.TEX_EDGE,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_AA_ZB_TEX_TERR" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.Z_CMP | RenderModeFlags.Z_UPD | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_FULL | RenderModeFlags.ZMODE_OPA | RenderModeFlags.ALPHA_CVG_SEL,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_AA_ZB_SUB_TERR" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_CLAMP | RenderModeFlags.ZMODE_OPA | RenderModeFlags.ALPHA_CVG_SEL,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_A_MEM),
                "RM_AA_OPA_SURF" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.CVG_DST_CLAMP | RenderModeFlags.ZMODE_OPA | RenderModeFlags.ALPHA_CVG_SEL,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_A_MEM),
                "RM_RA_OPA_SURF" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_WRAP | RenderModeFlags.CLR_ON_CVG | RenderModeFlags.FORCE_BL | RenderModeFlags.ZMODE_OPA,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_AA_XLU_SURF" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_CLAMP | RenderModeFlags.CVG_X_ALPHA | RenderModeFlags.ALPHA_CVG_SEL | RenderModeFlags.FORCE_BL | RenderModeFlags.ZMODE_OPA,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_AA_XLU_LINE" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_FULL | RenderModeFlags.CVG_X_ALPHA | RenderModeFlags.ALPHA_CVG_SEL | RenderModeFlags.FORCE_BL | RenderModeFlags.ZMODE_OPA,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_AA_DEC_LINE" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_CLAMP | RenderModeFlags.CVG_X_ALPHA | RenderModeFlags.ALPHA_CVG_SEL | RenderModeFlags.ZMODE_OPA | RenderModeFlags.TEX_EDGE,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_A_MEM),
                "RM_AA_TEX_EDGE" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_FULL | RenderModeFlags.ZMODE_OPA | RenderModeFlags.ALPHA_CVG_SEL,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_A_MEM),
                "RM_AA_SUB_SURF" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_CLAMP | RenderModeFlags.ZMODE_OPA | RenderModeFlags.G_AC_DITHER,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_AA_PCL_SURF" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_CLAMP | RenderModeFlags.ZMODE_OPA | RenderModeFlags.ALPHA_CVG_SEL,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_AA_OPA_TERR" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_CLAMP | RenderModeFlags.CVG_X_ALPHA | RenderModeFlags.ALPHA_CVG_SEL | RenderModeFlags.ZMODE_OPA | RenderModeFlags.TEX_EDGE,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_AA_TEX_TERR" },
            { new HalfMode(RenderModeFlags.AA_EN | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_FULL | RenderModeFlags.ZMODE_OPA | RenderModeFlags.ALPHA_CVG_SEL,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_AA_SUB_TERR" },
            { new HalfMode(RenderModeFlags.Z_CMP | RenderModeFlags.Z_UPD | RenderModeFlags.CVG_DST_FULL | RenderModeFlags.ALPHA_CVG_SEL | RenderModeFlags.ZMODE_OPA,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_A_MEM),
                "RM_ZB_OPA_SURF" },
            { new HalfMode(RenderModeFlags.Z_CMP | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_FULL | RenderModeFlags.FORCE_BL | RenderModeFlags.ZMODE_XLU,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_ZB_XLU_SURF" },
            { new HalfMode(RenderModeFlags.Z_CMP | RenderModeFlags.CVG_DST_FULL | RenderModeFlags.ALPHA_CVG_SEL | RenderModeFlags.ZMODE_DEC,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_A_MEM),
                "RM_ZB_OPA_DECAL" },
            { new HalfMode(RenderModeFlags.Z_CMP | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_FULL | RenderModeFlags.FORCE_BL | RenderModeFlags.ZMODE_DEC,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_ZB_XLU_DECAL" },
            { new HalfMode(RenderModeFlags.Z_CMP | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_SAVE | RenderModeFlags.FORCE_BL | RenderModeFlags.ZMODE_XLU,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_ZB_CLD_SURF" },
            { new HalfMode(RenderModeFlags.Z_CMP | RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_SAVE | RenderModeFlags.FORCE_BL | RenderModeFlags.ZMODE_DEC,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_ZB_OVL_SURF" },
            { new HalfMode(RenderModeFlags.Z_CMP | RenderModeFlags.Z_UPD | RenderModeFlags.CVG_DST_FULL | RenderModeFlags.ZMODE_OPA | RenderModeFlags.G_AC_DITHER,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_0, BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_1),
                "RM_ZB_PCL_SURF" },
            { new HalfMode(RenderModeFlags.CVG_DST_CLAMP | RenderModeFlags.FORCE_BL | RenderModeFlags.ZMODE_OPA,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_0, BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_1),
                "RM_OPA_SURF" },
            { new HalfMode(RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_FULL | RenderModeFlags.FORCE_BL | RenderModeFlags.ZMODE_OPA,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_XLU_SURF" },
            { new HalfMode(RenderModeFlags.CVG_DST_CLAMP | RenderModeFlags.CVG_X_ALPHA | RenderModeFlags.ALPHA_CVG_SEL | RenderModeFlags.FORCE_BL | RenderModeFlags.ZMODE_OPA | RenderModeFlags.TEX_EDGE | RenderModeFlags.AA_EN,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_0, BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_1),
                "RM_TEX_EDGE" },
            { new HalfMode(RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_SAVE | RenderModeFlags.FORCE_BL | RenderModeFlags.ZMODE_OPA,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1MA),
                "RM_CLD_SURF" },
            { new HalfMode(RenderModeFlags.CVG_DST_FULL | RenderModeFlags.FORCE_BL | RenderModeFlags.ZMODE_OPA | RenderModeFlags.G_AC_DITHER,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_0, BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_1),
                "RM_PCL_SURF" },
            { new HalfMode(RenderModeFlags.IM_RD | RenderModeFlags.CVG_DST_SAVE | RenderModeFlags.FORCE_BL | RenderModeFlags.ZMODE_OPA,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_FOG, BlenderColorInput.G_BL_CLR_MEM, BlenderAlphaInput.G_BL_1),
                "RM_ADD" },
            { new HalfMode(RenderModeFlags.IM_RD | RenderModeFlags.FORCE_BL,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_0, BlenderColorInput.G_BL_CLR_BL, BlenderAlphaInput.G_BL_A_MEM),
                "RM_VISCVG" },
            { new HalfMode(0,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_A_IN, BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_1MA),
                "RM_NOOP" },
            { new HalfMode(RenderModeFlags.CVG_DST_CLAMP | RenderModeFlags.ZMODE_OPA,
                BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_0, BlenderColorInput.G_BL_CLR_IN, BlenderAlphaInput.G_BL_1),
                "RM_OPA_CI" },
        };
        public readonly int Combined;
        public readonly BlenderColorInput P, M;
        public readonly BlenderAlphaInput A, B;
        public readonly RenderModeFlags Flags;
        public HalfMode (RenderModeFlags Flags, BlenderColorInput P, BlenderAlphaInput A, BlenderColorInput M, BlenderAlphaInput B)
        {
            this.P = P;
            this.M = M;
            this.A = A;
            this.B = B;
            this.Flags = Flags;
            Combined =
                ((int)Flags << 2 * 4) |
                ((int)P     << 2 * 3) |
                ((int)A     << 2 * 2) |
                ((int)M     << 2 * 1) |
                ((int)B     << 2 * 0);
        }
        public override int GetHashCode()
        {
            return Combined;
        }
        public string GetModeString(int cycle)
        {
            string lookupName;
            if (BASE_MODE_NAMES.TryGetValue(this, out lookupName))
            {
                return string.Format("G_{0}{1}", lookupName, cycle == 1 ? "2" : "");
            }
            return "Unknown";
        }
        public override bool Equals(object obj)
        {
            if (obj is HalfMode other)
            {
                return Combined == other.Combined;
            }
            return false;
        }
    }

    public class RenderMode
    {
        public static readonly string[] MODE_NAMES =
        {
            "",               // 0x0001
            "",               // 0x0002
            "",               // 0x0004
            "AA_EN",          // 0x0008
            "Z_CMP",          // 0x0010
            "Z_UPD",          // 0x0020
            "IM_RD",          // 0x0040
            "CLR_ON_CVG",     // 0x0080
            "CVG_DST_WRAP",   // 0x0100
            "CVG_DST_FULL",   // 0x0200
            "ZMODE_INTER",    // 0x0400
            "ZMODE_XLU",      // 0x0800
            "CVG_X_ALPHA",    // 0x1000
            "ALPHA_CVG_SEL",  // 0x2000
            "FORCE_BL",       // 0x4000
        };

        public static readonly string[,] BLENDER_INPUTS =
        {
            { "G_BL_CLR_IN", "G_BL_CLR_MEM", "G_BL_CLR_BL",  "G_BL_CLR_FOG" }, // Color 1
            { "G_BL_CLR_IN", "G_BL_CLR_MEM", "G_BL_CLR_BL",  "G_BL_CLR_FOG" }, // Color 2
            { "G_BL_A_IN",   "G_BL_A_FOG",   "G_BL_A_SHADE", "G_BL_0"}, // Alpha 1
            { "G_BL_1MA",    "G_BL_A_MEM",   "G_BL_1",       "G_BL_0"}, // Alpha 2
        };

        public static readonly int[] BLENDER0_SHIFTS =
        {
            30, 26, 22, 18
        };
        public static readonly int[] BLENDER1_SHIFTS =
        {
            28, 24, 20, 16
        };

        public readonly ulong Words;
        public readonly HalfMode Mode0, Mode1;
        public readonly bool Known;

        public RenderMode(ulong words)
        {
            int[] BlendMode0 = new int[4], BlendMode1 = new int[4];
            Words = words;
            
            RenderModeFlags flags = (RenderModeFlags)((int)words & ((1 << MODE_NAMES.Length) - 1));

            for (int i = 0; i < BLENDER0_SHIFTS.Length; i++)
            {
                BlendMode0[i] = (int)(words >> BLENDER0_SHIFTS[i]) & 0b11;
            }

            for (int i = 0; i < BLENDER1_SHIFTS.Length; i++)
            {
                BlendMode1[i] = (int)(words >> BLENDER1_SHIFTS[i]) & 0b11;
            }

            HalfMode tmpMode0 = new HalfMode(flags,
                (BlenderColorInput)BlendMode0[0],
                (BlenderAlphaInput)BlendMode0[1],
                (BlenderColorInput)BlendMode0[2],
                (BlenderAlphaInput)BlendMode0[3]);

            HalfMode tmpMode1 = new HalfMode(flags,
                (BlenderColorInput)BlendMode1[0],
                (BlenderAlphaInput)BlendMode1[1],
                (BlenderColorInput)BlendMode1[2],
                (BlenderAlphaInput)BlendMode1[3]);

            (Mode0, Mode1, Known) = FindModes(tmpMode0, tmpMode1);
        }

        public RenderMode(BlenderColorInput P, BlenderAlphaInput A, BlenderColorInput M, BlenderAlphaInput B, RenderModeFlags flags)
        {
            HalfMode tmpMode0 = new HalfMode(flags, P, A, M, B);
            HalfMode tmpMode1 = new HalfMode(flags, P, A, M, B);

            (Mode0, Mode1, Known) = FindModes(tmpMode0, tmpMode1);
        }

        public RenderMode(BlenderColorInput P0, BlenderAlphaInput A0, BlenderColorInput M0, BlenderAlphaInput B0,
                          BlenderColorInput P1, BlenderAlphaInput A1, BlenderColorInput M1, BlenderAlphaInput B1,
                          RenderModeFlags flags)
        {
            HalfMode tmpMode0 = new HalfMode(flags, P0, A0, M0, B0);
            HalfMode tmpMode1 = new HalfMode(flags, P1, A1, M1, B1);

            (Mode0, Mode1, Known) = FindModes(tmpMode0, tmpMode1);
        }

        private static (HalfMode, bool) FindReplacement(HalfMode toReplace, HalfMode otherHalf)
        {
            foreach (var kvp in HalfMode.BASE_MODE_NAMES)
            {
                HalfMode modeToCheck = kvp.Key;
                RenderModeFlags combinedFlags = modeToCheck.Flags | otherHalf.Flags;
                if (combinedFlags == otherHalf.Flags)
                {
                    if (modeToCheck.P == toReplace.P &&
                        modeToCheck.M == toReplace.M &&
                        modeToCheck.A == toReplace.A &&
                        modeToCheck.B == toReplace.B)
                    {
                        return (modeToCheck, true);
                    }
                }
            }
            return (toReplace, false);
        }

        private static (HalfMode,HalfMode,bool) FindModes(HalfMode tmpMode0, HalfMode tmpMode1)
        {
            bool found0 = HalfMode.BASE_MODE_NAMES.TryGetValue(tmpMode0, out string name0);
            bool found1 = HalfMode.BASE_MODE_NAMES.TryGetValue(tmpMode1, out string name1);

            // If we found one but not the other, do a relaxed search for the second
            if (found0 && !found1)
            {
                (tmpMode1, found1) = FindReplacement(tmpMode1, tmpMode0);
            }
            else if (!found0 && found1)
            {
                (tmpMode0, found0) = FindReplacement(tmpMode0, tmpMode1);
            }

            return (tmpMode0, tmpMode1, found0 && found1);
        }

        public override string ToString()
        {
            return Mode0.GetModeString(0) + ", " + Mode1.GetModeString(1);
        }
    }
}
