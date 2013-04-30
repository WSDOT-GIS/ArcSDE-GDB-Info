﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wsdot.Geodatabase
{
	public class GeodatabaseItemInfo
	{
		public string Name { get; set; }
		public string PhysicalName { get; set; }
		public string Path { get; set; }
		public GeodatabaseType Type { get; set; }
	}

	/// <summary>
	/// Represents a type of object in an ArcSDE Geodatabase.
	/// </summary>
	public struct GeodatabaseType
	{
		private Guid _uuid;
		private string _name;
		private Guid _parentTypeId;

		public GeodatabaseType(string name, Guid uuid, Guid parentTypeId)
		{
			_name = name;
			_uuid = uuid;
			_parentTypeId = parentTypeId;
		}

		/// <summary>
		/// The GUID that identifies this type. This value is what will be used to compare this <see cref="GeodatabaseType"/> 
		/// against another <see cref="GeodatabaseType"/>.
		/// </summary>
		public Guid Uuid
		{
			get { return _uuid; }
		}

		/// <summary>
		/// The name of the type.
		/// </summary>
		public string Name
		{
			get { return _name; }
		}

		/// <summary>
		/// The GUID of the parent of this type.
		/// </summary>
		public Guid ParentTypeId
		{
			get { return _parentTypeId; }
		}

		/// <summary>
		/// Compares the <see cref="Uuid"/>
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (obj != null && obj.GetType() == typeof(GeodatabaseType))
			{
				return this.Uuid == ((GeodatabaseType)obj).Uuid;
			}
			return base.Equals(obj);
		}

		/// <summary>
		/// Returns the hash code of the <see cref="GeodatabaseType.Uuid"/>.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this._uuid.GetHashCode();
		}

		/// <summary>
		/// Determines if the <see cref="GeodatabaseType.Uuid"/> properties of two <see cref="GeodatabaseType"/> values are equal.
		/// </summary>
		/// <param name="gt1"></param>
		/// <param name="gt2"></param>
		/// <returns></returns>
		public static bool operator ==(GeodatabaseType gt1, GeodatabaseType gt2)
		{
			return gt1._uuid.Equals(gt2);
		}

		/// <summary>
		/// Determines if the <see cref="GeodatabaseType.Uuid"/> properties of two <see cref="GeodatabaseType"/> values are not equal.
		/// </summary>
		/// <param name="gt1"></param>
		/// <param name="gt2"></param>
		/// <returns></returns>
		public static bool operator !=(GeodatabaseType gt1, GeodatabaseType gt2)
		{
			return !gt1._uuid.Equals(gt2);
		}
	}
}

/*
ObjectID	UUID	Name	ParentTypeID
1	8405ADD5-8DF8-4227-8FAC-3FCADE073386	Item	00000000-0000-0000-0000-000000000000
2	F3783E6F-65CA-4514-8315-CE3985DAD3B1	Folder	8405ADD5-8DF8-4227-8FAC-3FCADE073386
3	FFD09C28-FE70-4E25-907C-AF8E8A5EC5F3	Resource	8405ADD5-8DF8-4227-8FAC-3FCADE073386
4	28DA9E89-FF80-4D6D-8926-4EE2B161677D	Dataset	FFD09C28-FE70-4E25-907C-AF8E8A5EC5F3
5	FBDD7DD6-4A25-40B7-9A1A-ECC3D1172447	Tin	28DA9E89-FF80-4D6D-8926-4EE2B161677D
6	D4912162-3413-476E-9DA4-2AEFBBC16939	AbstractTable	28DA9E89-FF80-4D6D-8926-4EE2B161677D
7	B606A7E1-FA5B-439C-849C-6E9C2481537B	Relationship Class	28DA9E89-FF80-4D6D-8926-4EE2B161677D
8	74737149-DCB5-4257-8904-B9724E32A530	Feature Dataset	28DA9E89-FF80-4D6D-8926-4EE2B161677D
9	73718A66-AFB9-4B88-A551-CFFA0AE12620	Geometric Network	28DA9E89-FF80-4D6D-8926-4EE2B161677D
10	767152D3-ED66-4325-8774-420D46674E07	Topology	28DA9E89-FF80-4D6D-8926-4EE2B161677D
11	E6302665-416B-44FA-BE33-4E15916BA101	Survey Dataset	28DA9E89-FF80-4D6D-8926-4EE2B161677D
12	D5A40288-029E-4766-8C81-DE3F61129371	Schematic Dataset	28DA9E89-FF80-4D6D-8926-4EE2B161677D
13	DB1B697A-3BB6-426A-98A2-6EE7A4C6AED3	Toolbox	28DA9E89-FF80-4D6D-8926-4EE2B161677D
14	C673FE0F-7280-404F-8532-20755DD8FC06	Workspace	28DA9E89-FF80-4D6D-8926-4EE2B161677D
15	DC9EF677-1AA3-45A7-8ACD-303A5202D0DC	Workspace Extension	28DA9E89-FF80-4D6D-8926-4EE2B161677D
16	77292603-930F-475D-AE4F-B8970F42F394	Extension Dataset	28DA9E89-FF80-4D6D-8926-4EE2B161677D
17	8637F1ED-8C04-4866-A44A-1CB8288B3C63	Domain	28DA9E89-FF80-4D6D-8926-4EE2B161677D
18	4ED4A58E-621F-4043-95ED-850FBA45FCBC	Replica	28DA9E89-FF80-4D6D-8926-4EE2B161677D
19	D98421EB-D582-4713-9484-43304D0810F6	Replica Dataset	28DA9E89-FF80-4D6D-8926-4EE2B161677D
20	DC64B6E4-DC0F-43BD-B4F5-F22385DCF055	Historical Marker	28DA9E89-FF80-4D6D-8926-4EE2B161677D
21	CD06BC3B-789D-4C51-AAFA-A467912B8965	Table	D4912162-3413-476E-9DA4-2AEFBBC16939
22	70737809-852C-4A03-9E22-2CECEA5B9BFA	Feature Class	D4912162-3413-476E-9DA4-2AEFBBC16939
23	5ED667A3-9CA9-44A2-8029-D95BF23704B9	Raster Dataset	D4912162-3413-476E-9DA4-2AEFBBC16939
24	35B601F7-45CE-4AFF-ADB7-7702D3839B12	Raster Catalog	D4912162-3413-476E-9DA4-2AEFBBC16939
25	7771FC7D-A38B-4FD3-8225-639D17E9A131	Network Dataset	77292603-930F-475D-AE4F-B8970F42F394
26	76357537-3364-48AF-A4BE-783C7C28B5CB	Terrain	77292603-930F-475D-AE4F-B8970F42F394
27	A3803369-5FC2-4963-BAE0-13EFFC09DD73	Parcel Fabric	77292603-930F-475D-AE4F-B8970F42F394
28	A300008D-0CEA-4F6A-9DFA-46AF829A3DF2	Representation Class	77292603-930F-475D-AE4F-B8970F42F394
29	787BEA35-4A86-494F-BB48-500B96145B58	Catalog Dataset	77292603-930F-475D-AE4F-B8970F42F394
30	F8413DCB-2248-4935-BFE9-315F397E5110	Mosaic Dataset	77292603-930F-475D-AE4F-B8970F42F394
31	8C368B12-A12E-4C7E-9638-C9C64E69E98F	Coded Value Domain	8637F1ED-8C04-4866-A44A-1CB8288B3C63
32	C29DA988-8C3E-45F7-8B5C-18E51EE7BEB4	Range Domain	8637F1ED-8C04-4866-A44A-1CB8288B3C63
*/