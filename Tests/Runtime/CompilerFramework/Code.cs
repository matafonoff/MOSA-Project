/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Michael Fröhlich (grover) <michael.ruck@michaelruck.de>
 *  
 */

namespace Test.Mosa.Runtime.CompilerFramework
{
	public static class Code
	{
		public const string AllTestCode = NoStdLibDefinitions + ObjectClassDefinition + VmDefinitions;

		public const string ObjectClassDefinition = @"
			namespace System
			{
				public class Object
				{
					private IntPtr methodTablePtr;
					private IntPtr syncBlock;

					public Object()
					{
					}

					public virtual int GetHashCode()
					{
						return 0;
					}

					public virtual string ToString()
					{
						return null;
					}

					public virtual bool Equals(object obj)
					{
						return true;
					}

					public virtual void Finalize()
					{
					}
				}
			}

			namespace System.Runtime.InteropServices
			{

				public enum UnmanagedType
				{
					Bool = 2,
					I1 = 3,
					U1 = 4,
					I2 = 5,
					U2 = 6,
					I4 = 7,
					U4 = 8,
					I8 = 9,
					U8 = 10,
					R4 = 11,
					R8 = 12
				}

				public sealed class MarshalAsAttribute : Attribute 
				{
					public MarshalAsAttribute(short unmanagedType) 
					{
					}
					
					public MarshalAsAttribute(UnmanagedType unmanagedType) 
					{
					}
				}
			}
		";

		public const string NoStdLibDefinitions = @"
			namespace System
			{
				public class ValueType : Object
				{
				}

				public class Enum : ValueType
				{
				}

				public class Delegate : Object
				{
				}

				public struct SByte
				{
				}

				public struct Byte
				{
				}

				public struct Int16
				{
				}

				public struct Int32
				{
				}

				public struct Int64
				{
				}   

				public struct UInt16
				{
				}

				public struct UInt32
				{
				}

				public struct UInt64
				{
				}   

				public struct Single
				{
				}

				public struct Double
				{
				}

				public struct Char
				{
				}

				public struct Boolean
				{
				}

				public struct IntPtr
				{
					private int value;
				}

				public struct UIntPtr
				{
				}

				public struct Decimal
				{
				}

				public class String
				{
					private int length;
					private char first_char;

					public int Length
					{
						get
						{
							return this.length;
						}
					}

					public unsafe char this[int index]
					{
						get
						{
							/*
							 * HACK: This is not GC safe. Once we have GCHandle and the other structures,
							 * we need to wrap this in fixed-Block.
							 * 
							 */

							char result;

							fixed (char *pChars = &this.first_char)
							{
								result = pChars[index];
							}

							return result;
						}
					}
				}

				public class MulticastDelegate : Delegate
				{
				}

				public class Array
				{
				}

				public class Exception
				{
				}

				public class Type
				{
				}

				public class Attribute
				{
				}

				public class ParamArrayAttribute : Attribute
				{
				}

				public struct RuntimeTypeHandle
				{
				}

				public struct RuntimeFieldHandle
				{
				}

				public interface IDisposable
				{
				}

				public struct Void
				{
				}

				namespace Runtime
				{
					namespace InteropServices
					{
						public class OutAttribute : Attribute
						{
						}
					}
				}

				namespace Reflection
				{
					public class DefaultMemberAttribute : Attribute
					{
						public DefaultMemberAttribute(string name)
						{
						}
					}
				}

				namespace Collections
				{
					public interface IEnumerable
					{
					}

					public interface IEnumerator
					{
					}
				}
			}
		";

		public const string VmDefinitions = @"
			namespace Mosa.Vm
			{
				
				using System;
				
				public static class Runtime
				{
					public static unsafe void* AllocateObject(void* methodTable, uint classSize)
					{
						return null;
					}

					public static unsafe void* AllocateArray(void* methodTable, uint elementSize, uint elements)
					{
						return null;
					}

					public static object Box(ValueType valueType)
					{
						return null;
					}

					public static object Castclass(object obj, UIntPtr typeHandle)
					{
						return null;
					}

					public static bool IsInstanceOfType(object obj, UIntPtr typeHandle)
					{
						return false;
					}

					public unsafe static void Memcpy(byte* destination, byte* source, int count)
					{
					}

					public unsafe static void Memset(byte* destination, byte value, int count)
					{
					}

					public static void Rethrow()
					{
					}

					public static void Throw(object exception)
					{
					}

					public static void Unbox(object obj, ValueType valueType) 
					{
					}
				}
			}
		";

	}
}
