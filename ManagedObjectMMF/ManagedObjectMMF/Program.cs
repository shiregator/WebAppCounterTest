using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Text;

namespace ManagedMMF
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Build a sample object and report records
            TestObject obj = new TestObject();
            obj = CreateTestObject();
            Console.WriteLine("object created with " + obj.test1 + "& " + obj.test2);

            // Write object to MMF
            WriteObjectToMMF("C:\\TEMP\\TEST.MMF", obj);

            // Clear object and report
            obj = null;
            Console.WriteLine("Database object has been destroyed.");

            // Read new object from MMF and report records
            obj = ReadObjectFromMMF("C:\\TEMP\\TEST.MMF") as TestObject;
            Console.WriteLine("obect re-loaded from MMF with " + obj.test1 + "& " + obj.test2);

            // Wait for input and terminate
            Console.ReadLine();
        }

        #region Generic MMF read/write object functions

        public static void WriteObjectToMMF(string mmfFile, object objectData)
        {
            // Convert .NET object to byte array
            byte[] buffer = ObjectToByteArray(objectData);

            // Create a new memory mapped file
            using (MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(mmfFile, FileMode.Create, null, buffer.Length))
            {
                // Create a view accessor into the file to accommmodate binary data size
                using (MemoryMappedViewAccessor mmfWriter = mmf.CreateViewAccessor(0, buffer.Length))
                {
                    // Write the data
                    mmfWriter.WriteArray<byte>(0, buffer, 0, buffer.Length);
                }
            }
        }

        public static object ReadObjectFromMMF(string mmfFile)
        {
            // Get a handle to an existing memory mapped file
            using (MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(mmfFile, FileMode.Open))
            {
                // Create a view accessor from which to read the data
                using (MemoryMappedViewAccessor mmfReader = mmf.CreateViewAccessor())
                {
                    // Create a data buffer and read entire MMF view into buffer
                    byte[] buffer = new byte[mmfReader.Capacity];
                    mmfReader.ReadArray<byte>(0, buffer, 0, buffer.Length);

                    // Convert the buffer to a .NET object
                    return ByteArrayToObject(buffer);
                }
            }
        }

        #endregion

        #region Object/Binary serialization

        public static object ByteArrayToObject(byte[] buffer)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();    // Create new BinaryFormatter
            MemoryStream memoryStream = new MemoryStream(buffer);       // Convert byte array to memory stream, set position to start
            return binaryFormatter.Deserialize(memoryStream);           // Deserializes memory stream into an object and return
        }

        public static byte[] ObjectToByteArray(object inputObject)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();    // Create new BinaryFormatter
            MemoryStream memoryStream = new MemoryStream();             // Create target memory stream
            binaryFormatter.Serialize(memoryStream, inputObject);       // Convert object to memory stream
            return memoryStream.ToArray();                              // Return memory stream as byte array
        }

        #endregion

        public static TestObject CreateTestObject()
        {
            TestObject obj = new TestObject();

            return obj;
        }
    }




    [Serializable]
    public class TestObject
    {
        public String test1 = "test3 Serialized Object";
        public String test2;
		// Uncommenting the following line will result in a runtime exception because the object is not serializable 
		// This simulates a situation if you have an included assembly dll which you do not have access to which is not serializable
		//public UnserializableTestObject usto;

		public TestObject()
        {
            this.test2 = "test4 Serialized Object";
		}
	}

	public class UnserializableTestObject
	{
		public String test3 = "test5 unserialized Object";
		public String test4;

		public UnserializableTestObject()
		{
			this.test4 = "test6 unserialized Object";
		}
	}



}
