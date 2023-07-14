/*두개의 GameObject objA, objB에 대해서
objA.Equals(objB) 와같이 코드를 만들면 
objA나 objB가 null일 경우에 error가 발생함.
하나가 null이고 나머지 하나는 값이 있을때 false,
둘다 null일때 true로 나오게 하기 위한 코드*/

public class EqualErrorCheck{
  void Test(){
    if (objA == null && objB == null)
    {
        return true; // 둘 다 null인 경우
    }
    else if (objA == null || objB == null)
    {
        return false; // 하나는 null이고 다른 하나는 값이 있는 경우
    }
    else
    {
        return objA.Equals(objB); // 둘 다 값이 있는 경우
    }
    
  }
  
}
