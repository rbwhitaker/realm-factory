using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Starbound.RealmFactory.DataModel
{
    public class Project
    {
        public Settings Settings { get; set; }
        public List<Level> Levels { get; set; }
        public string Name { get; set; }
        public SmartList<GameObjectType> Types { get; set; }

        public Project()
        {
            Settings = new Settings() { CellWidth = 20, CellHeight = 20, Rows = 30, Columns = 30, BackColor = Color.Black };
            Levels = new List<Level>();
            Name = "New Project";
            Types = new SmartList<GameObjectType>();
        }

        public static Project CreateDefaultProject()
        {
            Project project = new Project();
            project.Settings = new Settings() { CellWidth = 20, CellHeight = 20, Rows = 30, Columns = 30, BackColor = Color.Black };
            project.Name = "New Project";
            project.CreateNewLevel();

            return project;
        }

        public void CreateNewLevel()
        {
            Levels.Add(new Level(GenerateUniqueLevelName()) { Settings = this.Settings.Copy() });
        }

        public void AddType(GameObjectType newType)
        {
            Types.Add(newType);
        }

        private string GenerateUniqueLevelName()
        {
            int index = 1;

            while (true)
            {
                string currentLevelName = "Level " + index;
                bool nameTaken = false;
                foreach (Level level in Levels)
                {
                    if (level.Name == currentLevelName)
                    {
                        nameTaken = true;
                        break;
                    }
                }

                if (!nameTaken) { return currentLevelName; }

                index++;
            }
        }

        public Project Copy()
        {
            Project copy = new Project();
            copy.Name = this.Name;
            copy.Settings = this.Settings.Copy();

            Dictionary<object, object> converter = new Dictionary<object, object>();

            foreach (GameObjectType type in Types)
            {
                GameObjectType typeCopy = type.Copy();
                copy.Types.Add(typeCopy);
                converter[type] = typeCopy;
            }

            foreach (Level level in Levels)
            {
                Level levelCopy = level.Copy(converter);
                levelCopy.Swap(converter);
                copy.Levels.Add(levelCopy);
            }

            return copy;
        }
    }
}
