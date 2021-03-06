﻿﻿/**
 *  XZ.NET - a .NET wrapper for liblzma.dll
 *
 *  Copyright 2015 by Roman Belkov <romanbelkov@gmail.com>
 *  Copyright 2015 by Melentyev Kirill <melentyev.k@gmail.com>
 *
 *  Licensed under GNU General Public License 3.0 or later. 
 *  Some rights reserved. See LICENSE, AUTHORS, LICENSE-Notices.
 *
 * @license GPL-3.0+ <http://www.gnu.org/licenses/gpl-3.0.en.html>
 */

using System;
using System.Runtime.InteropServices;

namespace XZ.NET
{
    internal enum LzmaReturn : uint
    {
        LzmaOK = 0,
        LzmaStreamEnd = 1,
        LzmaNoCheck = 2,
        LzmaUnsupportedCheck = 3,
        LzmaGetCheck = 4,
        LzmaMemError = 5,
        LzmaMemlimitError = 6,
        LzmaFormatError = 7,
        LzmaOptionsError = 8,
        LzmaDataError = 9,
        LzmaBufError = 10,
        LzmaProgError = 11
    }

    internal enum LzmaAction
    {
        LzmaRun = 0,
        LzmaSyncFlush = 1,
        LzmaFullFlush = 2,
        LzmaFinish = 3,
        LzmaFullBarrier = 4
    }

    internal enum LzmaCheck
    {
        LzmaCheckNone = 0,
        LzmaCheckCrc32 = 1,
        LzmaCheckCrc64 = 4,
        LzmaCheckSha256 = 10
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LzmaStreamFlags
    {
        private readonly UInt32 version;

        public UInt64 backwardSize;

        public LzmaCheck check;

        private readonly int reserved_enum1;
        private readonly int reserved_enum2;
        private readonly int reserved_enum3;
        private readonly int reserved_enum4;
        private readonly char reserved_bool1;
        private readonly char reserved_bool2;
        private readonly char reserved_bool3;
        private readonly char reserved_bool4;
        private readonly char reserved_bool5;
        private readonly char reserved_bool6;
        private readonly char reserved_bool7;
        private readonly char reserved_bool8;
        private readonly UInt32 reserved_int1;
        private readonly UInt32 reserved_int2;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LzmaMT
    {
        public UInt32 flags;
        public UInt32 threads;
        public UInt64 block_size;
        public UInt32 timeout;
        public UInt32 preset;
        public IntPtr filters;
        //public LzmaFilter filters;
        public LzmaCheck check;

        private readonly int reserved_enum1;
        private readonly int reserved_enum2;
        private readonly int reserved_enum3;
        private readonly int reserved_int1;
        private readonly int reserved_int2;
        private readonly int reserved_int3;
        private readonly int reserved_int4;
        private readonly UInt64 reserved_int5;
        private readonly UInt64 reserved_int6;
        private readonly UInt64 reserved_int7;
        private readonly UInt64 reserved_int8;
        private readonly IntPtr reserved_ptr1;
        private readonly IntPtr reserved_ptr2;
        private readonly IntPtr reserved_ptr3;
        private readonly IntPtr reserved_ptr4;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LzmaStream
    {
        public IntPtr next_in;
        public UInt32 avail_in;
        public UInt64 total_in;

        public IntPtr next_out;
        public UInt32 avail_out;
        public UInt64 total_out;

        public IntPtr allocator;

        private readonly IntPtr internalState;

        private readonly IntPtr reserved_ptr1;
        private readonly IntPtr reserved_ptr2;
        private readonly IntPtr reserved_ptr3;
        private readonly IntPtr reserved_ptr4;
        private readonly UInt64 reserved_int1;
        private readonly UInt64 reserved_int2;
        private readonly UInt32 reserved_int3;
        private readonly UInt32 reserved_int4;
        private readonly UInt32 reserved_enum1;
        private readonly UInt32 reserved_enum2;
    }
    public static class Native
    {
        internal delegate LzmaReturn lzma_stream_decoder_delegate(ref LzmaStream stream, UInt64 memLimit, UInt32 flags);
        internal static readonly lzma_stream_decoder_delegate lzma_stream_decoder = IntPtr.Size > 4 ? (lzma_stream_decoder_delegate)NativeX64.lzma_stream_decoder : NativeX86.lzma_stream_decoder;

        internal delegate LzmaReturn lzma_code_delegate(ref LzmaStream stream, LzmaAction action);
        internal static readonly lzma_code_delegate lzma_code = IntPtr.Size > 4 ? (lzma_code_delegate)NativeX64.lzma_code : NativeX86.lzma_code;

        internal delegate LzmaReturn lzma_stream_footer_decode_delegate(ref LzmaStreamFlags options, byte[] inp);
        internal static readonly lzma_stream_footer_decode_delegate lzma_stream_footer_decode = IntPtr.Size > 4 ? (lzma_stream_footer_decode_delegate)NativeX64.lzma_stream_footer_decode : NativeX86.lzma_stream_footer_decode;

        internal delegate UInt64 lzma_index_uncompressed_size_delegate(IntPtr i);
        internal static readonly lzma_index_uncompressed_size_delegate lzma_index_uncompressed_size = IntPtr.Size > 4 ? (lzma_index_uncompressed_size_delegate)NativeX64.lzma_index_uncompressed_size : NativeX86.lzma_index_uncompressed_size;

        internal delegate UInt32 lzma_index_buffer_decode_delegate(ref IntPtr i, ref UInt64 memLimit, IntPtr allocator, byte[] indexBuffer, ref UInt32 inPosition, UInt64 inSize);
        internal static readonly lzma_index_buffer_decode_delegate lzma_index_buffer_decode = IntPtr.Size > 4 ? (lzma_index_buffer_decode_delegate)NativeX64.lzma_index_buffer_decode : NativeX86.lzma_index_buffer_decode;

        internal delegate void lzma_index_end_delegate(IntPtr i, IntPtr allocator);
        internal static readonly lzma_index_end_delegate lzma_index_end = IntPtr.Size > 4 ? (lzma_index_end_delegate)NativeX64.lzma_index_end : NativeX86.lzma_index_end;

        internal delegate void lzma_end_delegate(ref LzmaStream stream);
        internal static readonly lzma_end_delegate lzma_end = IntPtr.Size > 4 ? (lzma_end_delegate)NativeX64.lzma_end : NativeX86.lzma_end;

        internal delegate LzmaReturn lzma_stream_encoder_mt_delegate(ref LzmaStream stream, ref LzmaMT mt);
        internal static readonly lzma_stream_encoder_mt_delegate lzma_stream_encoder_mt = IntPtr.Size > 4 ? (lzma_stream_encoder_mt_delegate)NativeX64.lzma_stream_encoder_mt : NativeX86.lzma_stream_encoder_mt;
    }

    public static class NativeX64
    {
        [DllImport("liblzma64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern LzmaReturn lzma_stream_decoder(ref LzmaStream stream, UInt64 memLimit, UInt32 flags);

        [DllImport("liblzma64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern LzmaReturn lzma_code(ref LzmaStream stream, LzmaAction action);

        [DllImport("liblzma64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern LzmaReturn lzma_stream_footer_decode(ref LzmaStreamFlags options, byte[] inp);

        [DllImport("liblzma64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern UInt64 lzma_index_uncompressed_size(IntPtr i);

        [DllImport("liblzma64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern UInt32 lzma_index_buffer_decode(ref IntPtr i, ref UInt64 memLimit, IntPtr allocator, byte[] indexBuffer,
            ref UInt32 inPosition, UInt64 inSize);

        [DllImport("liblzma64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void lzma_index_end(IntPtr i, IntPtr allocator);

        [DllImport("liblzma64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void lzma_end(ref LzmaStream stream);

        [DllImport("liblzma64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern LzmaReturn lzma_stream_encoder_mt(ref LzmaStream stream, ref LzmaMT mt);
    }
    public static class NativeX86
    {
        [DllImport("liblzma.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern LzmaReturn lzma_stream_decoder(ref LzmaStream stream, UInt64 memLimit, UInt32 flags);

        [DllImport("liblzma.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern LzmaReturn lzma_code(ref LzmaStream stream, LzmaAction action);

        [DllImport("liblzma.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern LzmaReturn lzma_stream_footer_decode(ref LzmaStreamFlags options, byte[] inp);

        [DllImport("liblzma.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern UInt64 lzma_index_uncompressed_size(IntPtr i);

        [DllImport("liblzma.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern UInt32 lzma_index_buffer_decode(ref IntPtr i, ref UInt64 memLimit, IntPtr allocator, byte[] indexBuffer,
            ref UInt32 inPosition, UInt64 inSize);

        [DllImport("liblzma.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void lzma_index_end(IntPtr i, IntPtr allocator);

        [DllImport("liblzma.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void lzma_end(ref LzmaStream stream);

        [DllImport("liblzma.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern LzmaReturn lzma_stream_encoder_mt(ref LzmaStream stream, ref LzmaMT mt);
    }
}