namespace Aiwins.Rocket.Pinyin {
    public interface IPySpelling {
        string Name { get; set; }
        string FullPySpelling { get; set; }
        string FirstPySpelling { get; set; }
    }
}