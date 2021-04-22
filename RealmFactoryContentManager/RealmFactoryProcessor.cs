using Microsoft.Xna.Framework.Content.Pipeline;

using TInput = RealmFactory.Core.LevelSet;
using TOutput = RealmFactory.Core.LevelSet;

namespace Starbound.RealmFactoryContentManager
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    /// </summary>
    [ContentProcessor(DisplayName = "Realm Factory Project Processor")]
    public class RealmFactoryProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            return input;
        }
    }
}