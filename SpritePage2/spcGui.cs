using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TGADotNet;

namespace SpritePage2
{
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Main window for SpritePageCreator
	/// </summary>
	public partial class formSPC : Form
	{
		private SpriteFileModel Sprites = new SpriteFileModel();

		// C O N S T R U C T O R ////////////////////////////////////////////////////////////////////////////////////////////////

		public formSPC()
		{
			InitializeComponent();
			lvwSprites.Items.Clear();
			lvwSprites.HideSelection = false;
			Sprites.ItemsUpdated += OnSpritesUpdated;
			Sprites.ItemsAdded += OnSpritesAdded;
			Sprites.ItemsRemoved += OnSpritesRemoved;
		}

		// E V E N T S //////////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnSpritesUpdated(int i)
		{
			lvwSprites.Items[i].SubItems[0].Text = (i + 1).ToString();
			lvwSprites.Items[i].SubItems[1].Text = Sprites[i].Name;
			lvwSprites.Items[i].SubItems[2].Text = Sprites[i].Size;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnSpritesAdded(SpriteFile spritefile)
		{
			ListViewItem li = new ListViewItem((lvwSprites.Items.Count + 1).ToString());
			li.SubItems.Add(spritefile.Name);
			li.SubItems.Add(spritefile.Size);
			lvwSprites.Items.Add(li);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnSpritesRemoved(int i)
		{
			lvwSprites.Items.Remove(lvwSprites.Items[i]);
		}

		// H E L P E R S ////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void PreviewImg(Bitmap bitmap)
		{
			ImagePreview preview = new ImagePreview();
			preview.PreviewImage(bitmap);
			preview.Show();
		}

		// B U T T O N S ////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void btnAdd_Click(object sender, EventArgs e)
		{
			OpenFileDialog addSprite = new OpenFileDialog();
			addSprite.Multiselect = true;
			addSprite.Filter = "Targa Files |*.tga|PNG Files |*.png";
			if (addSprite.ShowDialog() == DialogResult.OK)
			try
			{
				Sprites.AddRange(from s in addSprite.FileNames select new SpriteFile(s));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private void btnRemove_Click(object sender, EventArgs e)
		{
			if (lvwSprites.Items.Count > 0)
			{
				int newSelection = lvwSprites.SelectedIndices[0] == lvwSprites.Items.Count - 1 ? lvwSprites.SelectedIndices[0] - 1 : lvwSprites.SelectedIndices[0];
				try
				{
					Sprites.Remove(Sprites[lvwSprites.SelectedIndices[0]]);
				}
				catch (IndexOutOfRangeException)
				{ }
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Sprite Page Creator Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				if (lvwSprites.Items.Count > 0)
					lvwSprites.Items[newSelection].Selected = true;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private void btnClear_Click(object sender, EventArgs e)
		{
			Sprites.Clear();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private void btnView_Click(object sender, EventArgs e)
		{
			try
			{
				PreviewImg(Sprites[lvwSprites.SelectedIndices[0]].Image);
			}
			catch (IndexOutOfRangeException)
			{
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private void btnGenerate_Click(object sender, EventArgs e)
		{
			if (Sprites.Count == 0)
			{
				MessageBox.Show("Please add sprites to the page by clicking the 'Add' button", "Sprite Page Creator", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			Bitmap[] bmpArray = (from s in Sprites
								 select s.Image).ToArray();
			try
			{
				PreviewImg(SpriteGen.MakePage(bmpArray, Convert.ToInt32(cbxSize.Text), pnl_Color.BackColor));
			}
			catch (FormatException)
			{
				MessageBox.Show("Size must be a number!", "Sprite Page Creator Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Sprite Page Creator Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private void btn_Up_Click(object sender, EventArgs e)
		{
			int index = lvwSprites.SelectedIndices[0];
			if (index > 0)
			{
				SpriteFile mover = new SpriteFile(Sprites[index].File);
				Sprites[index] = Sprites[index - 1];
				Sprites[index - 1] = mover;
				lvwSprites.Items[index - 1].Selected = true;
				lvwSprites.Select();
			} // end if
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private void btn_Down_Click(object sender, EventArgs e)
		{
			int index = lvwSprites.SelectedIndices[0];
			if (index < Sprites.Count)
			{
				SpriteFile mover = new SpriteFile(Sprites[index].File);
				Sprites[index] = Sprites[index + 1];
				Sprites[index + 1] = mover;
				lvwSprites.Items[index + 1].Selected = true;
				lvwSprites.Select();
			} // end if
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private void btnPreview_Click(object sender, EventArgs e)
		{
			PreviewImg(Sprites[lvwSprites.SelectedIndices[0]].Image);
		}

		private void btn_Reverse_Click(object sender, EventArgs e)
		{
			Sprites.Reverse();
		}
	}

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Class representing a single sprite image. Handles the loading
	/// and base properties of an image
	/// </summary>
	public class SpriteFile
	{
		private string _file;
		private Bitmap _image;

		public SpriteFile(string file)
		{
			_file = file;
			if (String.Compare(_file.Split('.')[_file.Split('.').Count() - 1], "TGA", true) == 0)
				_image = new TargaImage(file).AsBitmap();
			else
				_image = new Bitmap(file);
		}

		public string File
		{
			get { return _file; }
		}

		public string Size
		{
			get { return (_image.Width.ToString() + "x" + _image.Height.ToString()); }
		}

		public Bitmap Image
		{
			get { return _image; }
		}

		public string Name
		{
			get { return _file.Split('\\')[_file.Split('\\').Count() - 1]; }
		}
	} // end SpriteFile

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Class that holds a collection of sprites and uses custom events to communicate
	/// changes to the GUI
	/// </summary>
	public class SpriteFileModel : List<SpriteFile>
	{
		public delegate void ItemsUpdatedHandler(int itemChanged);
		public delegate void ItemsRemovedHandler(int itemRemoved);
		public delegate void ItemsAddedHandler(SpriteFile spritefile);

		public event ItemsUpdatedHandler ItemsUpdated;
		public event ItemsRemovedHandler ItemsRemoved;
		public event ItemsAddedHandler ItemsAdded;

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Adds a SpriteFile to the collection and notifies its subscribers
		/// </summary>
		/// <param name="spritefile">the SpriteFile to add</param>
		public void Add (SpriteFile spritefile)
		{
			base.Add(spritefile);
			if (ItemsAdded != null)
				ItemsAdded(spritefile);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Removes a SpriteFile from the collection and notifies its subscribers
		/// </summary>
		/// <param name="spritefile">the SpriteFile to remove</param>
		public void Remove(SpriteFile spritefile)
		{
			if (ItemsRemoved != null)
				ItemsRemoved(base.IndexOf(spritefile));
			base.Remove(spritefile);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Clears all entries from the collection and notifies its subscribers
		/// </summary>
		public void Clear()
		{
			for (int i = base.Count-1; i >= 0; i--)
				this.Remove(base[i]);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Reverses the order of the items in the collection and notifies its subscribers
		/// </summary>
		public void Reverse()
		{
			base.Reverse();
			if (ItemsUpdated != null)
				for (int i = 0; i < base.Count; i++)
					ItemsUpdated(i);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// adds a range of SpriteFiles from an IEnumerable to the collection and notifies its subscribers
		/// </summary>
		/// <param name="collection">IEnumerable collection to add</param>
		public void AddRange(IEnumerable<SpriteFile> collection)
		{
			base.AddRange(collection);
			if (ItemsAdded != null)
				foreach (SpriteFile c in collection)
					ItemsAdded(c);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Accessor to an individual element in the collection. Setting notifies its subscribers of changes. 
		/// </summary>
		/// <param name="i">index to access</param>
		/// <returns>SpriteFile at index i</returns>
		public SpriteFile this[int i]
		{
			get
			{
				return base[i];
			}
			set
			{
				base[i] = value;
				if (ItemsUpdated != null)
					ItemsUpdated(i);
			}
		}
	}
}
