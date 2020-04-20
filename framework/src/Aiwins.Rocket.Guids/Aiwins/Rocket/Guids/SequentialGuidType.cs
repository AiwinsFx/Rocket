namespace Aiwins.Rocket.Guids {
    /// <summary>
    /// GUID生成方式
    /// </summary>
    public enum SequentialGuidType {
        /// <summary>
        /// 通过方法 <see cref="Guid.ToString()" /> 使GUID顺序排序。
        /// 用于MySql and PostgreSql数据库。
        /// </summary>
        SequentialAsString,

        /// <summary>
        /// 通过方法 <see cref="Guid.ToByteArray" /> 使GUID顺序排序。
        /// 用于Oracle数据库。
        /// </summary>
        SequentialAsBinary,

        /// <summary>
        /// GUID顺序排序位于Data4块的末尾字节
        /// 用于SqlServer数据库。
        /// </summary>
        SequentialAtEnd
    }
}