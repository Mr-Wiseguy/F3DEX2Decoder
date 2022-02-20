using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace F3DEX2Decoder
{
    public enum MatrixType
    {
        G_MTX_MODELVIEW = 0x00,
        G_MTX_PROJECTION = 0x04,
        MASK = 0x04,
    }
    public enum MatrixLoadType
    {
        G_MTX_MUL = 0x00,
        G_MTX_LOAD = 0x02,
        MASK = 0x02,
    }
    public enum MatrixPushType
    {
        G_MTX_NOPUSH = 0x00,
        G_MTX_PUSH = 0x01,
        MASK = 0x01,
    }
    public static class F3DEX2Decoder
    {
        public static ulong SegToVirtual(ulong address, ulong[] segTable)
        {
            if (address >= 0x80000000)
            {
                address -= 0x80000000;
            }
            if ((address & 0xFF000000) != 0)
            {
                int segment = (int)(address >> 24) & 0xFF;
                ulong segmentAddr = segTable[segment] & 0x1FFFFFF;
                address &= 0xFFFFFF;
                address += segmentAddr;
            }

            return address;
        }
        public static void PrintDisplaylist(byte[] rdram, ulong address, ulong[] segTable, ref int totalTris, ref int totalVertices, string indentation = "")
        {
            List<F3DEX2Command> cmds = GetDLCommands(rdram, address, segTable);

            for (int i = 0; i < cmds.Count; i++)
            {
                F3DEX2Command cmd = cmds[i];

                string cmdStr = cmd.ParseString(cmds.GetRange(i + 1, Math.Min((cmds.Count() - i - 1), 10)), out int cmdsSkipped, ref totalTris, ref totalVertices, indentation + "                    "); // TODO replace getrange with something of constant time

                if (cmdStr.StartsWith("gs"))
                {
                    Console.Write(string.Format("{0:X16}@{1:X8}/{2:X8}: ", cmd.Words, address + (ulong)(i * 8), SegToVirtual(address + (ulong)(i * 8), segTable)));
                    Console.WriteLine(indentation + cmdStr + ",");
                }
                else
                {
                    Console.WriteLine(indentation + cmd.CommandId);
                    Console.WriteLine(" " + indentation + cmd + ",");
                }
                if (cmd.CommandId == F3DEX2CommandId.G_DL && (cmd.Words >> (32 + 16) & 0xFF) != 0x01)
                {
                    PrintDisplaylist(rdram, cmd.Words & 0xFFFFFFFF, segTable, ref totalTris, ref totalVertices, "  " + indentation);
                }
                else if (cmd.CommandId == F3DEX2CommandId.G_MOVEWORD)
                {
                    MoveWord mw = new MoveWord(cmd.Words);
                    if (mw.Index == MoveWordIndex.G_MW_SEGMENT)
                    {
                        segTable[mw.Offset / 4] = mw.Data;
                    }
                }

                i += cmdsSkipped;
            }


        }
        public static List<F3DEX2Command> GetDLCommands(byte[] rdram, ulong address, ulong[] segTable)
        {
            List<F3DEX2Command> commands = new List<F3DEX2Command>();

            address = SegToVirtual(address, segTable);

            do
            {
                ArraySegment<byte> curWordsArray = new ArraySegment<byte>(rdram, (int)address, 8);
                ulong curWords = BitConverter.ToUInt64(curWordsArray.Reverse().ToArray());
                F3DEX2Command curCommand = new F3DEX2Command(curWords);
                commands.Add(curCommand);
                address += 8;
                if (curCommand.CommandId == F3DEX2CommandId.G_DL && (curWords >> (32 + 16) & 0xFF) == 0x01)
                {
                    address = curWords & 0xFFFFFFFF;
                    if (address >= 0x80000000)
                    {
                        address -= 0x80000000;
                    }
                    else if ((address & 0xFF000000) != 0)
                    {
                        int segment = (int)(address >> 24) & 0xFF;
                        ulong segmentAddr = segTable[segment] & 0x1FFFFFFF;
                        address &= 0xFFFFFF;
                        address += segmentAddr;
                    }
                    else
                    {
                        throw new Exception(string.Format("Invalid address: {0}", address));
                    }
                }
                //else if (curCommand.CommandByte == G_)
            } while (commands.Last().CommandId != F3DEX2CommandId.G_ENDDL);

            return commands;
        }

        public static void ParseSetTile(F3DEX2Command cmd,
            out int fmt, out int siz, out int line, out int tmem, out int tile, out int palette, out int cmt,
            out int maskt, out int shiftt, out int cms, out int masks, out int shifts)
        {
            fmt = (int)((cmd.Words >> (32 + 21)) & 0b111);
            siz = (int)((cmd.Words >> (32 + 19)) & 0b11);
            line = (int)((cmd.Words >> (32 + 9)) & 0b111111111);
            tmem = (int)((cmd.Words >> (32 + 0)) & 0b111111111);

            tile = (int)((cmd.Words >> (24)) & 0b111);
            palette = (int)((cmd.Words >> (20)) & 0b1111);
            cmt = (int)((cmd.Words >> (18)) & 0b11);
            maskt = (int)((cmd.Words >> (14)) & 0b1111);
            shiftt = (int)((cmd.Words >> (10)) & 0b1111);
            cms = (int)((cmd.Words >> (8)) & 0b11);
            masks = (int)((cmd.Words >> (4)) & 0b1111);
            shifts = (int)((cmd.Words >> (0)) & 0b1111);
        }

        public static void ParseLoadTLUT(F3DEX2Command cmd,
            out int tile, out int count)
        {
            tile = (int)((cmd.Words >> (24)) & 0b111);
            count = (int)((cmd.Words >> (14)) & 0b1111111111);
        }

        public static void ParseLoadTileGeneric(F3DEX2Command cmd,
            out int tile, out int uls, out int ult, out int lrs, out int lrt)
        {
            uls = (int)((cmd.Words >> (32 + 12)) & 0xFFF);
            ult = (int)((cmd.Words >> (32 + 0)) & 0xFFF);
            tile = (int)((cmd.Words >> (24)) & 0b111);
            lrs = (int)((cmd.Words >> (12)) & 0xFFF);
            lrt = (int)((cmd.Words >> (0)) & 0xFFF);
        }

        public static void ParseSetImage(F3DEX2Command cmd,
            out int fmt, out int siz, out int width, out ulong i)
        {
            fmt = (int)((cmd.Words >> (32 + 21)) & 0b111);
            siz = (int)((cmd.Words >> (32 + 19)) & 0b11);
            width = (int)((cmd.Words >> (32 + 0)) & 0xFFF) + 1;
            i = cmd.Words & 0xFFFFFFFF;
        }

        public static void ParseRect(F3DEX2Command cmd,
            out int xl, out int yl, out int xh, out int yh, out int tile)
        {
            xh = (int)((cmd.Words >> (32 + 12)) & 0xFFF);
            yh = (int)((cmd.Words >> (32 + 0)) & 0xFFF);
            tile = (int)((cmd.Words >> (24)) & 0b111);
            xl = (int)((cmd.Words >> (12)) & 0xFFF);
            yl = (int)((cmd.Words >> (0)) & 0xFFF);
        }

        public static void ParseRDPHalf_W(F3DEX2Command cmd,
            out ulong a)
        {
            a = cmd.Words & 0xFFFFFFFF;
        }

        public static void ParseRDPHalf_HH(F3DEX2Command cmd,
            out int a, out int b)
        {
            a = (int)(cmd.Words >> (16)) & 0xFFFF;
            b = (int)(cmd.Words >> (0)) & 0xFFFF;
        }

        public static void ParseConvert(F3DEX2Command cmd,
            out int[] coeffs)
        {
            ulong words = cmd.Words;
            coeffs = new int[6];

            for (int i = 5; i >= 0; i--)
            {
                coeffs[i] = (int)(words & 0b111111111);
                
                if ((coeffs[i] & 0b100000000) != 0)
                {
                    coeffs[i] = unchecked((int)((uint) coeffs[i] | 0xFFFFFE00));
                }

                words >>= 9;
            }
        }

        public static void ParseScissor(F3DEX2Command cmd,
            out int mode, out int ulx, out int uly, out int lrx, out int lry)
        {
            ulx = (int)((cmd.Words >> (32 + 12)) & 0xFFF);
            uly = (int)((cmd.Words >> (32 + 0)) & 0xFFF);
            mode = (int)((cmd.Words >> (24)) & 0b11);
            lrx = (int)((cmd.Words >> (12)) & 0xFFF);
            lry = (int)((cmd.Words >> (0)) & 0xFFF);
        }
        
        public static void ParseSetColor(F3DEX2Command cmd,
            out ulong col)
        {
            col = (cmd.Words & 0xFFFFFFFF);
        }

        public static void GetComponents(ulong color,
            out int r, out int g, out int b, out int a)
        {
            r = (int)((color >> 24) & 0xFF);
            g = (int)((color >> 16) & 0xFF);
            b = (int)((color >>  8) & 0xFF);
            a = (int)((color >>  0) & 0xFF);
        }
        public static void ParseOtherMode(F3DEX2Command cmd,
            out int sft, out int len, out ulong data)
        {
            len = (int)((cmd.Words >> (32 + 0)) & 0xFF) + 1;
            sft = 32 - ((int)((cmd.Words >> (32 + 8)) & 0xFF) + len);
            data = cmd.Words & 0xFFFFFFFF;
        }
        public static void ParseBranchLessZ(F3DEX2Command cmd,
            out int vtx, out int zval)
        {
            vtx = (int)((cmd.Words >> 32) & 0xFFF) / 2;
            zval = (int)(cmd.Words & 0xFFFFFFFF);
        }
        public static void ParseLoadUcode(F3DEX2Command cmd,
            out ulong uc_start, out int uc_dsize)
        {
            uc_dsize = (int)((cmd.Words >> (32 + 0)) & 0xFFFF) + 1;
            uc_start = cmd.Words & 0xFFFFFFFF;
        }

        public static void ParseRDPSetOtherMode(F3DEX2Command cmd,
            out PipelineMode pm, out CycleType cyc, out TexturePersp tp, out TextureDetail td, out TextureLOD tl,
            out TextureLUT tt, out TextureFilter tf, out TextureConvert tc, out CombineKey ck, out ColorDither cd,
            out AlphaDither ad, out AlphaCompare ac, out DepthSource zs, out RenderMode rm)
        {
            rm = new RenderMode(cmd.Words & (0xFFFFFFFF & ~((ulong)AlphaCompare.Mask | (ulong)DepthSource.Mask)));

            if (!rm.Known) // Handle TCL modes by checking again with alpha compare and dither included
            {
                rm = new RenderMode(cmd.Words & (0xFFFFFFFF & ~(ulong)DepthSource.Mask));
            }

            ulong wordH = cmd.Words >> 32;
            ad =  (AlphaDither)     (wordH & (ulong)AlphaDither.Mask);
            cd =  (ColorDither)     (wordH & (ulong)ColorDither.Mask);
            ck =  (CombineKey)      (wordH & (ulong)CombineKey.Mask);
            pm =  (PipelineMode)    (wordH & (ulong)PipelineMode.Mask);
            cyc = (CycleType)       (wordH & (ulong)CycleType.Mask);
            tp =  (TexturePersp)    (wordH & (ulong)TexturePersp.Mask);
            td =  (TextureDetail)   (wordH & (ulong)TextureDetail.Mask);
            tl =  (TextureLOD)      (wordH & (ulong)TextureLOD.Mask);
            tt =  (TextureLUT)      (wordH & (ulong)TextureLUT.Mask);
            tf =  (TextureFilter)   (wordH & (ulong)TextureFilter.Mask);
            tc =  (TextureConvert)  (wordH & (ulong)TextureConvert.Mask);

            ac =  (AlphaCompare)    (cmd.Words & (ulong)AlphaCompare.Mask);
            zs =  (DepthSource)     (cmd.Words & (ulong)DepthSource.Mask);
        }

        public static void ParseSetCombine(F3DEX2Command cmd,
            out ulong mode)
        {
            mode = cmd.Words & 0xFFFFFFFFFFFFFF;
        }

        public static void ParseMatrix(F3DEX2Command cmd,
            out MatrixType mType, out MatrixLoadType loadType, out MatrixPushType pushType, out ulong mAddr,
            out int size, out int offset)
        {
            int matrixParams = (int)((cmd.Words >> (32 + 0)) & 0xFF) ^ (int)MatrixPushType.G_MTX_PUSH;

            mType =        (MatrixType)(matrixParams & (int)MatrixType.MASK);
            loadType = (MatrixLoadType)(matrixParams & (int)MatrixLoadType.MASK);
            pushType = (MatrixPushType)(matrixParams & (int)MatrixPushType.MASK);
            size =  (int)(((cmd.Words >> (32 + 19)) & 0b11111) + 1) * 8;
            offset = (int)((cmd.Words >> (32 + 8)) & 0xFF) * 8;

            mAddr = cmd.Words & 0xFFFFFFFF;
        }

        public static void ParsePopMtx(F3DEX2Command cmd,
            out int bytes, out int idx)
        {
            bytes = (int)(((cmd.Words >> (32 + 19)) & 0b11111) + 1) * 8;
            idx = (int)((cmd.Words >> (32 + 0)) & 0xFF);
        }

        public static void ParseTexture(F3DEX2Command cmd,
            out int s, out int t, out int level, out int tile, out int on)
        {
            level = (int)((cmd.Words >> (32 + 11))) & 0b111;
            tile = (int)((cmd.Words >> (32 + 8))) & 0b111;
            on = (int)((cmd.Words >> (32 + 1))) & 0b1111111;
            s = (int)((cmd.Words >> (0 + 16))) & 0xFFFF;
            t = (int)((cmd.Words >> (0 + 0))) & 0xFFFF;
        }

        public static void ParseVtx(F3DEX2Command cmd,
            out int vtxCount, out int vtxOffset, out ulong vtxAddr)
        {
            vtxCount =    (int)((cmd.Words >> (32 + 12)) & 0xFF);
            int lastVtx = (int)((cmd.Words >> (32 +  1)) & 0x7F);
            vtxOffset = lastVtx - vtxCount;
            vtxAddr = cmd.Words & 0xFFFFFFFF;
        }

        public static void ParseCullDL(F3DEX2Command cmd,
            out int vstart, out int vend)
        {
            vstart = (int)((cmd.Words >> (32 + 0)) & 0xFFFF) / 2;
            vend   = (int)((cmd.Words >> ( 0 + 0)) & 0xFFFF) / 2;
        }

        public static void ParseTri(F3DEX2Command cmd, bool firstTri,
            out int a, out int b, out int c)
        {
            a = (int)((cmd.Words >> ((firstTri ? 32 : 0) + 16)) & 0xFF) / 2;
            b = (int)((cmd.Words >> ((firstTri ? 32 : 0) +  8)) & 0xFF) / 2;
            c = (int)((cmd.Words >> ((firstTri ? 32 : 0) +  0)) & 0xFF) / 2;
        }

        public static string TexCoord102ToStr(int coord, bool isHighCoord = false)
        {
            if ((coord & 0b11) == 0)
            {
                if (isHighCoord)
                {
                    return string.Format("({0} - 1) << 2", (coord >> 2) + 1);
                }
                else
                {
                    return string.Format("{0} << 2", (coord >> 2));
                }
            }
            else
            {
                return string.Format("0x{0:X3}", coord);
            }
        }

        public static string TexCoord102OffsetToStr(int coordLow, int coordHigh)
        {
            if ((coordLow & 0b11) == 0 && (coordHigh & 0b11) == 0)
            {
                return string.Format("({0} + {1}) << 2", coordLow >> 2, (coordHigh - coordLow >> 2));
            }
            else
            {
                return string.Format("0x{0:X3}", coordHigh);
            }
        }

        public static string PackedColorHalfwordToStr(int col)
        {
            int r = col & 0xF800 >> 8;
            int g = col & 0x07C0 >> 3;
            int b = col & 0x003E << 2;
            int a = col & 0x0001;

            return string.Format("GPACK_RGBA5551({0}, {1}, {2}, {3})", r, g, b, a);
        }

        public static string PackedColorWordToStr(ulong col)
        {
            string first = PackedColorHalfwordToStr((int)(col >> 16) & 0xFFFF);
            string second = PackedColorHalfwordToStr((int)(col >>  0) & 0xFFFF);

            return first + " << 16 | " + second;
        }
    }
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 2 || args.Length > 3)
            {
                Console.Error.WriteLine("Incorrect Usage!");
                Console.Error.WriteLine("Program must be called with 2 or 3 arguments: filename of rdram dump and memory address of starting displaylist and optional segment table address");
                return -1;
            }

            byte[] rdram;
            ulong startAddress;
            ulong[] segTable = new ulong[32];

            try
            {
                // Try to parse as decimal
                startAddress = Convert.ToUInt64(args[1], 10);
            }
            catch
            {
                try
                {
                    // Try to parse as hexadecimal
                    startAddress = Convert.ToUInt64(args[1], 16);
                }
                catch
                {
                    Console.Error.WriteLine(string.Format(string.Format("Invalid displaylist start address given: {0}", args[1])));
                    return -1;
                }
            }

            if (startAddress < 0x80000000 || startAddress > (0x88000000 - 8))
            {
                Console.Error.WriteLine(string.Format("Displaylist start address 0x{0:X7} out of range!", startAddress));
                return -1;
            }

            try
            {
                rdram = File.ReadAllBytes(args[0]);
            }
            catch (FileNotFoundException)
            {
                Console.Error.WriteLine(string.Format("File {0} does not exist!", Path.Combine(Directory.GetCurrentDirectory(), args[0])));
                return -1;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(string.Format("Error opening file {0}!", Path.Combine(Directory.GetCurrentDirectory(), args[0])));
                Console.Error.WriteLine("Exception thrown:" + e.Message);
                Console.Error.WriteLine(e.StackTrace);
                return -1;
            }

            if (args.Length == 3)
            {
                ulong segTableAddr;
                try
                {
                    // Try to parse as decimal
                    segTableAddr = Convert.ToUInt64(args[2], 10);
                }
                catch
                {
                    try
                    {
                        // Try to parse as hexadecimal
                        segTableAddr = Convert.ToUInt64(args[2], 16);
                    }
                    catch
                    {
                        Console.Error.WriteLine(string.Format(string.Format("Invalid segment table address given: {0}", args[1])));
                        return -1;
                    }
                }

                if (segTableAddr < 0x80000000 || segTableAddr > (0x88000000 - 32 * 4))
                {
                    Console.Error.WriteLine(string.Format("Segment table address 0x{0:X7} out of range!", startAddress));
                    return -1;
                }

                segTableAddr -= 0x80000000;

                for (int segIndex = 0; segIndex < 32; segIndex++)
                {
                    segTable[segIndex] = (ulong)
                        ((rdram[(4 * segIndex) + (int)segTableAddr + 0] << 24) |
                         (rdram[(4 * segIndex) + (int)segTableAddr + 1] << 16) |
                         (rdram[(4 * segIndex) + (int)segTableAddr + 2] <<  8) |
                         (rdram[(4 * segIndex) + (int)segTableAddr + 3] <<  0));
                }
            }

            int totalTris = 0;
            int totalVertices = 0;

            F3DEX2Decoder.PrintDisplaylist(rdram, startAddress, segTable, ref totalTris, ref totalVertices);

            Console.WriteLine(string.Format("Total Tris {0}", totalTris));
            Console.WriteLine(string.Format("Total Vertices {0}", totalVertices));

            return 0;
        }
    }
}
