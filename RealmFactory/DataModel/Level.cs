using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.RealmFactory.DataModel
{
    public class Level
    {
        public string Name
        {
            get;
            set;
        }

        private GameObject[,] tiles;

        private Settings settings;

        public Settings Settings
        {
            get
            {
                return settings;
            }
            set
            {
                settings = value;
                RebuildTiles();
            }
        }

        public Level(string name)
        {
            Name = name;
            Settings = new Settings();
            RebuildTiles();
        }

        public override string ToString()
        {
            return Name;
        }

        public void RebuildTiles()
        {
            GameObject[,] newTiles = new GameObject[settings.Columns, settings.Rows];

            if (tiles == null)
            {
                tiles = newTiles;
                return;
            }

            for (int row = 0; row < newTiles.GetLength(1) && row < tiles.GetLength(1); row++)
            {
                for (int column = 0; column < newTiles.GetLength(0) && column < tiles.GetLength(0); column++)
                {
                    newTiles[column, row] = tiles[column, row];
                }
            }

            tiles = newTiles;
        }

        public GameObject Get(int x, int y)
        {
            return tiles[x, y];
        }

        public void Put(GameObject imageTile, int x, int y)
        {
            tiles[x, y] = imageTile;
        }

        public void Erase(int x, int y)
        {
            tiles[x, y] = null;
        }

        public void Clear()
        {
            tiles = new GameObject[settings.Columns, settings.Rows];
        }

        public Level Copy(Dictionary<object, object> converter)
        {
            Level copy = new Level(this.Name);
            copy.Settings = this.Settings.Copy();

            for (int row = 0; row < copy.tiles.GetLength(1) && row < tiles.GetLength(1); row++)
            {
                for (int column = 0; column < copy.tiles.GetLength(0) && column < tiles.GetLength(0); column++)
                {
                    if (tiles[column, row] == null)
                    {
                        copy.tiles[column, row] = null;
                    }
                    else
                    {
                        copy.tiles[column, row] = tiles[column, row].Copy();
                    }
                }
            }

            return copy;
        }

        public void Swap(Dictionary<object, object> converter)
        {
            for (int row = 0; row < tiles.GetLength(1); row++)
            {
                for (int column = 0; column < tiles.GetLength(0); column++)
                {
                    if (tiles[column, row] != null)
                    {
                        tiles[column, row].ParentType = (GameObjectType)converter[tiles[column, row].ParentType];
                    }
                }
            }
        }

        public bool UsesType(GameObjectType tile)
        {
            for (int row = 0; row < tiles.GetLength(1); row++)
            {
                for (int column = 0; column < tiles.GetLength(0); column++)
                {
                    if (tiles[column, row].ParentType == tile)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void RemoveType(GameObjectType type)
        {
            for (int row = 0; row < tiles.GetLength(1); row++)
            {
                for (int column = 0; column < tiles.GetLength(0); column++)
                {
                    if (tiles[column, row].ParentType == type)
                    {
                        tiles[column, row] = null;
                    }
                }
            }
        }
    }
}
