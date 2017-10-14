# MyInjectionFramework
This is a test project to understand Dependancy Injection frameworks like NInject. 
**It is not a production worthy framework.

## TODO
### DI.Get<T>()
- [x] Simple structures
- [ ] Classes with multiple constructors ( only try to resolve most complex constructor )
- [ ] Classes without constructor
- [ ] Circular reference detection
- [ ] Using the same instance when resolving the same type in a chain

### DI.Map...
- [x] DI.Map<TRequested, TActual>()
- [ ] DI.Map<TRequested>(TActual () => { return T})
- [ ] DI.AsConstant().Map<,>()
- [ ] DI.PerRequest().Map<,>()
