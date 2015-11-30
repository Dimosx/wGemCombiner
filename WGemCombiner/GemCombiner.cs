﻿namespace WGemCombiner
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Drawing;
	using System.Globalization;
	using System.IO;
	using System.Text;
	using System.Threading;
	using System.Windows.Forms;
	using Properties;
	using static Instruction;
	using static NativeMethods;

	public partial class GemCombiner : Form
	{
		#region Static Fields
		private static Dictionary<GemColors, string> gemGroups = new Dictionary<GemColors, string>()
		{
			[GemColors.Black] = "Black",
			[GemColors.Generic] = "Other",
			[GemColors.Kill] = "Kill",
			[GemColors.Mana] = "Mana",
			[GemColors.Orange] = "Leech (Orange)",
			[GemColors.Red] = "Red",
			[GemColors.Yellow] = "Critical (Yellow)"
		};
		#endregion

		#region Fields
		private HelpForm helpForm = new HelpForm();
		private Options optionsForm = new Options();
		private bool asyncWaiting = false;
		private Dictionary<string, RecipeCollection> recipes = new Dictionary<string, RecipeCollection>();
		#endregion

		#region Constructors
		public GemCombiner()
		{
			this.InitializeComponent();
		}
		#endregion

		#region Form/Control Methods
		private void ColorComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			var cb = this.combineComboBox.Items;
			cb.Clear();
			foreach (var item in this.recipes[this.colorComboBox.Text])
			{
				cb.Add(item.Gem.CombineTitle);
			}

			this.combineComboBox.SelectedIndex = 0; // Preselect the first in the box
		}

		private void CombineButton_Click(object sender, EventArgs e)
		{
			if (this.asyncWaiting)
			{
				return; // there was already a thread waiting for hotkey
			}

			if (GetAsyncKeyState((Keys)Settings.Default.Hotkey) != 0)
			{
				////MessageBox.Show("Key detection failed, or you were already holding hotkey. Try again.");
				Thread.Sleep(500);
				this.combineButton.PerformClick(); // ignore holding hotkey error and try again.
				return;
			}

			this.combineButton.Text = "Press " + SettingsHandler.HotkeyText + " on A1"; // hotkey
			this.asyncWaiting = true;
			do
			{
				Application.DoEvents();
				Thread.Sleep(10);

				// [HR] Cancel before starting or if form is closing
				if (GetAsyncKeyState(Keys.Escape) != 0 || !CombinePerformer.Enabled)
				{
					this.combineButton.Text = "Combine";
					this.asyncWaiting = false;
					return;
				}
			}
			while (GetAsyncKeyState((Keys)Settings.Default.Hotkey) == 0);

			// User pressed hotkey
			this.asyncWaiting = false;
			this.combineButton.Text = "Working...";
			CombinePerformer.SleepTime = (int)this.delayNumeric.Value;
			CombinePerformer.PerformCombine((int)this.stepNumeric.Value);
			if (!CombinePerformer.CancelCombine)
			{
				this.combineButton.Text = "Combine";
				Thread.Sleep(500); // guess give it 0.5sec before going again
				this.combineButton.PerformClick(); // guess its finished, click the "combine" again
			}
		}

		private void CombineComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			var combine = this.recipes[this.colorComboBox.Text][this.combineComboBox.Text];
			// this.formulaInputRichTextBox.Text = combine.Gem.Recipe();
			this.GetInstructions(combine);
			this.combineButton.PerformClick(); // Auto-load the combine button so all u have to press is "9" over the gem
		}

		private void CopyList_Click(object sender, EventArgs e)
		{
			if (this.instructionsListBox.Items.Count > 0)
			{
				var sb = new StringBuilder();
				foreach (var instruction in this.instructionsListBox.Items)
				{
					sb.AppendLine((string)instruction);
				}

				Clipboard.SetText(sb.ToString());
			}
		}

		private void ExitButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void GemCombiner_FormClosing(object sender, FormClosingEventArgs e)
		{
			CombinePerformer.Enabled = false;
			Settings.Default.Save();
			SettingsHandler.BordersChanged -= this.ApplyBorders;
			SettingsHandler.SkinChanged -= this.ApplySkin;
		}

		private void GemCombiner_Load(object sender, EventArgs e)
		{
			CombinePerformer.StepComplete += this.CombinePerformer_StepComplete;
			this.ApplySkin(null, null);
			this.ApplyBorders(null, null);
			SettingsHandler.SkinChanged += this.ApplySkin;
			SettingsHandler.BordersChanged += this.ApplyBorders;
			this.TopMost = Settings.Default.TopMost;
			this.AddTextFileRecipes();

			var cb = this.colorComboBox.Items;
			foreach (var key in this.recipes.Keys)
			{
				cb.Add(key);
			}

			this.colorComboBox.SelectedIndex = 0;
			CombinePerformer.Enabled = true;
		}

		private void GemCombiner_MouseDown(object sender, MouseEventArgs e)
		{
			// This part allows you to drag the window around while holding it anywhere
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(this.Handle, WmNclButtonDown, HtCaption, IntPtr.Zero);
			}
		}

		private void ParseRecipeButton_Click(object sender, EventArgs e)
		{
			var parsedText = this.formulaInputRichTextBox.Text;
			if (parsedText.Contains("-combine:"))
			{
				// Remove X-combine: tag
				int tagEnd = parsedText.IndexOf(':') + 1;
				parsedText = parsedText.Substring(tagEnd).Trim();
			}

			Combiner combine;
			try
			{
				combine = new Combiner(parsedText);
			}
			catch (ArgumentException ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (combine != null)
			{
				this.GetInstructions(combine);
			}
		}

		private void HelpButton_Click(object sender, EventArgs e)
		{
			this.helpForm.Show();
		}

		private void OptionsButton_Click(object sender, EventArgs e)
		{
			// Open modally or we can trigger the combine while setting the hotkey. Could be worked around in other ways, but it's unlikely that a user will want to leave the Options screen open for any reason.
			this.optionsForm.ShowDialog(this);
		}

		private void StepNumeric_ValueChanged(object sender, EventArgs e)
		{
			var style = this.stepNumeric.Value == 0 ? FontStyle.Regular : FontStyle.Bold;
			this.stepNumeric.Font = new Font(this.stepNumeric.Font, style);
			this.stepLabel.Font = new Font(this.stepNumeric.Font, style);
		}
		#endregion

		#region Private Methods
		private void AddTextFileRecipes()
		{
			foreach (var filename in Directory.EnumerateFiles(".", "*.txt", SearchOption.TopDirectoryOnly))
			{
				Debug.WriteLine(filename);
				var lines = File.ReadAllLines(filename);
				var recipe = new List<string>();
				int lineNum = 0;
				foreach (var line in lines)
				{
					lineNum++;
					if (!line.StartsWith("#", StringComparison.Ordinal) && !line.StartsWith("//", StringComparison.Ordinal))
					{
						var trimmedLine = line.Trim();
						if (trimmedLine.Length == 0)
						{
							if (recipe.Count > 0)
							{
								this.AddRecipe(new Combiner(recipe));
								recipe.Clear();
							}
						}
						else if (line.Contains("="))
						{
							recipe.Add(line);
						}
						else
						{
							this.AddRecipe(new Combiner(trimmedLine));
						}
					}
				}
			}
		}

		private void AddRecipe(Combiner combine)
		{
			var gem = combine.Gem;
			var gemGroup = gemGroups[gem.Color] + " Gem " + (combine.BaseGems.Count == 1 ? "Combine" : "Spec");
			if (!this.recipes.ContainsKey(gemGroup))
			{
				this.recipes[gemGroup] = new RecipeCollection();
			}

			if (!this.recipes[gemGroup].Contains(gem.CombineTitle))
			{
				// TODO: Consider some other method of checking if these truly are duplicates or not.
				// Ignores gems with identical CombineTitles. Conceivably, there could be two different combines with identical titles, but I think this is fairly unlikely.
				this.recipes[gemGroup].Add(combine);
			}
		}

		private void ApplyBorders(object sender, EventArgs e)
		{
			SettingsHandler.ApplyBorders(this);
		}

		private void ApplySkin(object sender, EventArgs e)
		{
			SettingsHandler.ApplySkin(this);
		}

		private void CombinePerformer_StepComplete(object sender, int stepID)
		{
			Application.DoEvents();
			if (GetAsyncKeyState(Keys.Escape) != 0)
			{
				CombinePerformer.CancelCombine = true;
			}

			this.combineButton.Text = stepID.ToString(CultureInfo.CurrentCulture);
		}

		private void GetInstructions(Combiner combine)
		{
			var instructions = combine.GetInstructions();
			this.resultLabel.Text = combine.Gem.DisplayInfo(false, instructions.SlotsRequired);

			this.baseGemsListBox.Items.Clear();
			for (int ngem = 0; ngem < combine.BaseGems.Count; ngem++)
			{
				this.baseGemsListBox.Items.Add(SlotName(ngem) + ": " + combine.BaseGems[ngem].Color.ToString());
			}

			this.instructionsListBox.Items.Clear();
			var items = this.instructionsListBox.Items;
			for (int i = 0; i < instructions.Count; i++)
			{
				items.Add(string.Format(CultureInfo.CurrentCulture, "{0}: {1}", i, instructions[i]));
			}

			this.stepNumeric.Maximum = instructions.Count - 1;
		}
		#endregion
	}
}
