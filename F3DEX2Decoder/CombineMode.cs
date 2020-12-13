using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F3DEX2Decoder
{

    public enum ColorCombinerMux
    {
        G_CCMUX_COMBINED = 0,
        G_CCMUX_TEXEL0 = 1,
        G_CCMUX_TEXEL1 = 2,
        G_CCMUX_PRIMITIVE = 3,
        G_CCMUX_SHADE = 4,
        G_CCMUX_ENVIRONMENT = 5,
        G_CCMUX_CENTER = 6,
        G_CCMUX_SCALE = 6,
        G_CCMUX_COMBINED_ALPHA = 7,
        G_CCMUX_TEXEL0_ALPHA = 8,
        G_CCMUX_TEXEL1_ALPHA = 9,
        G_CCMUX_PRIMITIVE_ALPHA = 10,
        G_CCMUX_SHADE_ALPHA = 11,
        G_CCMUX_ENV_ALPHA = 12,
        G_CCMUX_LOD_FRACTION = 13,
        G_CCMUX_PRIM_LOD_FRAC = 14,
        G_CCMUX_NOISE = 7,
        G_CCMUX_K4 = 7,
        G_CCMUX_K5 = 15,
        G_CCMUX_1 = 6,
        G_CCMUX_0 = 31,
    }

    public enum AlphaCombinerMux
    {
        G_ACMUX_COMBINED = 0,
        G_ACMUX_TEXEL0 = 1,
        G_ACMUX_TEXEL1 = 2,
        G_ACMUX_PRIMITIVE = 3,
        G_ACMUX_SHADE = 4,
        G_ACMUX_ENVIRONMENT = 5,
        G_ACMUX_LOD_FRACTION = 0,
        G_ACMUX_PRIM_LOD_FRAC = 6,
        G_ACMUX_1 = 6,
        G_ACMUX_0 = 7,
    }

    public class CombineMode
    {
        public static readonly Dictionary<CombineMode, string> ModeNames = new Dictionary<CombineMode, string>()
        {
            { new CombineMode(
                ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_PRIMITIVE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_PRIMITIVE,
                ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_PRIMITIVE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_PRIMITIVE),
            "G_CC_PRIMITIVE, G_CC_PRIMITIVE" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE,
                ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE),
            "G_CC_SHADE, G_CC_SHADE" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE,
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE),
            "G_CC_MODULATEI, G_CC_MODULATEI" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0,
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0),
            "G_CC_MODULATEIDECALA, G_CC_MODULATEIDECALA" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_ENVIRONMENT,
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_ENVIRONMENT),
            "G_CC_MODULATEIFADE, G_CC_MODULATEIFADE" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_TEXEL0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE, AlphaCombinerMux.G_ACMUX_0,
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_TEXEL0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE, AlphaCombinerMux.G_ACMUX_0),
            "G_CC_MODULATEIA, G_CC_MODULATEIA" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_TEXEL0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_ENVIRONMENT, AlphaCombinerMux.G_ACMUX_0,
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_TEXEL0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_ENVIRONMENT, AlphaCombinerMux.G_ACMUX_0),
            "G_CC_MODULATEIFADEA, G_CC_MODULATEIFADEA" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_ENVIRONMENT, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0, AlphaCombinerMux.G_ACMUX_0,
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_ENVIRONMENT, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0, AlphaCombinerMux.G_ACMUX_0),
            "G_CC_MODULATEFADE, G_CC_MODULATEFADE" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_PRIMITIVE, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_PRIMITIVE,
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_PRIMITIVE, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_PRIMITIVE),
            "G_CC_MODULATEI_PRIM, G_CC_MODULATEI_PRIM" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_PRIMITIVE, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_TEXEL0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_PRIMITIVE, AlphaCombinerMux.G_ACMUX_0,
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_PRIMITIVE, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_TEXEL0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_PRIMITIVE, AlphaCombinerMux.G_ACMUX_0),
            "G_CC_MODULATEIA_PRIM, G_CC_MODULATEIA_PRIM" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_PRIMITIVE, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0,
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_PRIMITIVE, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0),
            "G_CC_MODULATEIDECALA_PRIM, G_CC_MODULATEIDECALA_PRIM" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_ENVIRONMENT, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_SHADE, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_ENVIRONMENT, AlphaCombinerMux.G_ACMUX_0,
                ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_ENVIRONMENT, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_SHADE, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_ENVIRONMENT, AlphaCombinerMux.G_ACMUX_0),
            "G_CC_FADE, G_CC_FADE" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_ENVIRONMENT, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_TEXEL0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_ENVIRONMENT, AlphaCombinerMux.G_ACMUX_0,
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_ENVIRONMENT, ColorCombinerMux.G_CCMUX_0,
                AlphaCombinerMux.G_ACMUX_TEXEL0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_ENVIRONMENT, AlphaCombinerMux.G_ACMUX_0),
            "G_CC_FADEA, G_CC_FADEA" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_TEXEL0,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE,
                ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_TEXEL0,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE),
            "G_CC_DECALRGB, G_CC_DECALRGB" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_TEXEL0,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0,
                ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_TEXEL0,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0),
            "G_CC_DECALRGBA, G_CC_DECALRGBA" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_TEXEL0,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_ENVIRONMENT,
                ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_TEXEL0,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_ENVIRONMENT),
            "G_CC_DECALFADE, G_CC_DECALFADE" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_TEXEL0,
                AlphaCombinerMux.G_ACMUX_TEXEL0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_ENVIRONMENT, AlphaCombinerMux.G_ACMUX_0,
                ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_TEXEL0,
                AlphaCombinerMux.G_ACMUX_TEXEL0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_ENVIRONMENT, AlphaCombinerMux.G_ACMUX_0),
            "G_CC_DECALFADEA, G_CC_DECALFADEA" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_ENVIRONMENT, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE,
                ColorCombinerMux.G_CCMUX_ENVIRONMENT, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE),
            "G_CC_BLENDI, G_CC_BLENDI" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_ENVIRONMENT, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_TEXEL0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE, AlphaCombinerMux.G_ACMUX_0,
                ColorCombinerMux.G_CCMUX_ENVIRONMENT, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_TEXEL0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE, AlphaCombinerMux.G_ACMUX_0),
            "G_CC_BLENDIA, G_CC_BLENDIA" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_ENVIRONMENT, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0,
                ColorCombinerMux.G_CCMUX_ENVIRONMENT, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0),
            "G_CC_BLENDIDECALA, G_CC_BLENDIDECALA" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_TEXEL0_ALPHA, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE,
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_TEXEL0_ALPHA, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE),
            "G_CC_BLENDRGBA, G_CC_BLENDRGBA" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_TEXEL0_ALPHA, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0,
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_TEXEL0_ALPHA, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0),
            "G_CC_BLENDRGBDECALA, G_CC_BLENDRGBDECALA" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_TEXEL0_ALPHA, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_ENVIRONMENT,
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_TEXEL0_ALPHA, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_ENVIRONMENT),
            "G_CC_BLENDRGBFADEA, G_CC_BLENDRGBFADEA" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE,
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE),
            "G_CC_ADDRGB, G_CC_ADDRGB" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0,
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0),
            "G_CC_ADDRGBDECALA, G_CC_ADDRGBDECALA" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_ENVIRONMENT,
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_ENVIRONMENT),
            "G_CC_ADDRGBFADE, G_CC_ADDRGBFADE" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_ENVIRONMENT, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE,
                ColorCombinerMux.G_CCMUX_ENVIRONMENT, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE),
            "G_CC_REFLECTRGB, G_CC_REFLECTRGB" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_ENVIRONMENT, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0,
                ColorCombinerMux.G_CCMUX_ENVIRONMENT, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0),
            "G_CC_REFLECTRGBDECALA, G_CC_REFLECTRGBDECALA" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_PRIMITIVE, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE,
                ColorCombinerMux.G_CCMUX_PRIMITIVE, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE),
            "G_CC_HILITERGB, G_CC_HILITERGB" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_PRIMITIVE, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_PRIMITIVE, AlphaCombinerMux.G_ACMUX_SHADE, AlphaCombinerMux.G_ACMUX_TEXEL0, AlphaCombinerMux.G_ACMUX_SHADE,
                ColorCombinerMux.G_CCMUX_PRIMITIVE, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_PRIMITIVE, AlphaCombinerMux.G_ACMUX_SHADE, AlphaCombinerMux.G_ACMUX_TEXEL0, AlphaCombinerMux.G_ACMUX_SHADE),
            "G_CC_HILITERGBA, G_CC_HILITERGBA" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_PRIMITIVE, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0,
                ColorCombinerMux.G_CCMUX_PRIMITIVE, ColorCombinerMux.G_CCMUX_SHADE, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0),
            "G_CC_HILITERGBDECALA, G_CC_HILITERGBDECALA" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0,
                ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0),
            "G_CC_SHADEDECALA, G_CC_SHADEDECALA" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_ENVIRONMENT,
                ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_0, ColorCombinerMux.G_CCMUX_SHADE,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_ENVIRONMENT),
            "G_CC_SHADEFADEA, G_CC_SHADEFADEA" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_PRIMITIVE, ColorCombinerMux.G_CCMUX_ENVIRONMENT, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_ENVIRONMENT,
                AlphaCombinerMux.G_ACMUX_TEXEL0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE, AlphaCombinerMux.G_ACMUX_0,
                ColorCombinerMux.G_CCMUX_PRIMITIVE, ColorCombinerMux.G_CCMUX_ENVIRONMENT, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_ENVIRONMENT,
                AlphaCombinerMux.G_ACMUX_TEXEL0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE, AlphaCombinerMux.G_ACMUX_0),
            "G_CC_BLENDPE, G_CC_BLENDPE" },

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_PRIMITIVE, ColorCombinerMux.G_CCMUX_ENVIRONMENT, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_ENVIRONMENT,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0,
                ColorCombinerMux.G_CCMUX_PRIMITIVE, ColorCombinerMux.G_CCMUX_ENVIRONMENT, ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_ENVIRONMENT,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_TEXEL0),
            "G_CC_BLENDPEDECALA, G_CC_BLENDPEDECALA" },

            // Oddball modes (TODO)


            // Color convert

            { new CombineMode(
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_K4, ColorCombinerMux.G_CCMUX_K5, ColorCombinerMux.G_CCMUX_TEXEL0,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE,
                ColorCombinerMux.G_CCMUX_TEXEL0, ColorCombinerMux.G_CCMUX_K4, ColorCombinerMux.G_CCMUX_K5, ColorCombinerMux.G_CCMUX_TEXEL0,
                AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_0, AlphaCombinerMux.G_ACMUX_SHADE),
            "G_CC_1CYUV2RGB, G_CC_1CYUV2RGB" },

            // 2 cycle modes


        };

        private readonly ColorCombinerMux[] m_cc0, m_cc1;
        private readonly AlphaCombinerMux[] m_ac0, m_ac1;

        public CombineMode(ColorCombinerMux[] cc0, AlphaCombinerMux[] ac0, ColorCombinerMux[] cc1, AlphaCombinerMux[] ac1)
        {
            m_cc0 = new ColorCombinerMux[4];
            m_cc1 = new ColorCombinerMux[4];
            m_ac0 = new AlphaCombinerMux[4];
            m_ac1 = new AlphaCombinerMux[4];

            Array.Copy(m_cc0, cc0, m_cc0.Length);
            Array.Copy(m_cc1, cc1, m_cc1.Length);
            Array.Copy(m_ac0, ac0, m_ac0.Length);
            Array.Copy(m_ac1, ac1, m_ac1.Length);
        }
        public CombineMode(
            ColorCombinerMux cca0, ColorCombinerMux ccb0, ColorCombinerMux ccc0, ColorCombinerMux ccd0,
            AlphaCombinerMux aca0, AlphaCombinerMux acb0, AlphaCombinerMux acc0, AlphaCombinerMux acd0,
            ColorCombinerMux cca1, ColorCombinerMux ccb1, ColorCombinerMux ccc1, ColorCombinerMux ccd1,
            AlphaCombinerMux aca1, AlphaCombinerMux acb1, AlphaCombinerMux acc1, AlphaCombinerMux acd1)
        {
            m_cc0 = new ColorCombinerMux[4];
            m_cc1 = new ColorCombinerMux[4];
            m_ac0 = new AlphaCombinerMux[4];
            m_ac1 = new AlphaCombinerMux[4];

            m_cc0[0] = cca0; m_cc0[1] = ccb0; m_cc0[2] = ccc0; m_cc0[3] = ccd0;
            m_cc1[0] = cca1; m_cc1[1] = ccb1; m_cc1[2] = ccc1; m_cc1[3] = ccd1;
            m_ac0[0] = aca0; m_ac0[1] = acb0; m_ac0[2] = acc0; m_ac0[3] = acd0;
            m_ac1[0] = aca1; m_ac1[1] = acb1; m_ac1[2] = acc1; m_ac1[3] = acd1;
        }

        public CombineMode(ulong modeVal)
        {
            m_cc0 = new ColorCombinerMux[4];
            m_cc1 = new ColorCombinerMux[4];
            m_ac0 = new AlphaCombinerMux[4];
            m_ac1 = new AlphaCombinerMux[4];

            m_ac1[3] = (AlphaCombinerMux)Utils.GetBitsAndShift(ref modeVal, 3);
            m_ac1[1] = (AlphaCombinerMux)Utils.GetBitsAndShift(ref modeVal, 3);
            m_cc1[3] = (ColorCombinerMux)Utils.GetBitsAndShift(ref modeVal, 3);
                if (m_cc1[3] == ColorCombinerMux.G_CCMUX_COMBINED_ALPHA) m_cc1[3] = ColorCombinerMux.G_CCMUX_0;
            m_ac0[3] = (AlphaCombinerMux)Utils.GetBitsAndShift(ref modeVal, 3);

            m_ac0[1] = (AlphaCombinerMux)Utils.GetBitsAndShift(ref modeVal, 3);
            m_cc0[3] = (ColorCombinerMux)Utils.GetBitsAndShift(ref modeVal, 3);
                if (m_cc0[3] == ColorCombinerMux.G_CCMUX_COMBINED_ALPHA) m_cc0[3] = ColorCombinerMux.G_CCMUX_0;
            m_ac1[2] = (AlphaCombinerMux)Utils.GetBitsAndShift(ref modeVal, 3);
            m_ac1[0] = (AlphaCombinerMux)Utils.GetBitsAndShift(ref modeVal, 3);
            
            m_cc1[1] = (ColorCombinerMux)Utils.GetBitsAndShift(ref modeVal, 4);
                if (m_cc1[1] == ColorCombinerMux.G_CCMUX_K5) m_cc1[1] = ColorCombinerMux.G_CCMUX_0;
            m_cc0[1] = (ColorCombinerMux)Utils.GetBitsAndShift(ref modeVal, 4);
                if (m_cc0[1] == ColorCombinerMux.G_CCMUX_K5) m_cc0[1] = ColorCombinerMux.G_CCMUX_0;
            m_cc1[2] = (ColorCombinerMux)Utils.GetBitsAndShift(ref modeVal, 5);
            m_cc1[0] = (ColorCombinerMux)Utils.GetBitsAndShift(ref modeVal, 4);
                if (m_cc1[0] == ColorCombinerMux.G_CCMUX_K5) m_cc1[0] = ColorCombinerMux.G_CCMUX_0;

            m_ac0[2] = (AlphaCombinerMux)Utils.GetBitsAndShift(ref modeVal, 3);
            m_ac0[0] = (AlphaCombinerMux)Utils.GetBitsAndShift(ref modeVal, 3);
            m_cc0[2] = (ColorCombinerMux)Utils.GetBitsAndShift(ref modeVal, 5);
            m_cc0[0] = (ColorCombinerMux)Utils.GetBitsAndShift(ref modeVal, 4);
                if (m_cc0[0] == ColorCombinerMux.G_CCMUX_K5) m_cc0[0] = ColorCombinerMux.G_CCMUX_0;
        }

        public override bool Equals(object obj)
        {
            if (obj is CombineMode other)
            {
                return (
                    Enumerable.SequenceEqual(m_cc0, other.m_cc0) &&
                    Enumerable.SequenceEqual(m_cc1, other.m_cc1) &&
                    Enumerable.SequenceEqual(m_ac0, other.m_ac0) &&
                    Enumerable.SequenceEqual(m_ac1, other.m_ac1));
            }
            return false;
        }

        public override int GetHashCode()
        {

            return HashCode.Combine(
                HashCode.Combine(m_cc0[0], m_cc0[1], m_cc0[2], m_cc0[3], m_cc1[0], m_cc1[1], m_cc1[2], m_cc1[3]),
                HashCode.Combine(m_ac0[0], m_ac0[1], m_ac0[2], m_ac0[3], m_ac1[0], m_ac1[1], m_ac1[2], m_ac1[3]));
        }

        public override string ToString()
        {
            if (ModeNames.ContainsKey(this))
            {
                return ModeNames[this];
            }

            string retVal = "";

            foreach (ColorCombinerMux mux in m_cc0)
            {
                retVal += mux.ToString().Substring(8) + ", ";
            }
            foreach (AlphaCombinerMux mux in m_ac0)
            {
                if (mux == AlphaCombinerMux.G_ACMUX_PRIM_LOD_FRAC)
                {
                    retVal += "1, ";
                }
                else
                {
                    retVal += mux.ToString().Substring(8) + ", ";
                }
            }
            foreach (ColorCombinerMux mux in m_cc1)
            {
                retVal += mux.ToString().Substring(8) + ", ";
            }
            foreach (AlphaCombinerMux mux in m_ac1)
            {
                if (mux == AlphaCombinerMux.G_ACMUX_PRIM_LOD_FRAC)
                {
                    retVal += "1, ";
                }
                else
                {
                    retVal += mux.ToString().Substring(8) + ", ";
                }
            }

            return retVal.Substring(0, retVal.Length - 2);
        }
    }
}
