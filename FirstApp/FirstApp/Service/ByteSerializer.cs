using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text.Json;

namespace FirstApp.Service
{
	public class ByteSerializer
	{
		// public byte[] Serialize(object o)
		// {
		//     var json = JsonSerializer.CreateDefault();
		//     json.Serialize(new JTokenWriter(), o );
		//     var ms = new MemoryStream();
		//     Serializer.Serialize(ms, o);
		//     return ms.ToArray();
		// }
		//
		// public object Deserialize(byte[] bytes)
		// {
		//     var ms = new MemoryStream(bytes);
		//     return formatter.Deserialize(ms);
		// }
		//
		// private readonly IFormatter formatter = new BinaryFormatter();
	}
}