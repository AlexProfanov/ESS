using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace MyEs.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
	internal class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("MyEs.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		[EditorBrowsable]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		internal static Bitmap Aim
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Aim", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap dialog_question
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("dialog_question", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap Document
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Document", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap Exit
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Exit", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Icon jack
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("jack", Resources.resourceCulture);
				return (Icon)@object;
			}
		}

		internal static Bitmap Open
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Open", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap Save
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Save", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap SaveAll
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("SaveAll", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal Resources()
		{
		}
	}
}
