//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using NUnit.Framework;

//namespace UnitTests.UndoRedo
//{
//    public class CheckProjectDuplication : Test
//    {
//        public void Initialize()
//        {
//        }

//        public void Execute()
//        {
//            Project project = new Project();
//            project.CreateNewLevel();
//            project.AddType(new ImageObject2DType() { Name = "test type", Image = new System.Drawing.Bitmap(20, 20) });

//            project.Levels[0].Put(project.Types[0].GenerateNew(), 5, 5);
//            project.Levels[0].Put(project.Types[0].GenerateNew(), 5, 6);

//            Project copy = project.Copy();

//            CompareProjects(project, copy);
//        }

//        private void CompareProjects(Project original, Project copy)
//        {
//            Assert.AreEqual(original.Name, copy.Name);
//            Assert.AreEqual(original.Levels.Count, copy.Levels.Count);

//            Assert.AreEqual(original.Types.Count, copy.Types.Count);

//            for (int index = 0; index < original.Types.Count; index++)
//            {
//                GameObjectType originalType = original.Types[index];
//                GameObjectType copyType = copy.Types[index];

//                Assert.AreEqual(originalType.Name, copyType.Name);
//            }

//            for(int index = 0; index < original.Levels.Count; index++)
//            {
//                Level originalLevel = original.Levels[index];
//                Level copyLevel = copy.Levels[index];
//                Assert.AreEqual(originalLevel.Name, copyLevel.Name);

//                Assert.AreNotEqual(originalLevel.Settings, copy.Settings);
//                Assert.AreEqual(originalLevel.Settings.Columns, copy.Settings.Columns);
//                Assert.AreEqual(originalLevel.Settings.Rows, copy.Settings.Rows);
//                Assert.AreEqual(originalLevel.Settings.CellWidth, copy.Settings.CellWidth);
//                Assert.AreEqual(originalLevel.Settings.CellHeight, copy.Settings.CellHeight);
//                Assert.AreNotEqual(originalLevel.Settings.BackColor, copy.Settings.BackColor);
//                Assert.AreEqual(originalLevel.Settings.BackColor.A, copy.Settings.BackColor.A);
//                Assert.AreEqual(originalLevel.Settings.BackColor.R, copy.Settings.BackColor.R);
//                Assert.AreEqual(originalLevel.Settings.BackColor.G, copy.Settings.BackColor.G);
//                Assert.AreEqual(originalLevel.Settings.BackColor.B, copy.Settings.BackColor.B);

//                for (int row = 0; row < originalLevel.Settings.Rows; row++)
//                {
//                    for (int column = 0; column < originalLevel.Settings.Columns; column++)
//                    {
//                        GameObject originalObject = originalLevel.Get(column, row);
//                        GameObject copyObject = copyLevel.Get(column, row);

//                        Assert.AreNotEqual(originalObject, copyObject);
//                        if (originalObject != null)
//                        {
//                            Assert.AreNotEqual(originalObject.ParentType, copyObject.ParentType);
//                            Assert.AreNotEqual(originalObject.Properties, copyObject.Properties);

//                            Assert.AreEqual(originalObject.GetType().ToString(), copyObject.GetType().ToString());

//                            if (originalObject is ImageObject2D)
//                            {
//                                CompareImageObject2Ds((ImageObject2D)originalObject, (ImageObject2D)copyObject);
//                            }
//                        }
//                    }
//                }
//            }
//        }

//        private void CompareImageObject2Ds(ImageObject2D original, ImageObject2D copy)
//        {
//            Assert.AreNotEqual(original, copy);
//            Assert.AreNotEqual(original.Properties, copy.Properties);
//            Assert.AreEqual(original.Properties.Properties.Count, copy.Properties.Properties.Count);
//        }
//    }
//}
