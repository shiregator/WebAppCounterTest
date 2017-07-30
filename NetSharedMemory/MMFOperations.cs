using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NetSharedMemory
{
   public static class MMFOperation
    {       

        public static void WriteSequence(MemoryMappedViewAccessor accessor,
    Sequence seq)
        {
            // Get size of struct
            int size = Marshal.SizeOf(typeof(Sequence));
            byte[] data = new byte[size];

            // Initialize unmanaged memory.
            IntPtr p = Marshal.AllocHGlobal(size);

            try
            {
                // Copy struct to unmanaged memory.
                Marshal.StructureToPtr(seq, p, false);

                // Copy from unmanaged memory to byte array.
                Marshal.Copy(p, data, 0, size);

                // Write to memory mapped file.
                accessor.WriteArray<byte>(0, data, 0, data.Length);
            }
            finally
            {
                // Free unmanaged memory.
                Marshal.FreeHGlobal(p);
                p = IntPtr.Zero;
            }
        }

        public static Sequence ReadSequence(MemoryMappedViewAccessor accessor)
        {
            Sequence seq;
            // Get size of struct
            int size = Marshal.SizeOf(typeof(Sequence));
            byte[] data = new byte[size];

            // Initialize unmanaged memory.
            IntPtr p = Marshal.AllocHGlobal(size);

            try
            {
                // Read from memory mapped file.
                accessor.ReadArray<byte>(0, data, 0, data.Length);

                // Copy from byte array to unmanaged memory.
                Marshal.Copy(data, 0, p, size);

                // Copy unmanaged memory to struct.
                seq = (Sequence)Marshal.PtrToStructure(p, typeof(Sequence));
            }
            finally
            {
                // Free unmanaged memory.
                Marshal.FreeHGlobal(p);
                p = IntPtr.Zero;
            }

            return seq;
        }

       
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct Sequence
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
        public string Name;
        public long Minimum;
        public long Maximum;
        public long Current;
		public TestObject testObject;
    }

	public class TestObject
	{
		String test1 = "test1 Shared Memory";
		String test2;

		public TestObject()
		{
			this.test2 = "test2 Shared Memory";
		}
	}
}
