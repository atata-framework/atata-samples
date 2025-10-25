[assembly: Parallelizable(ParallelScope.Children)]
[assembly: SetCulture("en-US")]

// LevelOfParallelism is optional here to restrict the number of parallel threads in testing purposes.
[assembly: LevelOfParallelism(3)]
