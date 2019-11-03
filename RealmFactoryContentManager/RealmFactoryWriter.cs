using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Starbound.RealmFactoryCore;

using TWrite = Starbound.RealmFactoryCore.LevelSet;

namespace Starbound.RealmFactoryContentManager
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to write the specified data type into binary .xnb format.
    /// </summary>
    [ContentTypeWriter]
    public class RealmFactoryWriter : ContentTypeWriter<TWrite>
    {
        protected override void Write(ContentWriter output, TWrite value)
        {
            output.Write(value.Tiles.Count);

            foreach(AbstractTile tile in value.Tiles)
            {
                output.Write(tile.Id);
                output.Write(tile.Name);

                if (tile is SimpleTile2DTransitionObject)
                {
                    SimpleTile2DTransitionObject simpleTile2D = (SimpleTile2DTransitionObject)tile;
                    output.WriteObject<Texture2DContent>(simpleTile2D.TextureContent);
                }                
            }

            output.Write(value.Levels.Count);

            foreach (AbstractLevelDefinition level in value.Levels)
            {
                if (level is LevelDefinition)
                {
                    LevelDefinition simpleGrid2DLevel = (LevelDefinition)level;

                    output.Write(simpleGrid2DLevel.Name);
                    output.Write(simpleGrid2DLevel.Rows);
                    output.Write(simpleGrid2DLevel.Columns);
                    output.Write(simpleGrid2DLevel.CellSize);
                    output.Write(simpleGrid2DLevel.BackColor);

                    for (int row = 0; row < simpleGrid2DLevel.Rows; row++)
                    {
                        for (int column = 0; column < simpleGrid2DLevel.Columns; column++)
                        {
                            output.Write(simpleGrid2DLevel.GetTileId(row, column));
                        }
                    }
                }
            }
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Starbound.RealmFactoryCore.RealmFactoryReader, RealmFactoryCore";
        }
    }
}
