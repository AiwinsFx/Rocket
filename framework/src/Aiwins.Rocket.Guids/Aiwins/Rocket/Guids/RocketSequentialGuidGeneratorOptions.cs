namespace Aiwins.Rocket.Guids {
    public class RocketSequentialGuidGeneratorOptions {
        /// <summary>
        /// 默认值: <see cref="SequentialGuidType.SequentialAtEnd"/>.
        /// </summary>
        public SequentialGuidType DefaultSequentialGuidType { get; set; }

        public RocketSequentialGuidGeneratorOptions () {
            DefaultSequentialGuidType = SequentialGuidType.SequentialAtEnd;
        }
    }
}