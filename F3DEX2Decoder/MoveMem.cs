using System;
using System.Collections.Generic;
using System.Text;

namespace F3DEX2Decoder
{
    public enum MoveMemIndex
    {
        G_MV_MMTX       = 2,
        G_MV_PMTX       = 6,
        G_MV_VIEWPORT   = 8,
        G_MV_LIGHT      = 10,
        G_MV_POINT      = 12,
        G_MV_MATRIX     = 14,
    }
    public enum MoveMemOffset
    {
        G_MVO_LOOKATX   = (0 * 24),
        G_MVO_LOOKATY   = (1 * 24),
        G_MVO_L0        = (2 * 24),
        G_MVO_L1        = (3 * 24),
        G_MVO_L2        = (4 * 24),
        G_MVO_L3        = (5 * 24),
        G_MVO_L4        = (6 * 24),
        G_MVO_L5        = (7 * 24),
        G_MVO_L6        = (8 * 24),
        G_MVO_L7        = (9 * 24),
    }
    public class MoveMem
    {
        public static readonly int VIEWPORT_SIZE = 16;
        public static readonly int LIGHT_SIZE = 16;
        public static readonly int LIGHT1 = 1;

        public readonly MoveMemIndex Index;
        public readonly int Offset;
        public readonly int Length;
        public readonly ulong Address;
        public MoveMem(ulong words)
        {
            Length = (int)(((words >> (32 + 19)) & 0b11111) + 1) * 8;
            Offset = (int)((words >> (32 + 8)) & 0xFF) * 8;
            Index = (MoveMemIndex)((words >> (32 + 0)) & 0xFF);
            Address = words & 0xFFFFFFFF;
        }

        public override string ToString()
        {
            string offsetStr = ((MoveMemOffset)Offset).ToString();
            switch (Index)
            {
                case MoveMemIndex.G_MV_MMTX:
                case MoveMemIndex.G_MV_PMTX:
                    break;
                case MoveMemIndex.G_MV_VIEWPORT:
                    {
                        if (Length == VIEWPORT_SIZE && Offset == 0)
                        {
                            return string.Format("gsSPViewport(0x{0:X8})", Address);
                        }
                        break;
                    }
                case MoveMemIndex.G_MV_LIGHT:
                    {
                        if (Length == LIGHT_SIZE && offsetStr.StartsWith("G_MVO_L"))
                        {
                            int lightIndex = (Offset - (int)MoveMemOffset.G_MVO_L0) / (MoveMemOffset.G_MVO_L1 - MoveMemOffset.G_MVO_L0);
                            if (lightIndex >= 0)
                            {
                                return string.Format("gsSPLight(0x{0:X8}, LIGHT_{1})", Address, LIGHT1 + lightIndex);
                            }
                            else // TODO gsSPLookAt
                            {
                                switch (Offset)
                                {
                                    case (int)MoveMemOffset.G_MVO_LOOKATX:
                                        return string.Format("gsSPLookAtX(0x{0:X8})", Address);
                                    case (int)MoveMemOffset.G_MVO_LOOKATY:
                                        return string.Format("gsSPLookAtY(0x{0:X8})", Address);
                                }
                            }
                        }
                        break;
                    }
            }

            return string.Format("gsDma2({0}, 0x{1:X8}, 0x{2:X}, {3})", Index, Address, Length, (MoveMemOffset)Offset);
        }
    }
}
