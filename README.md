# MyInjectionFramework
This is a test project to understand Dependancy Injection frameworks like NInject. 
**It is not a production worthy framework.

## TODO
### DI.Get<T>()
- [x] Simple structures
- [x] Classes with generics
- [ ] Classes with multiple constructors ( only try to resolve most complex constructor )
- [ ] Classes without constructor
- [ ] Circular reference detection
- [x] Only use 1 instance of a resolved type on the same thread

### DI.Map...
- [x] DI.Map<TRequested, TActual>()
- [ ] DI.Map<TRequested>(TActual () => { return T})
- [x] DI.AsConstant().Map<,>()
- [ ] DI.AsPerThread().Map<,>()
- [x] DI.AsUseOnce().Map<,>()


## Interesting read
- Generics
  * https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generics-and-reflection
- Reflection 
  * https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/reflection
  * https://stackoverflow.com/questions/752/get-a-new-object-instance-from-a-type
