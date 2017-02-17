using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Expert_system_shell_Ladyzhets2015
{
	public class TreeViewExposition : Exposition
	{
		private TreeView treeNode = new TreeView();

		private F_ExplanRes formExplanation = new F_ExplanRes();

		private Stack<TreeNodeCollection> nodeCollectionParentStack = new Stack<TreeNodeCollection>();

		private TreeNodeCollection currNodeCollection;

		public override void startRecording()
		{
			this.treeNode.Nodes.Clear();
			this.nodeCollectionParentStack.Clear();
			this.currNodeCollection = this.treeNode.Nodes;
		}

		public override void goToLowLevel()
		{
			if (this.currNodeCollection.Count == 0)
			{
				throw new InvalidOperationException();
			}
			this.nodeCollectionParentStack.Push(this.currNodeCollection);
			this.currNodeCollection = this.currNodeCollection[this.currNodeCollection.Count - 1].Nodes;
		}

		public override void goToHighLevel()
		{
			this.currNodeCollection = this.nodeCollectionParentStack.Pop();
		}

		public override void printText(string text)
		{
			this.currNodeCollection.Add(text);
			this.treeNode.ExpandAll();
		}

		public void showExposition()
		{
			this.formExplanation = new F_ExplanRes();
			this.formExplanation.Controls.Add(this.treeNode);
			this.treeNode.Dock = 5;
			int num = this.formExplanation.ShowDialog();
		}
	}
}
