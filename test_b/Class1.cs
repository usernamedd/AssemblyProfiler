namespace test_b;
using test_a;
public class ClassB
{
    public void CallMethodFromA()
    {
        ClassA.StaticMethod();
        new ClassA().InstanceMethod();
        new ClassA().AgeOfProperty=0;
    }
}
